using AutoMapper;
using ContactsManager.Application.DTOs;
using ContactsManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Contact, ContactDTO>().ReverseMap();
        }
    }
}
