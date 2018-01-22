using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Models;
using PTApi.Data.Repo;
using PTApi.Utility;
using PTApi.ViewModels.Dosen;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/dosen")]
    public class DosenController : Controller
    {
        private readonly IUnitOfWork<Dosen> _unitOfWork;
        private readonly IMapper _mapper;

        public DosenController(IUnitOfWork<Dosen> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dsns = await _unitOfWork.CommonRepo.GetAll();
            return Ok(_mapper.Map<IEnumerable<DsnVm>>(dsns));
        }

        [AllowAnonymous]
        [HttpGet("{nik}", Name = "GetDsn")]
        public async Task<IActionResult> Get(string nik)
        {
            var dsn = await _unitOfWork.CommonRepo.Get(d => d.NIK == nik);
            if (dsn == null) return NotFound();
            return Ok(_mapper.Map<DsnVm>(dsn));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]DsnVm vm)
        {
            var dsn = _mapper.Map<Dosen>(vm);
            var nikAlreadyExists = await _unitOfWork.CommonRepo.Find(d => d.NIK == vm.NIK);
            if (vm == null) return BadRequest();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            if (nikAlreadyExists) return new StatusCodeResult(StatusCodes.Status409Conflict);
            _unitOfWork.CommonRepo.Add(dsn);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<DsnVm>(dsn));
            // return CreatedAtRoute("GetMhs", new { mhs.Nim },
            //     _mapper.Map<MhsVm>(mhs));
        }

        [AllowAnonymous]
        [HttpPut("{nik}")]
        public async Task<IActionResult> Put(string nik, [FromBody]DsnVm vm)
        {
            var dsn = await _unitOfWork.CommonRepo.Get(d => d.NIK == nik);
            if (dsn == null) return NotFound();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            _mapper.Map(vm, dsn);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<Dosen>(dsn));
        }

        [AllowAnonymous]
        [HttpDelete("{nik}")]
        public async Task<IActionResult> Delete(string nik)
        {
            var dsn = await _unitOfWork.CommonRepo.Get(d => d.NIK == nik);
            if (dsn == null) return NotFound();
            _unitOfWork.CommonRepo.Remove(dsn);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}