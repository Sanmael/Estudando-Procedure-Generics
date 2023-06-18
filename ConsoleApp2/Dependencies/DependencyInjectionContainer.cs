using BdOptions.UOW;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceDebbug.Dependencies
{
    public class DependencyInjectionContainer
    {
        ServiceCollection serviceProvider = new ServiceCollection();
        public ServiceCollection InjetarDependencias()
        {
            serviceProvider.AddScoped<IUnitOfWork, UnitOfWork>().BuildServiceProvider();
            serviceProvider.AddScoped<TestandoClasses>().BuildServiceProvider();

            return serviceProvider;
        }
    }
}
