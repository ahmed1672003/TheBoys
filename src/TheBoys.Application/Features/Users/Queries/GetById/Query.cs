namespace TheBoys.Application.Features.Users.Queries.GetById;

public sealed record GetUserByIdQuery(int Id) : IRequest<ResponseOf<GetUserByIdResult>>;
