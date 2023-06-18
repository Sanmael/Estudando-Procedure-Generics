using BdOptions.UOW;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.ServiceProcess;
using System.Threading.Tasks;
using MicroServiceDebbug.Keys;
using MicroServiceDebbug.Dependencies;
using ClassLibrary1;

namespace MicroServiceDebbug
{
    class Program
   {
        static void Main(string[] args)
        {

            if (GetKeys.GetReset())
            {
                while (true)
                {
                    IniciarServico();
                    Thread.Sleep(GetKeys.GetIntervalTime());
                }
            }
            IniciarServico();
        }

        static void IniciarServico()
        {
            TestandoClasses testandoClasses = new TestandoClasses(GetKeys.GetConnections());

            List<Person> person = testandoClasses.ObterPessoas();

            person.ForEach(x => Console.WriteLine($"{x.PersonId},{x.PersonName},{x.TaxNumber}"));
        }
    }
}
