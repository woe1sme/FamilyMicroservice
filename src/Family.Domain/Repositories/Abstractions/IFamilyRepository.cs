using System.Collections.Generic;
using Family.Domain;
using Family.Domain.Entities;

namespace Family.Domain.Repositories.Abstractions;

public interface IFamilyRepository : IRepository<Entities.Family, Guid>
{

}