using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace BdOptions
{

    public class ProcedureConcret : IRepository, IGetRepository
    {
        private readonly string _connectionString;
        public ProcedureConcret(IConfiguration Configuration)
        {
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        public void Insert<T>(T entity) where T : class
        {
            var entitie = entity.GetType();

            string procedureName = $"Insert{typeof(T).Name}";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    List<PropertyInfo> propertyInfos = entitie.GetProperties().ToList();

                    propertyInfos.RemoveAll(x => x.Name.Equals($"{typeof(T).Name}Id"));

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        var parameterName = "@" + propertyInfo.Name;
                        var value = propertyInfo.GetValue(entity);
                        command.Parameters.AddWithValue(parameterName, value);
                    }

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete<T>(long id) where T : class
        {
            var entity = Activator.CreateInstance<T>().GetType().Name;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand($"Delete{entity}", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"@{entity}Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Edit<T>(T entity) where T : class
        {
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand($"Edit{entity.GetType().Name}", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        command.Parameters.AddWithValue($"@{propertyInfo.Name}", propertyInfo.GetValue(entity));
                    }

                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            T entity = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("GetProcedureName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;


                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //entity = MapEntityFromDataReader(reader);
                        }
                    }
                }
            }

            return entity;
        }

        public IEnumerable<T> GetAll<T>(Dictionary<string, object> keyValuePairs = null, string procedureName = null) where T : class
        {
            var entities = new List<T>();

            var propertyInfos = Activator.CreateInstance<T>().GetType().GetProperties();

            if (procedureName == null)
                procedureName = $"GetAll{typeof(T).Name}";
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(procedureName, connection))
                    {
                        if (keyValuePairs != null)
                        {
                            List<SqlParameter> sqlParameters = keyValuePairs.Select(x => new SqlParameter($"{x.Key}", x.Value)).ToList();
                            command.Parameters.AddRange(sqlParameters.ToArray());
                        }

                        command.CommandType = CommandType.StoredProcedure;

                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                T entity = Activator.CreateInstance<T>();

                                foreach (var propertyInfo in propertyInfos)
                                {
                                    var columnName = propertyInfo.Name;

                                    if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                                    {
                                        propertyInfo.SetValue(entity, reader[propertyInfo.Name]);
                                    }
                                }
                                entities.Add(entity);
                            }
                        }
                    }

                    return entities;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public T GetEntityById<T>(long id) where T : class
        {
            T entity = Activator.CreateInstance<T>();

            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            string procedureName = $"Get{entity.GetType().Name}ById";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue($"@{entity.GetType().Name}Id", id);

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            foreach (PropertyInfo propertyInfo in propertyInfos)
                            {
                                propertyInfo.SetValue(entity, reader[propertyInfo.Name]);
                            }
                        }
                    }
                }
            }
            return entity;
        }
    }
}