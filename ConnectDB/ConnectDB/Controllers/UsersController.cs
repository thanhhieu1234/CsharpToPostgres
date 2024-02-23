using ConnectDB.Models;
using ConnectDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConnectDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult getAll()
        {
          var users = userService.getAll();
          return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult findOne(int id)
        {
            var users = userService.findOne(id);
            return Ok(users);
        }

        [HttpPost]
        [Route("save")]
        public IActionResult save(Users users)
        {
            long result = userService.save(users);
            if(result >= 0)
            {
                return Ok(result);
            }
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult update(Users users, int id)
        {
            userService.update(users,id);
           
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult delete( int id)
        {
            userService.delete(id);

            return Ok();
        }

    }
}
