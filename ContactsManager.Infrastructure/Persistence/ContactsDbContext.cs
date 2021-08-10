﻿using ContactsManager.Domain.Entities;
using ContactsManager.Domain.EntityMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Infrastructure.Persistence
{
    public partial class ContactsDbContext : DbContext, IContactsDbContext
    {
        public DbSet<Contact> Contact { get; set; }

        public ContactsDbContext(DbContextOptions<ContactsDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());

            base.OnModelCreating(modelBuilder);
        }
     
        public void SetModified<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void RemoveEntity<T>(T entity)
        {
            Remove(entity);
        }
    }
}
 