﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Models;
using BL;
using DL;
using CustomExceptions;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUBL _iubl;
        public UserController(IUBL iubl)
        {
            _iubl = iubl;
        }
        // GET: api/<UserController>
        // Returns all the users in the database
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            List<User> allUsers = _iubl.GetAllUsers();
            if (allUsers.Count == 0)
            {
                return NoContent();
            }
            return Ok(allUsers);
        }
        // GET api/<UserController>/username
        // Returns a single user with the matching username
        [HttpGet("{username}")]
        public ActionResult<User> GetUserByUsername(string username)
        {
            User user = _iubl.GetCurrentUserByUsername(username);
            if (user.ID == null)
            {
                return NoContent();
            }
            return Ok(user);
        }
        // POST api/<UserController>
        // Add a user to the database
        [HttpPost]
        public ActionResult Post([FromBody] User userToAdd)
        {
            try
            {
                _iubl.AddUser(userToAdd);
                return Created("Successfully added", userToAdd);
            }
            catch (DuplicateRecordException ex)
            {
                return Conflict(ex.Message);
            }

        }
        // GET: api/<UserController>/username/password
        // Verifies if a user has the correct username and password
        [HttpGet("{username}/{password}")]
        public ActionResult<User> LoginUser(string username, string password)
        {
            User currUser = _iubl.GetCurrentUserByUsername(username);
            if (currUser.ID == null)
            {
                //No username found that matched
                return NoContent();
            }

            if (_iubl.LoginUser(username, password))
            {
                //Login information correct
                return Ok("Your login information has been validated!");
            }
            //Invalid password
            return Unauthorized("Your password is incorrect");
            
        }
    }
}
