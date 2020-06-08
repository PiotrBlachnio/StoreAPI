using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Store.Data;

namespace Items.Controllers {

    [Route("/api/items")]
    [ApiController]
    public class ItemsController: ControllerBase {
        private readonly IStoreRepo _repository;
        private readonly IMapper _mapper;

        public ItemsController(IStoreRepo repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }
    }

    [HttpGet]
    public ActionResult <IEnumerable
}