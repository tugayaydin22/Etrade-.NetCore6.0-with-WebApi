using Etrade.DAL.Abstract;
using Etrade.Entities.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryDAL _ICategoryDAL;

        public CategoriesController(ICategoryDAL ıCategoryDAL)
        {
            this._ICategoryDAL = ıCategoryDAL;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_ICategoryDAL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || _ICategoryDAL.GetAll() == null)
            {
                return BadRequest();
            }

            var category = _ICategoryDAL.Get(Convert.ToInt32(id));
            if (category == null)
            {
                return NotFound("Categori bulunamadı");
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _ICategoryDAL.Add(category);
                //return Ok(category);
                return CreatedAtAction("Get", new { id = category.Id }, category);//Status Code 201
            }
            return BadRequest();

        }

        [HttpPut]
        public IActionResult Put([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _ICategoryDAL.Update(category);
                return Ok(category);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _ICategoryDAL.Get(id);
            if (category == null)
                return BadRequest();
            _ICategoryDAL.Delete(id);
            return Ok();
        }
    }
}
