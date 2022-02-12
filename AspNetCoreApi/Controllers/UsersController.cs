using AspNetCoreApi.Models;
using AspNetCoreApi.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepository.GetList();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User userInfo = _userRepository.GetUser(id);
            if (userInfo == null)
                return NotFound();
            else
                return new ObjectResult(userInfo);
        }

        // POST api/<UsersController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
            int retVal = _userRepository.Add(user);
            if (retVal > 0)
                return Ok();
            else
                return NotFound();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest();

            if (_userRepository.GetUser(user.id) == null) return NotFound();
            int retVal = _userRepository.EditUser(user);
            if (retVal > 0) return Ok(); else return NotFound();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _userRepository.DeleteUser(id);
            if (retVal > 0) return Ok(); else return NotFound();
        }
    }
}
