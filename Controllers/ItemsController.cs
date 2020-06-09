using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using Store.Database.Models;
using Store.Contracts;
using Store.Services;
using System;
using System.Threading.Tasks;
using Store.Contracts.Responses;
using Store.Contracts.Requests;

namespace Store.Controllers
{
    [Produces("application/json")]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper) 
        {

            //TODO: Add localization for update requests
            //TODO: Update patch method
            _itemService = itemService;
            _mapper = mapper;
        }

        // GET api/v1/items
        [HttpGet(ApiRoutes.Items.GetAll)]
        public async Task<ActionResult> GetAllItems() 
        {
            var items = await _itemService.GetAllItemsAsync();

            return Ok(_mapper.Map<List<ItemResponse>>(items));
        }

        // GET api/v1/items/{id}
        [HttpGet(ApiRoutes.Items.Get)]
        public async Task<ActionResult> Get([FromRoute] Guid id)
        {
            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<ItemResponse>(item));
        }

        // POST api/v1/items
        [HttpPost(ApiRoutes.Items.Create)]
        public async Task<ActionResult> CreateItem([FromBody] CreateItemRequest input)
        {
            var item = _mapper.Map<Item>(input);

            await _itemService.CreateItemAsync(item);

            var output = _mapper.Map<ItemResponse>(item);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Items.Get.Replace("{id}", item.Id.ToString());

            return Created(locationUrl, output);
        }

        // PUT api/v1/items/{id}
        [HttpPut(ApiRoutes.Items.Update)]
        public async Task<ActionResult> UpdateItem([FromRoute] Guid id, [FromBody] UpdateItemRequest input)
        {
            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            _mapper.Map(input, item);
            await _itemService.UpdateItemAsync(item);

            return NoContent();
        }

        // PATCH api/v1/items/{id}
        //! To fix
        // [HttpPatch(ApiRoutes.Items.PartialUpdate)]
        // public async Task<ActionResult> PartialItemUpdate([FromRoute] Guid id, [FromBody] JsonPatchDocument<ItemUpdateDto> patchDocument)
        // {
        //     var item = await _itemService.GetItemAsync(id);

        //     if(item == null)
        //     {
        //         return NotFound();
        //     }

        //     var itemToPatch = _mapper.Map<ItemUpdateDto>(item);

        //     patchDocument.ApplyTo(itemToPatch, ModelState);

        //     if(!TryValidateModel(itemToPatch))
        //     {
        //         return ValidationProblem(ModelState);
        //     }

        //     _mapper.Map(itemToPatch, item);

        //     await _itemService.UpdateItemAsync(item);

        //     return NoContent();
        // }

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