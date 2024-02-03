using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Udemy.Api.Business;
using Udemy.Api.Data.VO;
using Udemy.Api.Hypermedia.Filters;
using Udemy.Api.Models;

namespace Udemy.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BookController : Controller
    {
        private readonly IBookBusiness _business;

        public BookController(IBookBusiness business)
        {
            _business = business;
        }

        /// <summary>
        /// GetAll Book
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<BookVO>))]
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
        /// GetById Book
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <param name="id">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<Book>))]
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
        /// Create Book
        /// </summary>
        /// <remarks>
        /// 
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
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<Book>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Post([FromBody] BookVO entity)
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
        /// Update Book
        /// </summary>
        /// <remarks>
        /// 
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
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<Book>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status404NotFound))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Put([FromBody] BookVO entity)
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
        /// Delete Book
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
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
