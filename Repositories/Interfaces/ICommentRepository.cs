using Project1.Dtos.Comment;
using Project1.Models;

namespace Project1.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment comment);
    Task<Comment?> UpdateAsync(int id, UpdateCommentRequest req);
    Task<Comment?> DeleteAsync(int id);
}