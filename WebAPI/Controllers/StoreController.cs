using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
        // Returns all the stores in the database
        [HttpGet]
        public ActionResult<List<Store>> GetStore()
        {
            List<Store> allStores = _sbl.GetAllStores();
            if (allStores.Count == 0)
            {
                return NoContent();
            }
            return Ok(allStores);
        }
        // GET api/<StoreController>/id
        // Returns a single store with the matching id
        [HttpGet("{id}")]
        public ActionResult<Store> GetStoreById(int id)
        {
            Store store = _sbl.GetStoreByID(id);
            if (store.ID == null)
            {
                return NoContent();
            }
            return Ok(store);
        }
        // POST api/<StoreController>
        // Add a store to the database
        [HttpPost]
        public ActionResult Post([FromBody] Store storeToAdd)
        {
            _sbl.AddStore(storeToAdd);
            return Created("Successfully added", storeToAdd);
        }

        // DELETE api/<StoreControllerr>/id
        // Deletes a store from the database
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Store store = _sbl.GetStoreByID(id);
            if (store.ID == null)
            {
                return NoContent();
            }
            else
            {
                _sbl.DeleteStore(id);
            }
            return Ok();
        }
    }
}
