using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Store.Data;
using System.Collections.Generic;
using Store.Dtos;
using Store.Models;

namespace Items.Controllers
{

    [Route("/api/items")]
    [ApiController]
    public class ItemsController: ControllerBase 
    {
        private readonly IStoreRepo _repository;
        private readonly IMapper _mapper;

        public ItemsController(IStoreRepo repository, IMapper mapper) 
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
    }
}