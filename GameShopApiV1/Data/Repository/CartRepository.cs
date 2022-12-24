using GameShopApiV1.Models;
using GameShopApiV1.Models.DTOs.CartDto;
using Microsoft.EntityFrameworkCore;

namespace GameShopApiV1.Data.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly GameShopApiDbContext _shopApiDbContext;

        public CartRepository(GameShopApiDbContext shopApiDbContext)
        {
            _shopApiDbContext = shopApiDbContext;
        }

        public async Task<int> CreateCartAsync(CreateCartDto createCart)
        {
            var cart = new CartModel()
            {
                PlayerId = createCart.PlayerId,
                GameId = createCart.GameId
            };

            _shopApiDbContext.Carts.Add(cart);
            await _shopApiDbContext.SaveChangesAsync();
            return cart.Id;
        }

        public async Task<IEnumerable<DisplayCartDto>> DisplayCartAsync(string playerId)
        {
            var r = await _shopApiDbContext.Carts.ToListAsync();

            var x = r.FirstOrDefault();

            var cart = await _shopApiDbContext.Carts
                 .Include(x => x.Player).Include(x => x.Game)
                 .Where(x => x.Player.Id == x.PlayerId && x.Player.Id == playerId)
                 .Where(x => x.Game.Id == x.GameId)
                 .Select(x => new DisplayCartDto()
                 {
                     PlayerUserName = x.Player.UserName,
                     PlayerEmail = x.Player.Email,
                     GameName = x.Game.Name,
                     GamePrice = x.Game.Price,
                     GameDetails = x.Game.GameDetails
                 }).ToListAsync();

            return cart;
        }
    }
}
