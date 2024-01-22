﻿using FC.Codeflix.Catalog.Application.Exceptions;
using FC.Codeflix.Catalog.Domain.Entity;
using FC.Codeflix.Catalog.Domain.Repository;
using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.Codeflix.Catalog.Infra.Data.EF.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CodeflixCatalogDbContext _context;

    public CategoryRepository(CodeflixCatalogDbContext context)
    {
        _context = context;
    }

    public async Task Insert(Category aggregate, CancellationToken cancellationToken)
    {
        await _context.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Category> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FindAsync(
            new object[] { id }, 
            cancellationToken
        );

        if (category == null)
        {
            throw new NotFoundException($"Category '{id}' not found");
        }

        return category;
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


    public Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
