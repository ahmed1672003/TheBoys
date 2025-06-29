namespace TheBoys.Shared.Abstractions;

public interface IUserContext
{
    (bool Exist, int Value) Id { get; }
    (bool Exist, string Value) Language { get; }
}
