using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Udemy.Api.Business;
using Udemy.Api.Data.VO;

namespace Udemy.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginBusiness _business;

        public AuthController(ILoginBusiness business)
        {
            _business = business;
        }

        /// <summary>
        /// Signin Token
        /// </summary>
        /// <remarks>
        /// {"user_name": "string", "password": "string"}
        /// </remarks>
        /// <param name="user">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Signin")]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<UserVO>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Signin([FromBody] UserVO user)
        {
            try
            {
                if (user == null)
                    return BadRequest("Ivalid client request");

                var token = _business.ValidateCredentials(user);

                if (token == null)
                    return Unauthorized();

                return Ok(token);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <remarks>
        /// {"authenticated": true, "created": "string", "expiration": "string", "access_token": "string", "refresh_token": "string"}
        /// </remarks>
        /// <param name="token">Event Identifier</param>
        /// <returns>Event Data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Refresh")]
        [ProducesResponseType((StatusCodes.Status200OK), Type = typeof(List<TokenVO>))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Refresh([FromBody] TokenVO token)
        {
            try
            {
                if (token is null)
                    return BadRequest("Ivalid client request");

                var result = _business.ValidateCredentials(token);

                if (result == null)
                    return BadRequest("Ivalid client request");

                return Ok(result);
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Revoke Token
        /// </summary>
        /// <returns>Event Data</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="401">Not Authorized</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("Revoke")]
        [Authorize("Bearer")]
        [ProducesResponseType((StatusCodes.Status204NoContent))]
        [ProducesResponseType((StatusCodes.Status400BadRequest))]
        [ProducesResponseType((StatusCodes.Status401Unauthorized))]
        [ProducesResponseType((StatusCodes.Status500InternalServerError))]
        public IActionResult Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var result = _business.RevokeToken(username);

                if (!result)
                    return BadRequest("Ivalid client request");

                return NoContent();
            }
            catch (ArgumentException ex)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
