using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext context, IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult>  GetAll()
        {
            var stock = await _stockRepository.GetAllAsync();

            var stockResult = stock.Select(s => s.ToStockDto());

            return Ok(stockResult);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult>  GetById([FromRoute] int id)
        {
            var stock = await _stockRepository.GetByIdAsync(id);

            if (stock == null)
                return NotFound();

            return Ok(stock.ToStockDto());
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stock =  stockDto.ToStockFromCreateDTO();
            var stockModel = await _stockRepository.CreateAsync(stock);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRequestDto update)
        {
            var stockToUpdate = await _stockRepository.DeleteAsync(id);

            if (stockToUpdate == null) 
                return NotFound();

            return Ok(stockToUpdate.ToStockDto());
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockToDelete = await _context.Stocks.FirstOrDefaultAsync( x => x.Id == id);

            if (stockToDelete == null)
                return NotFound();

            return NoContent();
        }
    }
}