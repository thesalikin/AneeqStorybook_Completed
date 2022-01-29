using AneeqStorybook.Server.Data;
using AneeqStorybook.Server.IRepository;
using AneeqStorybook.Server.Models;
using AneeqStorybook.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AneeqStorybook.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Author> _Authors;
        private IGenericRepository<Title> _Titles;
        private IGenericRepository<Reservation> _Reservations;
        private IGenericRepository<Patron> _Patrons;
        private IGenericRepository<Book> _Books;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Author> Authors
            => _Authors ??= new GenericRepository<Author>(_context);
        public IGenericRepository<Title> Titles
            => _Titles ??= new GenericRepository<Title>(_context);
        public IGenericRepository<Book> Books
            => _Books ??= new GenericRepository<Book>(_context);
        public IGenericRepository<Reservation> Reservations
            => _Reservations ??= new GenericRepository<Reservation>(_context);
        public IGenericRepository<Patron> Patrons
            => _Patrons ??= new GenericRepository<Patron>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}