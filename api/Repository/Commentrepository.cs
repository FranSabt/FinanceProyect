using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class Commentrepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public Commentrepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Comment>> GetAllSync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment> CreateCommentAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<Comment?> DeleteCommentAync(int id)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Comment?> UpdateCommentAsync(int id, UpdateCommentDto commentUpdate)
        {
           var result = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

           if (result == null)
            return null;

            result.Title = !string.IsNullOrEmpty(commentUpdate.Title) ? commentUpdate.Title : result.Title;
            result.Content = !string.IsNullOrEmpty(commentUpdate.Content) ? commentUpdate.Content : result.Content;

            await _context.SaveChangesAsync();

            return result;

        }
    }
}