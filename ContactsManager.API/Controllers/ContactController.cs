using ContactsManager.API.Extension;
using ContactsManager.Application.Contracts;
using ContactsManager.Application.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsManager.API.Controllers
{
    [LogHandler]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        IValidator<ContactDTO> _validator;
        public ContactsController(IContactsService contactsService, IValidator<ContactDTO> validator)
        {
            _contactsService = contactsService;
            _validator = validator;
        }

        // GET: api/<ContactsController>
        [HttpGet]
        public async Task<IEnumerable<ContactResultDTO>> Get()
        { 
            var contacts = await _contactsService.GetAllContactsAsync();
            return contacts;
        }

        // GET api/<ContactsController>/5s
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _contactsService.GetByIdAsync(id);
            return Ok(contact);
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDTO contactDTO)
        {
            var result = await _contactsService.AddAsync(contactDTO);
            return Created("Contacts/Get", result);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactDTO contact)
        {
            var result = await _contactsService.UpdateAsync(id, contact);
            return Ok(result);
        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _contactsService.DeleteAsync(id);
            return Ok(result);
        }
    }
}
