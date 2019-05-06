using System.Threading.Tasks;
using dapper_rest_api.Data.Dapper;
using dapper_rest_api.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace dapper_rest_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductsRepository _productHelper;
        public ProductController(ProductsRepository productHelper)
        {
            _productHelper = productHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productHelper.GetAll());
        }

        [HttpGet("/GetByCategoryId({categoryId})")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            return Ok(await _productHelper.GetByCategoryId(categoryId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _productHelper.GetById(id));
        }

        [HttpGet("getbypagination({page},{take})")]
        public async Task<IActionResult> GetByPagination(int page = 1, int take = 10)
        {
            return Ok(await _productHelper.ToPaginationList(page, take));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Products model)
        {
            bool result = await _productHelper.Insert(model);
            if (result)
            {
                return Ok(new { Message = "Kayıt işlemi başarılı :)" });
            }

            return BadRequest(new { Message = "Kayıt işlemi başarısız :(" });
        }

        [HttpPut("{id}/{name}")]
        public async Task<IActionResult> Put(int id, string name)
        {
            bool result = await _productHelper.UpdateName(id, name);
            if (result)
            {
                return Ok(new { Message = "Güncelleme işlemi başarılı :)" });
            }

            return BadRequest(new { Message = "Güncelleme işlemi başarısız :(" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _productHelper.Delete(id);
            if (result)
            {
                return Ok(new { Message = "Silme işlemi başarılı :)" });
            }

            return BadRequest(new { Message = "Silme işlemi başarısız :(" });
        }
    }
}