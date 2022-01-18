using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Models;
using BL;
using DL;
using CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private ISBL _sbl;
        public ProductController(ISBL sbl)
        {
            _sbl = sbl;
        }
        // GET: api/<ProductController>
        // Gets all products of a specified store
        [HttpGet]
        public ActionResult<List<Product>> Get(int storeID)
        {
            List<Product> allProducts = _sbl.GetAllProducts(storeID);
            if (allProducts.Count == 0)
            {
                return NoContent();
            }
            return Ok(allProducts);
        }

        // GET api/<ProductController>/id
        // Gets a selected product and its details by ID
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            Product selectedProduct = _sbl.GetProductByID(id);
            if (selectedProduct.ID == null)
            {
                return NoContent();
            }
            return Ok(selectedProduct);
        }

        // POST api/<ProductController>
        // Adds a product to the selected store
        [Authorize]
        [HttpPost]
        public ActionResult Post(int storeID, [FromBody] Product productToAdd)
        {
            try
            {
                _sbl.AddProduct(storeID, productToAdd);
                return Created("Successfully added", productToAdd);
            }
            catch (InputInvalidException ex)
            {
                return Conflict(ex.Message);
            }

        }

        // PUT api/<ProductController>/5
        // Edits a current product and saves to the database
        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product prodToEdit)
        {
            try
            {
                _sbl.EditProduct(id, prodToEdit);
                return Created("Successfully updated", prodToEdit);
            }
            catch (InputInvalidException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        // Delets a selected product from the database
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Product selectedProduct = _sbl.GetProductByID(id);
            if (selectedProduct.ID == null)
            {
                return NoContent();
            }
            _sbl.DeleteProduct(id);
            return Ok("Successfully deleted a product");
        }
    }
}
