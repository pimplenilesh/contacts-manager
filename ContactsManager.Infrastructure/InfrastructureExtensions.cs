using ContactsManager.Domain.Interfaces;
using ContactsManager.Infrastructure.Persistence;
using ContactsManager.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddDbContext<ContactsDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("ContactsDatabase")));
            services.AddTransient<IContactsRepository, ContactsRepository>();
        }
    }
}
