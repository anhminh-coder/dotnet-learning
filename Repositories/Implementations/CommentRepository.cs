using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Dtos.Comment;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Repositories.Implementations;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    
    public CommentRepository(ApplicationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }
    
    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }
    
    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequest req)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
            return null;
        
        _mapper.Map(req, comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return null;
        }
        
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}