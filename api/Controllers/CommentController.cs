using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;

        public CommentController(ApplicationDBContext context, ICommentRepository repository, IStockRepository stockRepository)
        {
            _context = context;
            _commentRepository = repository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllComments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _commentRepository.GetAllSync();
            var commentsFinds = comments.Select(x => x.ToCommentDto());
            return Ok(commentsFinds);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null) 
                return NotFound();

            return Ok(comment.ToCommentDto());


        }

        [HttpPost]
        [Route("{stockId:int}")]
        public async Task<IActionResult> PostComment([FromRoute] int stockId ,[FromBody] CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool exits = await  _stockRepository.CheckStockAsync(stockId);

            if (!exits)
                return BadRequest();

            var commentModel = commentDto.ToCommentFromCreate(stockId);
            var comment = _commentRepository.CreateCommentAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new {id = commentModel.Id}, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{commentID:int}")]
        public async Task<IActionResult> PostComment([FromRoute] int commentID ,[FromBody] UpdateCommentDto commentUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await  _commentRepository.UpdateCommentAsync(commentID, commentUpdate);

            if (comment == null)
                return BadRequest();

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{commentId:int}")]

        public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteCommentAync(commentId);

            if (comment == null)
                return NotFound();

            return  NoContent();
            
        }
        
    }
}