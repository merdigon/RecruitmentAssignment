using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecruitmentAssignment.Models;
using RecruitmentAssignment.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruitmentAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
        public async Task<ActionResult<IEnumerable<AccountDto>>> Get()
        {
            var accounts = await _accountService.Get();
            return Ok(accounts);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountDto>> Get(int id)
        {
            var account = await _accountService.Get(id);

            if(account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        // POST api/<AccountController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<int>> Post([FromBody] AccountDto value)
        {
            try
            {
                var id = await _accountService.Create(value);
                return new ObjectResult(id) { StatusCode = StatusCodes.Status201Created };
            }
            catch(ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult> Put(int id, [FromBody] AccountDto value)
        {
            try
            {
                await _accountService.Edit(id, value);
                return Ok();
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var wasDeleted = await _accountService.Delete(id);
            return Ok(wasDeleted);
        }
    }
}
