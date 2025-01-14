namespace Family.Infrastructure.Repositories.Implementations.Exceptions;

public class FamilyNotFoundException(long id)
    : ArgumentException($"No Family with such id {id} was found.")
{
    public long Id { get; } = id;
}