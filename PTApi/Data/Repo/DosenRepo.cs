using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTApi.Data.Models;

namespace PTApi.Data.Repo
{
    public interface IDosenRepo : IRepository<Dosen>
    {
        Task<Dosen> GetDsnDet(string nik);

        PTApiContext AppContext { get; }
    }

    public class DosenRepo : Repository<Dosen>, IDosenRepo
    {
        public DosenRepo(PTApiContext context) : base(context) { }

        public PTApiContext AppContext => _context as PTApiContext;

        public async Task<Dosen> GetDsnDet(string nik)
        {
            var dsn = await AppContext.Dosen
                .Include(d => d.Kelas)
                .SingleOrDefaultAsync(d => d.NIK == nik);

            return dsn;
        }
    }
}