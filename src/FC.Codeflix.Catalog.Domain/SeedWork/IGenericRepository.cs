﻿namespace FC.Codeflix.Catalog.Domain.SeedWork;
public interface IGenericRepository<TAgrregate> : IRepository
{
    public Task Insert(TAgrregate aggregate, CancellationToken cancellationToken);
    public Task<TAgrregate> Get(Guid id, CancellationToken cancellationToken); 
}
