using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Models;
using PTApi.Data.Repo;
using PTApi.Utility;
using PTApi.ViewModels;
using PTApi.ViewModels.Kelas;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/kelas")]
    public class KelasDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork<MhsKls> _unitOfWorkMhsKls;
        private readonly IUnitOfWork<Kelas> _unitOfWorkKls;
        private readonly IMapper _mapper;

        public KelasDetailController(
            IUnitOfWork unitOfWork,
            IUnitOfWork<MhsKls> unitOfWorkMhsKls,
            IUnitOfWork<Kelas> unitOfWorkKls,
            IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkMhsKls = unitOfWorkMhsKls;
            _unitOfWorkKls = unitOfWorkKls;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{kode}/detail")]
        public async Task<IActionResult> Get(string kode)
        {
            var kls = await _unitOfWork.Kelas.GetKlsDet(kode);
            if (kls == null) return NotFound();
            return Ok(_mapper.Map<KlsDetVm>(kls));
        }

        [AllowAnonymous]
        [HttpPost("{kode}/detail")]
        public async Task<IActionResult> Post([FromBody]MKVm vm)
        {
            var mhskls = _mapper.Map<MhsKls>(vm);
            var nkAlreadyExists = await _unitOfWorkMhsKls.CommonRepo.Find(mk => mk.KodeKelas == vm.KodeKelas && mk.Nim == vm.Nim);
            if (vm == null) return BadRequest();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            if (nkAlreadyExists) return new StatusCodeResult(StatusCodes.Status409Conflict);
            _unitOfWorkMhsKls.CommonRepo.Add(mhskls);
            await _unitOfWorkMhsKls.Complete();
            return Ok(_mapper.Map<MKVm>(mhskls));
        }

        [AllowAnonymous]
        [HttpDelete("{kode}/detail/{nim}")]
        public async Task<IActionResult> Delete(string kode, int nim)
        {
            var mhskls = await _unitOfWorkMhsKls.CommonRepo.Get(km => km.KodeKelas == kode && km.Nim == nim);
            if (mhskls == null) return NotFound();
            _unitOfWorkMhsKls.CommonRepo.Remove(mhskls);
            await _unitOfWorkMhsKls.Complete();
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("{kode}/detail (error)")]
        public async Task<IActionResult> Put(string kode, [FromBody]KlsDsnPutVm vm)
        {
            var kls = await _unitOfWorkKls.CommonRepo.Get(k => k.KodeKelas == kode);
            if (kode == null) return NotFound();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            _mapper.Map(vm, kls);
            await _unitOfWorkKls.Complete();
            return Ok(_mapper.Map<Kelas>(kls));
        }

        [AllowAnonymous]
        [HttpPut("{kode}/detail/{nim}")]
        public async Task<IActionResult> Put(string kode, int nim, [FromBody]MKNilVm vm)
        {
            var mhskls = await _unitOfWorkMhsKls.CommonRepo.Get(mk => mk.KodeKelas == kode && mk.Nim == nim);
            var nkExists = await _unitOfWorkMhsKls.CommonRepo.Find(mk => mk.KodeKelas == kode && mk.Nim == nim);
            if (!nkExists) return NotFound();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            _mapper.Map(vm, mhskls);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<MhsKls>(mhskls));
        }
    }
}