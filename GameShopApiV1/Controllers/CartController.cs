using GameShopApiV1.Data.Repository;
using GameShopApiV1.Models.DTOs.CartDto;
using Microsoft.AspNetCore.Mvc;

namespace GameShopApiV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCartAsync([FromBody] CreateCartDto createCart)
        {
            if (ModelState.IsValid)
            {
                var createdCartrId = await _cartRepository.CreateCartAsync(createCart);
                return StatusCode(201, createdCartrId);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("SearchedCarts/{value}")]
        public async Task<IActionResult> DisplayCartItemsAsync(string value)
        {
            var carts = await _cartRepository.DisplayCartAsync(value);
            if (carts.Count() == 0)
            {
                return NotFound("No cart found");
            }
            return Ok(carts);
        }
    }
}
