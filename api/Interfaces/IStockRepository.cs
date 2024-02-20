using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helpers;
using api.models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateRequestDto updateStock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> CheckStockAsync(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
    }
}