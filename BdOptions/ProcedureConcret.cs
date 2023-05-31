using Domain.Interfaces;
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

    public class ProcedureConcret<T> : IRepository<T>, IGetRepository<T> where T : class
    {
        private readonly string _connectionString;

        public ProcedureConcret()
        {
            _connectionString = "DATA SOURCE=DESKTOP-1BQD18C\\SQLEXPRESS;Initial Catalog=MyProject;Integrated Security=True";
        }

        public void Insert(T entity)
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

        public void Delete(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("DeleteProcedureName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Edit(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("EditProcedureName", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public T Get(Expression<Func<T, bool>> predicate)
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
                            entity = MapEntityFromDataReader(reader);
                        }
                    }
                }
            }

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            var entities = new List<T>();

            string procedureName = $"GetAll{typeof(T).Name}";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var entity = MapEntityFromDataReader(reader);
                            entities.Add(entity);
                        }
                    }
                }

                return entities;
            }
        }
        private T MapEntityFromDataReader(SqlDataReader reader)
        {
            var entity = Activator.CreateInstance<T>();

            var entityType = entity.GetType();
            var propertyInfos = entityType.GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                var columnName = propertyInfo.Name;

                if (!reader.IsDBNull(reader.GetOrdinal(columnName)))
                {
                    var value = reader[propertyInfo.Name];
                    propertyInfo.SetValue(entity, value);
                }
            }

            return entity;
        }

        public T GetEntity(long id)
        {
            var entity = Activator.CreateInstance<T>();

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
                            entity = MapEntityFromDataReader(reader);
                        }
                    }
                }
            }

            return entity;
        }

    }
}