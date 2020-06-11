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
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Store.Extensions;

namespace Store.Controllers
{
    //TODO: Change DTO's to display userId
    
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ItemsController : ControllerBase 
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper) 
        {
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
            item.UserId = HttpContext.GetUserId();

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
            var userOwnsItem = await _itemService.UserOwnsItemAsync(id, HttpContext.GetUserId());

            if(!userOwnsItem)
            {
                return BadRequest(new { error = "You do not own this item" });
            }
            
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
        [HttpPatch(ApiRoutes.Items.PartialUpdate)]
        public async Task<ActionResult> PartialItemUpdate([FromRoute] Guid id, [FromBody] JsonPatchDocument<UpdateItemRequest> patchDocument)
        {
            var userOwnsItem = await _itemService.UserOwnsItemAsync(id, HttpContext.GetUserId());

            if(!userOwnsItem)
            {
                return BadRequest(new { error = "You do not own this item" });
            }

            var item = await _itemService.GetItemAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<UpdateItemRequest>(item);

            patchDocument.ApplyTo(itemToPatch, ModelState);

            _mapper.Map(itemToPatch, item);

            await _itemService.UpdateItemAsync(item);

            return NoContent();
        }

        // DELETE api/v1/items/{id}
        [HttpDelete(ApiRoutes.Items.Delete)]
        public async Task<ActionResult> DeleteItem([FromRoute] Guid id)
        {
            var userOwnsItem = await _itemService.UserOwnsItemAsync(id, HttpContext.GetUserId());

            if(!userOwnsItem)
            {
                return BadRequest(new { error = "You do not own this item" });
            }

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