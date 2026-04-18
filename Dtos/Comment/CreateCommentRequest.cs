using System.ComponentModel.DataAnnotations;

namespace Project1.Dtos.Comment;

public class CreateCommentRequest
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(1000)]
    public string Content { get; set; } = string.Empty;
    
    [Required]
    public int StockId { get; set; }
}
