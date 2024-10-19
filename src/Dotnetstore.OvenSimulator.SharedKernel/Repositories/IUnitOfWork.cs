using Dotnetstore.OvenSimulator.SharedKernel.Models;

namespace Dotnetstore.OvenSimulator.SharedKernel.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;

    Task SaveChangesAsync(CancellationToken cancellationToken);

    void Rollback();
}