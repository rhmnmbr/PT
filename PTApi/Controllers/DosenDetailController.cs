using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Repo;
using PTApi.ViewModels.Dosen;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/dosen")]
    public class DosenDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DosenDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{nik}/detail")]
        public async Task<IActionResult> Get(string nik)
        {
            var dsn = await _unitOfWork.Dosen.GetDsnDet(nik);
            if (dsn == null) return NotFound();
            return Ok(_mapper.Map<DsnDetVm>(dsn));
        }
    }
}