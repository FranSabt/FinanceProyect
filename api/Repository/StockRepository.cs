using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
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

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync( x => x.Id == id);

            if (stock == null)
                return null;
            
            return stock;
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