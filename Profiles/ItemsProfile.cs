using AutoMapper;
using Store.Dtos;
using Store.Database.Models;

namespace Store.Profiles
{
    public class ItemsProfile: Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemReadDto>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<ItemUpdateDto, Item>();
            CreateMap<Item, ItemUpdateDto>();
        }
    }
}