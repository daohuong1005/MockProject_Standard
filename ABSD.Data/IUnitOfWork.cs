using Microsoft.EntityFrameworkCore;
using System;

namespace ABSD.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        int Commit();
    }
}