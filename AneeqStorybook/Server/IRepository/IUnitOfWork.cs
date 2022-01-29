using AneeqStorybook.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AneeqStorybook.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Title> Titles { get; }
        IGenericRepository<Reservation> Reservations { get; }
        IGenericRepository<Patron> Patrons { get; }
    }
}