using ContactsManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Domain.Interfaces
{
    public interface IContactsRepository : IGenericRepository<Contact>
    {
        Task<Contact> GetContactAsync(Contact contact);
    }
}
