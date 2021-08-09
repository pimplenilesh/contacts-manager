using ContactsManager.Application.Contracts;
using ContactsManager.Application.DTOs;
using ContactsManager.Application.Validators;
using ContactsManager.Application.Wrappers;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsManager.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
        public async Task<IEnumerable<ContactDTO>> Get()
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
            //var validation = await _validator.ValidateAsync(contactDTO);
            //if (!validation.IsValid)
            //{
            //    return ValidationFailed(contactDTO, validation);
            //}

            var result = await _contactsService.AddAsync(contactDTO);
            return Created("Contacts/Get", result);
        }

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactDTO contact)
        {
            //var validation = await _validator.ValidateAsync(contact);
            //if (!validation.IsValid)
            //{
            //    return ValidationFailed(contact, validation);
            //}

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

        private IActionResult ValidationFailed(ContactDTO contactDTO, ValidationResult validation)
        {
            var response = new Response<ContactDTO>
            {
                Errors = validation.Errors.Select(x => x.ErrorMessage).ToList(),
                Data = contactDTO,
                Succeeded = false,
                Message = "One or more validations failed."
            };

            return BadRequest(response);
        }
    }
}
