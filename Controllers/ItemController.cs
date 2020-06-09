using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Store.Data;
using System.Collections.Generic;
using Store.Dtos;
using Store.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;

namespace Items.Controllers
{

    [Route("/api/items")]
    [ApiController]
    public class ItemsController: ControllerBase 
    {
        private readonly IItemRepo _repository;
        private readonly IMapper _mapper;

        public ItemsController(IItemRepo repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/items
        [HttpGet]
        public ActionResult <IEnumerable<ItemReadDto>> GetAllItems() 
        {
            var items = _repository.GetAllItems();

            return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }

        // GET api/items/{id}
        [HttpGet("{id}", Name = "GetItemById")]
        public ActionResult <ItemReadDto> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);

            if(item == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        // POST api/items
        [HttpPost]
        public ActionResult <ItemReadDto> CreateItem(ItemCreateDto itemCreateDto)
        {
            var itemModel = _mapper.Map<Item>(itemCreateDto);

            _repository.CreateItem(itemModel);
            _repository.SaveChanges();

            var ItemReadDto = _mapper.Map<ItemReadDto>(itemModel);

            return CreatedAtRoute(nameof(GetItemById), new { Id = ItemReadDto.Id }, ItemReadDto);
        }

        // PUT api/items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var item = _repository.GetItemById(id);

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
        [HttpPatch("{id}")]
        public  ActionResult PartialItemUpdate(int id, JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            var item = _repository.GetItemById(id);

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
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            var item = _repository.GetItemById(id);

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