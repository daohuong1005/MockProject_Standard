using Microsoft.EntityFrameworkCore;

namespace ABSD.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        DbContext IUnitOfWork.Context => context;


        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}