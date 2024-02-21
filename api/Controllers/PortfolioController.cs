using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extension;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
                return BadRequest();

            var userPortfolio = await _portfolioRepository.GetAllUserPortfolioAsync(appUser);

            return Ok(userPortfolio);


        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostPortfolio(string symbol)
        {
            var username = User.GetUserName();

            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
                return BadRequest("No user");

            var stock =  await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null)
                return BadRequest("No stock with that symbol");

            var userPortfolio = await _portfolioRepository.GetAllUserPortfolioAsync(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower()))
                return BadRequest($"The symbol '{symbol}' is already in use");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
                AppUser = appUser,
                Stock = stock,
            };

            var portfolio = await _portfolioRepository.CreateAsync(portfolioModel);

            if (portfolio == null)
                return BadRequest("Portfolio not create");

            return Ok(portfolio);

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(int id)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);

            if (appUser == null)
                return BadRequest();
            
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
                return BadRequest("Stock don't exist");

            var portfolio = await _portfolioRepository.DeletePortfolioAsync(stock.Id, appUser.Id);

            if (portfolio == null)
                BadRequest("Portfolio dont exist");
            
            return NoContent();
        }
    }
}