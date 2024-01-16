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
    }
}