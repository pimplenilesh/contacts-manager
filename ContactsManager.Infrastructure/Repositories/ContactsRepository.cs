using ContactsManager.Domain.Entities;
using ContactsManager.Domain.Interfaces;
using ContactsManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Infrastructure.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DbSet<Contact> _contacts;
        private readonly IContactsDbContext _context;

        public ContactsRepository(IContactsDbContext context)
        {
            _contacts = context.Contact;
            _context = context;
        }

        public async Task<Contact> AddAsync(Contact entity)
        {
            await _contacts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(Contact entity)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            var contacts = await _contacts.ToListAsync();
            return contacts;
        }

        public async Task<Contact> GetByIdAsync(int id)
        {
            var contact = await _contacts.FindAsync(id);
            return contact;
        }

        public async Task<Contact> GetContactAsync(Contact contact)
        {
            var contactResult = await _contacts.FirstOrDefaultAsync(x => x.Email.Equals(contact.Email) || x.PhoneNumber.Equals(contact.PhoneNumber));
            return contactResult;
        }

        public async Task<Contact> UpdateAsync(Contact entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
