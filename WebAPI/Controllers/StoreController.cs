using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using BL;
using DL;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : Controller
    {
        private ISBL _sbl;
        public StoreController(ISBL sbl)
        {
            _sbl = sbl;
        }
        // GET: api/<StoreController>
        [HttpGet]
        public List<Store> Get()
        {
            return _sbl.GetAllStores();
        }

    }
}
