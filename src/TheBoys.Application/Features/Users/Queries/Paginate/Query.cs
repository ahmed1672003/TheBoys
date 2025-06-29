namespace TheBoys.Application.Features.Users.Queries.Paginate;

public sealed record PaginateUsersQuery()
    : PaginateQuery,
        IRequest<PaginationResponse<List<PaginateUsersResult>>>;
