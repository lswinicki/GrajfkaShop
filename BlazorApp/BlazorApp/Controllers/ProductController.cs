using Application.Requests.Commands.Products.Add;
using Application.Requests.Commands.Products.Delete;
using Application.Requests.Commands.Products.Update;
using Application.Requests.Commands.ProductImage.Add;
using Application.Requests.Commands.ProductImage.Delete;
using Application.Requests.Queries.Products.GetAllProducts;
using Application.Requests.Queries.Products.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public ProductController(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllProductsResult>> GetAll(
        [FromQuery] int? categoryId,
        [FromQuery] string? searchTerm,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize,
        [FromQuery] bool? activeOnly = true)
    {
        var query = new GetAllProductsQuery
        {
            CategoryId = categoryId,
            SearchTerm = searchTerm,
            PageNumber = pageNumber,
            PageSize = pageSize,
            ActiveOnly = activeOnly
        };
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Add([FromBody] AddProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Update([FromBody] UpdateProductCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        return await _mediator.Send(new DeleteProductCommand { Id = id });
    }

    [HttpPost("{id}/images/upload")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<string>> UploadImage(int id, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Nie przesłano pliku.");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "products");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var imageUrl = $"/uploads/products/{uniqueFileName}";

        await _mediator.Send(new AddProductImageCommand
        {
            ProductId = id,
            ImageUrl = imageUrl
        });

        return Ok(imageUrl);
    }

    [HttpDelete("images/{imageId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<int>> DeleteImage(int imageId)
    {
        return await _mediator.Send(new DeleteProductImageCommand { Id = imageId });
    }
}
