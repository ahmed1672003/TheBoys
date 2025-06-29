namespace TheBoys.Application.Features.Users.Queries.Paginate;

public sealed record PaginateUsersQuery(int? RoleId)
    : PaginateQuery,
        IRequest<PaginationResponse<List<PaginateUsersResult>>>;
