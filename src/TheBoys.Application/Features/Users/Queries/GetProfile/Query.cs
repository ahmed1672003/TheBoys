namespace TheBoys.Application.Features.Users.Queries.GetProfile;

public sealed record GetUserProfileQuery() : IRequest<ResponseOf<GetUserProfileResult>>;
