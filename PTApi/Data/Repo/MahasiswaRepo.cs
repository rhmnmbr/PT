using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTApi.Data.Models;

namespace PTApi.Data.Repo
{
    public interface IMahasiswaRepo : IRepository<Mahasiswa>
    {
        Task<Mahasiswa> GetMhsDet(int nim);

        PTApiContext AppContext { get; }
    }

    public class MahasiswaRepo : Repository<Mahasiswa>, IMahasiswaRepo
    {
        public MahasiswaRepo(PTApiContext context) : base(context) { }

        public PTApiContext AppContext => _context as PTApiContext;

        public async Task<Mahasiswa> GetMhsDet(int nim)
        {
            var mhs = await AppContext.Mahasiswa
                .Include(m => m.Klasses)
                .ThenInclude(k => k.Kelas)
                .SingleOrDefaultAsync(m => m.Nim == nim);

            return mhs;
        }
    }
}