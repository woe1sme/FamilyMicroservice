using Family.Domain.Entities.Base;

namespace Family.Infrastructure.Repositories.Implementations.Exceptions;

public class EntityNotFoundException(long id)
    : ArgumentException($"No entity with such id {id} was found.")
{

}