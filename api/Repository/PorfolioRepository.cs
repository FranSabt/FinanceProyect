using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PorfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PorfolioRepository(ApplicationDBContext context) 
        {
            _context = context;
        }
        public async  Task<List<Stock>> GetAllUserPortfolioAsync(AppUser user)
        {
           return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
                .Select( stock => new Stock 
                    {
                        Id = stock.Stock.Id,
                        Symbol = stock.Stock.Symbol,
                        CompanyName = stock.Stock.CompanyName,
                        LastDiv = stock.Stock.LastDiv,
                        Money = stock.Stock.Money,
                        Industry = stock.Stock.Industry,
                        MarketCap = stock.Stock.MarketCap
                    }
                ).ToListAsync();
        }
    }
}