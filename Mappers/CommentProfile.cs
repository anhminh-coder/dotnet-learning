using AutoMapper;
using Project1.Dtos.Comment;
using Project1.Models;

namespace Project1.Mappers;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        // Read
        CreateMap<Comment, CommentDto>();
        
        // Create
        CreateMap<CreateCommentRequest, Comment>();
        
        // Update
        CreateMap<UpdateCommentRequest, Comment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}