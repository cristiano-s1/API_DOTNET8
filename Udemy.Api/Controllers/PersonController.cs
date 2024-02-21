using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Udemy.Api.Business;
using Udemy.Api.Data.VO;
using Udemy.Api.Hypermedia.Filters;

namespace Udemy.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _business;

        public PersonController(IPersonBusiness business)
        {
            _business = business;
        }

        /// <summary>
        /// GetAll Person
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Get()
        {
            try
            {
                return Ok(_business.GetAll());
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// GetById Person
        /// </summary>
        /// <param name="id">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _business.GetById(id);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// HATEOAS Person pagination
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{sortDirection}/{pageSize}/{page}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Get([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            try
            {
                return Ok(_business.FindWithPagedSearch(name, sortDirection, pageSize, page));
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// GetById Person
        /// </summary>
        /// <param name="firstName">Event Identifier</param>
        /// <param name="lastName"></param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("findPersonByName")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Get([FromQuery] string firstName, string lastName)
        {
            try
            {
                var result = _business.FindByName(firstName, lastName);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Create Person
        /// </summary>
        /// <remarks>
        /// [{"code": 0, "first_name": "string", "last_name": "string", "address": "string", "gender": "string"}]
        /// </remarks>
        /// <param name="entity">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Post([FromBody] PersonVO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (entity == null)
                    return BadRequest();

                return Ok(_business.Update(entity));
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Patch Person
        /// </summary>
        /// <param name="id">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPatch("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Patch(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var person = _business.Disable(id);

                return Ok(person);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <remarks>
        /// [{"code": 0, "first_name": "string", "last_name": "string", "address": "string", "gender": "string"}]
        /// </remarks>
        /// <param name="entity">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<PersonVO>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Put([FromBody] PersonVO entity)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (entity == null)
                    return BadRequest();

                return Ok(_business.Update(entity));
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Delete Person
        /// </summary>
        /// <param name="id">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="204">No Content</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((StatusCodes.Status204NoContent))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Delete(int id)
        {
            try
            {
                _business.Delete(id);

                return NoContent();
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
