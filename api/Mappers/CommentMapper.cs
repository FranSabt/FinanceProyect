using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
namespace api.models
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                StockId = commentModel.StockId,
                Title = commentModel.Title,
                Content = commentModel.Content,
                Date = commentModel.Date,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockID)
        {
            return new Comment
            {
                StockId = stockID,
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }


        // public static Comment ToCommentFromCreateDto(this CommentDto commentDto)
        // {
        //     StockId = commentModel.StockId,
        //     Title = commentModel.Title,
        //     Content = commentModel.Content,
        //     Date = commentModel.Date,
        // }
    }
}