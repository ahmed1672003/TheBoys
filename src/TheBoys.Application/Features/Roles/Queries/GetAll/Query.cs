namespace TheBoys.Application.Features.Roles.Queries.GetAll;

public sealed record GetAllRolesQuery() : IRequest<ResponseOf<List<GetAllRolesResult>>>;
