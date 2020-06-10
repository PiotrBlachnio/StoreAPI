using AutoMapper;
using Store.Database.Models;
using Store.Contracts.Responses;
using Store.Contracts.Requests;

namespace Store.Profiles
{
    public class ItemsProfile: Profile
    {
        public ItemsProfile()
        {
            CreateMap<Item, ItemResponse>();
            CreateMap<CreateItemRequest, Item>();
            CreateMap<UpdateItemRequest, Item>();
            CreateMap<Item, UpdateItemRequest>();
        }
    }
}