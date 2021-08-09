using ContactsManager.Application.Contracts;
using ContactsManager.Application.DTOs;
using ContactsManager.Application.Implementation;
using ContactsManager.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IContactsService, ContactsService>();
            ///services.AddTransient<IValidator<ContactDTO>, ContactValidator>();
        }
    }
}
