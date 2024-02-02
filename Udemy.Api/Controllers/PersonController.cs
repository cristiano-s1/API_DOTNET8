using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Udemy.Api.Business;
using Udemy.Api.Models;

namespace Udemy.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        //[ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        //[ProducesResponseType((204))]
        //[ProducesResponseType((400))]
        //[ProducesResponseType((4001))]
        public IActionResult Get()
        {
            return Ok(_personBusiness.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.GetById(id);

            if (person == null) return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Create(person));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            if (person == null) return BadRequest();

            return Ok(_personBusiness.Update(person));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);

            return NoContent();
        }
    }
}
