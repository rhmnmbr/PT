using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Models;
using PTApi.Data.Repo;
using PTApi.Utility;
using PTApi.ViewModels.Kelas;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/kelas")]
    public class KelasController : Controller
    {
        private readonly IUnitOfWork<Kelas> _unitOfWork;
        private readonly IMapper _mapper;

        public KelasController(IUnitOfWork<Kelas> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var klss = await _unitOfWork.CommonRepo.GetAll();
            return Ok(_mapper.Map<IEnumerable<KlsVm>>(klss));
        }

        [AllowAnonymous]
        [HttpGet("{kode}", Name = "GetKls")]
        public async Task<IActionResult> Get(string kode)
        {
            var kls = await _unitOfWork.CommonRepo.Get(k => k.KodeKelas == kode);
            if (kls == null) return NotFound();
            return Ok(_mapper.Map<KlsVm>(kls));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]KlsVm vm)
        {
            var kls = _mapper.Map<Kelas>(vm);
            var kodeAlreadyExists = await _unitOfWork.CommonRepo.Find(k => k.KodeKelas == vm.KodeKelas);
            if (vm == null) return BadRequest();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            if (kodeAlreadyExists) return new StatusCodeResult(StatusCodes.Status409Conflict);
            _unitOfWork.CommonRepo.Add(kls);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<KlsVm>(kls));
            // return CreatedAtRoute("GetKls", new { kls.KodeKelas },
            //     _mapper.Map<KlsVm>(kls));
        }

        [AllowAnonymous]
        [HttpPut("{kode}")]
        public async Task<IActionResult> Put(string kode, [FromBody]KlsVm vm)
        {
            var kls = await _unitOfWork.CommonRepo.Get(k => k.KodeKelas == kode);
            if (kode == null) return NotFound();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            _mapper.Map(vm, kls);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<Kelas>(kls));
        }

        [AllowAnonymous]
        [HttpDelete("{kode}")]
        public async Task<IActionResult> Delete(string kode)
        {
            var kls = await _unitOfWork.CommonRepo.Get(k => k.KodeKelas == kode);
            if (kls == null) return NotFound();
            _unitOfWork.CommonRepo.Remove(kls);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}