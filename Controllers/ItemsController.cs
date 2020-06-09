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
using Store.Services;

namespace Store.Controllers
{
    [Produces("application/json")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper) 
        {

            //TODO: Services instead of repositories
            //TODO: Add async methods
            //TODO: Implement DTO's from contracts
            _itemService = itemService;
            _mapper = mapper;
        }

        // GET api/v1/items
        [HttpGet(ApiRoutes.Items.GetAll)]
        public ActionResult <IEnumerable<ItemReadDto>> GetAllItems() 
        {
            var items = _itemService.GetAllItems();

            return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }

        // GET api/v1/items/{id}
        [HttpGet(ApiRoutes.Items.Get, Name = "Get")]
        public ActionResult <ItemReadDto> Get(int id)
        {
            var item = _itemService.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        // POST api/v1/items
        [HttpPost(ApiRoutes.Items.Create)]
        public ActionResult <ItemReadDto> CreateItem(ItemCreateDto input)
        {
            var item = _mapper.Map<Item>(input);

            _itemService.CreateItem(item);
            _itemService.SaveChanges();

            var output = _mapper.Map<ItemReadDto>(item);

            return CreatedAtRoute(nameof(Get), new { Id = output.Id }, output);
        }

        // PUT api/v1/items/{id}
        [HttpPut(ApiRoutes.Items.Update)]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var item = _itemService.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDto, item);
            _itemService.UpdateItem(item);

            _itemService.SaveChanges();
            return NoContent();
        }

        // PATCH api/v1/items/{id}
        [HttpPatch(ApiRoutes.Items.PartialUpdate)]
        public  ActionResult PartialItemUpdate(int id, JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            var item = _itemService.GetItem(id);

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

            _itemService.UpdateItem(item);
            _itemService.SaveChanges();

            return NoContent();
        }

        // DELETE api/v1/items/{id}
        [HttpDelete(ApiRoutes.Items.Delete)]
        public ActionResult DeleteItem(int id)
        {
            var item = _itemService.GetItem(id);

            if(item == null)
            {
                return NotFound();
            }

            _itemService.DeleteItem(item);
            _itemService.SaveChanges();

            return NoContent();
        }
    }
}