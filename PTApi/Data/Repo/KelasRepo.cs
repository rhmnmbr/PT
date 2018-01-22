using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PTApi.Data.Models;

namespace PTApi.Data.Repo
{
    public interface IKelasRepo : IRepository<Kelas>
    {
        Task<Kelas> GetKlsDet(string kode);

        PTApiContext AppContext { get; }
    }

    public class KelasRepo : Repository<Kelas>, IKelasRepo
    {
        public KelasRepo(PTApiContext context) : base(context) { }

        public PTApiContext AppContext => _context as PTApiContext;

        public async Task<Kelas> GetKlsDet(string kode)
        {
            var kls = await AppContext.Kelas
                .Include(k => k.Dosen)
                .Include(k => k.Mhss)
                .ThenInclude(m => m.Mahasiswa)
                .SingleOrDefaultAsync(k => k.KodeKelas == kode);

            return kls;
        }
    }
}