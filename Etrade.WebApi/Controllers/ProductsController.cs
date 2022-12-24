using Etrade.DAL.Abstract;
using Etrade.Entities.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Etrade.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductDAL _IProductDAL;

        public ProductsController(IProductDAL ıProductDAL)
        {
            this._IProductDAL = ıProductDAL;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_IProductDAL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || _IProductDAL.GetAll() == null)
            {
                return BadRequest();
            }

            var product = _IProductDAL.Get(Convert.ToInt32(id));
            if (product == null)
            {
                return NotFound("Ürün bulunamadı");
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _IProductDAL.Add(product);
                //return Ok(category);
                return CreatedAtAction("Get", new { id = product.Id }, product);//Status Code 201
            }
            return BadRequest();

        }

        [HttpPut]
        public IActionResult Put([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                _IProductDAL.Update(product);
                return Ok(product);
            }
            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _IProductDAL.Get(i => i.Id == id);
            if (product == null)
                return BadRequest();
            _IProductDAL.Delete(id);
            return Ok();
        }
    }
}
