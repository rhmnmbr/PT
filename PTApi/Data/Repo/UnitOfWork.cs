using System;
using System.Threading.Tasks;

namespace PTApi.Data.Repo
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity : class
    {
        IRepository<TEntity> CommonRepo { get; }

        Task<bool> Complete();
    }

    public interface IUnitOfWork : IDisposable
    {
        IMahasiswaRepo Mahasiswa { get; }

        Task<bool> Complete();
    }

    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly PTApiContext _context;

        public UnitOfWork(PTApiContext context)
        {
            _context = context;
            CommonRepo = new Repository<TEntity>(_context);
        }

        public IRepository<TEntity> CommonRepo { get; }

        public async Task<bool> Complete()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly PTApiContext _context;

        public UnitOfWork(PTApiContext context)
        {
            _context = context;
            Mahasiswa = new MahasiswaRepo(_context);
        }

        public IMahasiswaRepo Mahasiswa { get; }

        public async Task<bool> Complete()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}