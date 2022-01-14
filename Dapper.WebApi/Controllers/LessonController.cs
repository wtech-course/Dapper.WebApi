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
    public class LessonController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LessonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.Lesson.GetAllAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.Lesson.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Add(Lesson lesson)
        {

            var data = await _unitOfWork.Lesson.AddAsync(lesson);
            return Ok(data);
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.Lesson.DeleteAsync(id);
            return Ok(data);
        }
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Update(Lesson lesson)
        {
            var data = await _unitOfWork.Lesson.UpdateAsync(lesson);
            return Ok(data);
        }
    }
}
