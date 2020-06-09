using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using Store.Dtos;
using Store.Database.Models;
using Microsoft.AspNetCore.JsonPatch;
using Store.Contracts;
using Store.Services;
using System;
using System.Threading.Tasks;

namespace Store.Controllers
{
    [Produces("application/json")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper) 
        {

            //TODO: Add async methods
            //TODO: Implement DTO's from contracts
            //TODO: Add localization for get requests
            //TODO: Implement save changes in the methods (private)
            //TODO: Update update service method
            _itemService = itemService;
            _mapper = mapper;
        }

        // GET api/v1/items
        [HttpGet(ApiRoutes.Items.GetAll)]
        public async Task<ActionResult> GetAllItems() 
        {
            var items = await _itemService.GetAllItemsAsync();

            return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(items));
        }

        // GET api/v1/items/{id}
        [HttpGet(ApiRoutes.Items.Get, Name = "Get")]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ItemReadDto>(item));
        }

        // POST api/v1/items
        [HttpPost(ApiRoutes.Items.Create)]
        public async Task<ActionResult> CreateItem([FromBody] ItemCreateDto input)
        {
            var item = _mapper.Map<Item>(input);

            await _itemService.CreateItemAsync(item);

            var output = _mapper.Map<ItemReadDto>(item);

            return CreatedAtRoute(nameof(Get), new { Id = output.Id }, output);
        }

        // PUT api/v1/items/{id}
        [HttpPut(ApiRoutes.Items.Update)]
        public async Task<ActionResult> UpdateItem([FromRoute] Guid id, [FromBody] ItemUpdateDto itemUpdateDto)
        {
            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDto, item);
            await _itemService.UpdateItemAsync(item);

            return NoContent();
        }

        // PATCH api/v1/items/{id}
        [HttpPatch(ApiRoutes.Items.PartialUpdate)]
        public async Task<ActionResult> PartialItemUpdate([FromRoute] Guid id, [FromBody] JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            var item = await _itemService.GetItemAsync(id);

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

            await _itemService.UpdateItemAsync(item);

            return NoContent();
        }

        // DELETE api/v1/items/{id}
        [HttpDelete(ApiRoutes.Items.Delete)]
        public async Task<ActionResult> DeleteItem([FromRoute] Guid id)
        {
            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            await _itemService.DeleteItemAsync(item);

            return NoContent();
        }
    }
}