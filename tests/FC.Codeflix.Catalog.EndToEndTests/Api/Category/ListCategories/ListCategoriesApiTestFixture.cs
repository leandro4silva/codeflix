﻿using FC.Codeflix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.ListCategories;

public class ListCategoriesApiTestFixture : CategoryBaseFixture
{
    public List<DomainEntity.Category> GetExampleCategoriesListWithName(List<string> name)
    {
        return name.Select(name =>
        {
            var category = GetExampleCategory();
            category.Update(name, category.Description);
            return category;
        }).ToList();
    }

    public List<DomainEntity.Category> CloneCategoriesListOrdered(
        List<DomainEntity.Category> categoriesList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<DomainEntity.Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id)
        };

        return orderedEnumerable.ToList();
    }
}

[CollectionDefinition(nameof(ListCategoriesApiTestFixture))]
public class ListCategoriesApiTestFixtureColletion : ICollectionFixture<ListCategoriesApiTestFixture> { }

