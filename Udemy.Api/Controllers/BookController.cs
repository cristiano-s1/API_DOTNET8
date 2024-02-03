using Microsoft.AspNetCore.Mvc;
using Udemy.Api.Business;
using Udemy.Api.Models;

namespace Udemy.Api.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookBusiness _business;

        public BookController(IBookBusiness business)
        {
            _business = business;
        }

        [HttpGet]
        //[ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        //[ProducesResponseType((204))]
        //[ProducesResponseType((400))]
        //[ProducesResponseType((4001))]
        public IActionResult Get()
        {
            return Ok(_business.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _business.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book entity)
        {
            if (entity == null)
                return BadRequest();

            return Ok(_business.Insert(entity));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book entity)
        {
            if (entity == null)
                return BadRequest();

            return Ok(_business.Update(entity));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _business.Delete(id);

            return NoContent();
        }
    }
}
