using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Repo;
using PTApi.ViewModels.Mahasiswa;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/mahasiswa")]
    public class MahasiswaDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MahasiswaDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{nim}/detail")]
        public async Task<IActionResult> Get(int nim)
        {
            var mhs = await _unitOfWork.Mahasiswa.GetMhsDet(nim);
            if (mhs == null) return NotFound();
            return Ok(_mapper.Map<MhsDetVm>(mhs));
        }
    }
}