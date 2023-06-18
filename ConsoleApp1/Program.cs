using BdOptions.AppDataBase;
using BdOptions.Repositories;
using BdOptions.UOW;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            Configuration.GetConnectionString("DefaultConnection");

            IServiceCollection services = new ServiceCollection();            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<PersonRepository>();
            services.BuildServiceProvider();

            UnitOfWork unitOfWork = new UnitOfWork(Configuration);
            

            unitOfWork.PersonRepository.GetPersonList();

    }


}
}
