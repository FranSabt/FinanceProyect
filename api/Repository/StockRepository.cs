using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository (ApplicationDBContext context) 
        {
            _context = context;
        }

        public async Task<bool> CheckStockAsync(int id)
        {
            var stock = await _context.Stocks.AnyAsync(s => s.Id == id);

            if (stock)
                return true;
            
            else 
                return false;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            return await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(query.Sortby))
            {
                if (query.Sortby.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.Descending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
                
                if (query.Sortby.Equals("CompanyName", StringComparison.OrdinalIgnoreCase))
                {
                    stocks = query.Descending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
                }
            }

            var skipNUmber = (query.PageNumber - 1) * query.PageZise;

            return await stocks.Skip(skipNUmber).Take(query.PageZise).ToListAsync();

        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync( x => x.Id == id);

            if (stock == null)
                return null;
            
            return stock;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(S => S.Symbol == symbol);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateRequestDto update)
        {
            var stockToUpdate = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);

            if (stockToUpdate == null) 
                return null;

            stockToUpdate.Symbol = update.Symbol;
            stockToUpdate.Industry = update.Industry;
            stockToUpdate.MarketCap = update.MarketCap;
            stockToUpdate.CompanyName = update.CompanyName;
            stockToUpdate.LastDiv = update.LastDiv;
            stockToUpdate.Money = update.Money;

             await _context.SaveChangesAsync();

            return stockToUpdate;

        }
    }     
}