using System.Linq.Expressions;
using Application.Common.Interfaces.Repositories;
using Application.Requests.Queries.Products.Models;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Requests.Queries.Products.GetAllProducts;

public class GetAllProductsHandler(IProductRepository productRepository)
    : IRequestHandler<GetAllProductsQuery, GetAllProductsResult>
{
    public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        // Build a composite filter
        Expression<Func<ProductDao, bool>>? filter = null;

        var conditions = new List<Expression<Func<ProductDao, bool>>>();

        if (request.ActiveOnly == true)
            conditions.Add(p => p.IsActive);

        if (request.CategoryId.HasValue)
        {
            var categoryId = request.CategoryId.Value;
            conditions.Add(p => p.CategoryId == categoryId);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            conditions.Add(p => p.Name.ToLower().Contains(searchTerm)
                              || (p.Description != null && p.Description.ToLower().Contains(searchTerm)));
        }

        if (request.MinPrice.HasValue)
        {
            var minPrice = request.MinPrice.Value;
            conditions.Add(p => p.Price >= minPrice);
        }

        if (request.MaxPrice.HasValue)
        {
            var maxPrice = request.MaxPrice.Value;
            conditions.Add(p => p.Price <= maxPrice);
        }

        // Combine all conditions with AND
        if (conditions.Any())
        {
            filter = conditions.First();
            foreach (var condition in conditions.Skip(1))
            {
                filter = CombineAnd(filter, condition);
            }
        }

        var totalCount = await productRepository.CountAsync(filter, cancellationToken);

        var products = await productRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            filter: filter,
            include: q => q.Include(p => p.Images).Include(p => p.Category),
            orderBy: p => p.Id,
            orderByDescending: true,
            cancellationToken: cancellationToken
        );

        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = p.Category?.Name ?? "",
            IsActive = p.IsActive,
            ImageUrls = p.Images?.Select(i => i.ImageUrl).ToList() ?? new List<string>()
        });

        return new GetAllProductsResult
        {
            Products = productDtos,
            TotalCount = totalCount
        };
    }

    private static Expression<Func<T, bool>> CombineAnd<T>(
        Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var parameter = Expression.Parameter(typeof(T));
        var combined = Expression.AndAlso(
            Expression.Invoke(left, parameter),
            Expression.Invoke(right, parameter));
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}
