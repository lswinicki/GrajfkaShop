using MediatR;

namespace Application.Requests.Queries.Admin.GetDashboardStats;

public class GetDashboardStatsQuery : IRequest<DashboardStatsDto>
{
}

public class DashboardStatsDto
{
    public int TotalProducts { get; set; }
    public int TotalOrders { get; set; }
    public int TotalCategories { get; set; }
    public int UnreadMessages { get; set; }
    public decimal TotalRevenue { get; set; }
    public int NewOrdersCount { get; set; }
}
