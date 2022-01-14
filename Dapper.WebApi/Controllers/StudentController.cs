using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get all student list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Student.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Student.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Student student)
        {

            var data = await _unitOfWork.Student.AddAsync(student);
            return Ok(data);
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Student.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Update(Student student)
        {
            var data = await _unitOfWork.Student.UpdateAsync(student);
            return Ok(data);
        }
    }
}
