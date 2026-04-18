using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dtos.Comment;
using Project1.Models;
using Project1.Repositories.Interfaces;

namespace Project1.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CommentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;
    
    public CommentsController(IMapper mapper, ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();
        var commentsDto = comments.Select(s => _mapper.Map<CommentDto>(s));
        
        return Ok(commentsDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null)
            return NotFound();
        
        return Ok(_mapper.Map<CommentDto>(comment));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest createCommentRequest)
    {
        // Validate that the stock exists
        var stockExists = await _stockRepository.IsExistAsync(createCommentRequest.StockId);
        if (!stockExists)
        {
            return BadRequest($"Stock with ID {createCommentRequest.StockId} does not exist.");
        }
        
        var comment = _mapper.Map<Comment>(createCommentRequest);
        var createdComment = await _commentRepository.CreateAsync(comment);
        
        return CreatedAtAction(
            nameof(GetById),
            new { id = createdComment.Id },
            _mapper.Map<CommentDto>(createdComment));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] UpdateCommentRequest updateCommentRequest, int id)
    {
        var updatedComment = await _commentRepository.UpdateAsync(id, updateCommentRequest);
        if (updatedComment == null)
        {
            return NotFound();
        }
        
        return Ok(_mapper.Map<CommentDto>(updatedComment));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedComment = await _commentRepository.DeleteAsync(id);
        if (deletedComment == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}