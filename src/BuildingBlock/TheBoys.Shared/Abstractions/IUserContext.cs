namespace TheBoys.Shared.Abstractions;

public interface IUserContext
{
    (bool Exist, Guid Value) Id { get; }
}
