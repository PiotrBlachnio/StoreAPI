using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Store.Data;
using System.Collections.Generic;
using Store.Dtos;
using Store.Database.Models;
using Microsoft.AspNetCore.JsonPatch;
using FluentValidation;
using FluentValidation.Results;
using System;
using Store.Contracts;

namespace Store.Controllers
{
    [Produces("application/json")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemRepo _repository;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<Item> _validator;

        public ItemsController(IItemRepo repository, IMapper mapper, AbstractValidator<Item> validator) 
        {

            //TODO: Services instead of repositories
            //TODO: Add async methods
            //TODO: Implement DTO's from contracts
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        // GET api/items
        [HttpGet(ApiRoutes.Items.GetAll)]
        public ActionResult <IEnumerable<ItemReadDto>> GetAllItems() 
        {
            var items = _repository.GetAllItems();

            return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }

        // GET api/items/{id}
        [HttpGet(ApiRoutes.Items.Get, Name = "Get")]
        public ActionResult <ItemReadDto> Get(int id)
        {
            var item = _repository.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        // POST api/items
        [HttpPost(ApiRoutes.Items.Create)]
        public ActionResult <ItemReadDto> CreateItem(ItemCreateDto input)
        {
            var item = _mapper.Map<Item>(input);

            ValidationResult results = _validator.Validate(item);

            if(!results.IsValid)
            {
                foreach(var failure in results.Errors)
                {
                    Console.WriteLine("Property" + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);

                    return BadRequest(new { msg = failure.ErrorMessage });
                }
            }

            _repository.CreateItem(item);
            _repository.SaveChanges();

            var output = _mapper.Map<ItemReadDto>(item);

            return CreatedAtRoute(nameof(Get), new { Id = output.Id }, output);
        }

        // PUT api/items/{id}
        [HttpPut(ApiRoutes.Items.Update)]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var item = _repository.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDto, item);
            _repository.UpdateItem(item);

            _repository.SaveChanges();
            return NoContent();
        }

        // PATCH api/items/{id}
        [HttpPatch(ApiRoutes.Items.PartialUpdate)]
        public  ActionResult PartialItemUpdate(int id, JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            var item = _repository.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<ItemUpdateDto>(item);

            patchDocument.ApplyTo(itemToPatch, ModelState);

            if(!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemToPatch, item);

            _repository.UpdateItem(item);
            _repository.SaveChanges();

            return NoContent();
        }

        // DELETE api/items/{id}
        [HttpDelete(ApiRoutes.Items.Delete)]
        public ActionResult DeleteItem(int id)
        {
            var item = _repository.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            _repository.DeleteItem(item);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}