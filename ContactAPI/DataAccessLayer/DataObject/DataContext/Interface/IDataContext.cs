using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ContactApi.DataAccessLayer.DataObject.Entity;

namespace ContactApi.DataAccessLayer.DataObject.DataContext.Interface
{
    public interface IDataContext
    {
        public IDbConnection Connection { get; }
        DatabaseFacade Database { get; }
        public DbSet<ContactEntity> Contact { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
