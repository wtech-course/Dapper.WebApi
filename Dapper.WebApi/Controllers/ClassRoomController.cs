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
    public class ClassRoomController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClassRoomController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Get all classroom list (Sınıfları listeleyen method)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _unitOfWork.ClassRoom.GetAllAsync();
           
            return Ok(data);
        }
        /// <summary>
        /// Example join custom method
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetJoinQuery()
        {
            var query = "select * from ClassRoom class right join Lesson less on class.Id=less.ClassRoom_Id";
            var data = await _unitOfWork.ClassRoom.GetAllJoinAsync(query);
            return Ok(data);
        }
        /// <summary>
        /// Get classroom by id (Sınıf özelliklerini id'ye göre çağıran method)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _unitOfWork.ClassRoom.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        /// <summary>
        /// Add classroom entity (Sınıf ekleme metodu)
        /// </summary>
        /// <param name="classRoom"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(ClassRoom classRoom)
        {

            var data = await _unitOfWork.ClassRoom.AddAsync(classRoom);
            return Ok(data);
        }
        /// <summary>
        /// Delete  classroom (Sınıfı id' ye göre silen metod)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _unitOfWork.ClassRoom.DeleteAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// Update classroom (Sınıf bilgilerini güncelleyen metod)
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Update(ClassRoom product)
        {
            var data = await _unitOfWork.ClassRoom.UpdateAsync(product);
            return Ok(data);
        }

        [HttpGet("[action]/{name}")]

        public async Task<IActionResult> Search(string name)
        {
            var customerData = (from tempcustomer in await _unitOfWork.ClassRoom.GetAllAsync() select tempcustomer);
            if (!string.IsNullOrEmpty(name))
            {
                customerData = customerData.Where(m =>  m.ClassRoom_Name.Contains(name));
            }
            return Ok(customerData);
        }
    }
}
