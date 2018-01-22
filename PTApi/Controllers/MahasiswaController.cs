using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTApi.Data.Models;
using PTApi.Data.Repo;
using PTApi.Utility;
using PTApi.ViewModels.Mahasiswa;

namespace PTApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("PT/mahasiswa")]
    public class MahasiswaController : Controller
    {
        private readonly IUnitOfWork<Mahasiswa> _unitOfWork;
        private readonly IMapper _mapper;

        public MahasiswaController(IUnitOfWork<Mahasiswa> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var mhss = await _unitOfWork.CommonRepo.GetAll();
            return Ok(_mapper.Map<IEnumerable<MhsVm>>(mhss));
        }

        [AllowAnonymous]
        [HttpGet("{nim}", Name = "GetMhs")]
        public async Task<IActionResult> Get(int nim)
        {
            var mhs = await _unitOfWork.CommonRepo.Get(m => m.Nim == nim);
            if (mhs == null) return NotFound();
            return Ok(_mapper.Map<MhsVm>(mhs));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MhsVm vm)
        {
            var mhs = _mapper.Map<Mahasiswa>(vm);
            var nimAlreadyExists = await _unitOfWork.CommonRepo.Find(m => m.Nim == vm.Nim);
            if (vm == null) return BadRequest();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            if (nimAlreadyExists) return new StatusCodeResult(StatusCodes.Status409Conflict);
            _unitOfWork.CommonRepo.Add(mhs);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<MhsVm>(mhs));
            // return CreatedAtRoute("GetMhs", new { mhs.Nim },
            //     _mapper.Map<MhsVm>(mhs));
        }

        [AllowAnonymous]
        [HttpPut("{nim}")]
        public async Task<IActionResult> Put(int nim, [FromBody]MhsVm vm)
        {
            var mhs = await _unitOfWork.CommonRepo.Get(nim);
            if (mhs == null) return NotFound();
            if (!ModelState.IsValid) return new UnprocessableEntityResult(ModelState);
            _mapper.Map(vm, mhs);
            await _unitOfWork.Complete();
            return Ok(_mapper.Map<Mahasiswa>(mhs));
        }

        [AllowAnonymous]
        [HttpDelete("{nim}")]
        public async Task<IActionResult> Delete(int nim)
        {
            var mhs = await _unitOfWork.CommonRepo.Get(nim);
            if (mhs == null) return NotFound();
            _unitOfWork.CommonRepo.Remove(mhs);
            await _unitOfWork.Complete();
            return Ok();
        }
    }
}
