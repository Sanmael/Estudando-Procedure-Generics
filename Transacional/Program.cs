using BdOptions.AppDataBase;
using BdOptions.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Transacional
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
             .AddScoped<PersonRepository>() // Registrar a classe PersonRepository
             .AddScoped<BdOptions.UOW.IUnitOfWork, BdOptions.UOW.UnitOfWork>() // Registrar a classe PersonRepository
             .AddScoped<IniciarTeste>() // Registrar a classe IniciarTeste
             .BuildServiceProvider();

            var iniciarTeste = serviceProvider.GetService<IniciarTeste>();

            iniciarTeste.ObterPessoar();

    }
    }
}
