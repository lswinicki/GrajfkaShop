using Application.Requests.Queries.Products.Models;
using MediatR;

namespace Application.Requests.Queries.Products.GetAllProducts;

public class GetAllProductsQuery : IRequest<GetAllProductsResult>
{
    public int? CategoryId { get; set; }
    public string? SearchTerm { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
    public bool? ActiveOnly { get; set; } = true;
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

public class GetAllProductsResult
{
    public IEnumerable<ProductDto> Products { get; set; } = new List<ProductDto>();
    public int TotalCount { get; set; }
}
