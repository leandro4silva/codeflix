﻿using FluentAssertions;
using FC.Codeflix.Catalog.Infra.Data.EF;
using Repository = FC.Codeflix.Catalog.Infra.Data.EF.Repositories;
using FC.Codeflix.Catalog.Application.Exceptions;

namespace FC.Codeflix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[Collection(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTest
{
    private readonly CategoryRepositoryTestFixture _fixture;

    public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact(DisplayName = nameof(Insert))]
    [Trait("Integration/Infra.Data.EF", "CategoryRepository - Repositories")]
    public async Task Insert()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        
        await categoryRepository.Insert(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext()).Categories.FindAsync(exampleCategory.Id);

        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(exampleCategory.Name);
        dbCategory.Description.Should().Be(exampleCategory.Description);
        dbCategory.IsActive.Should().Be(exampleCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }


    [Fact(DisplayName = nameof(Get))]
    [Trait("Integration/Infra.Data.EF", "CategoryRepository - Repositories")]
    public async Task Get()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var exampleCategoryList = _fixture.GetExampleCategoriesList();
        exampleCategoryList.Add(exampleCategory);
        
        await dbContext.AddRangeAsync(exampleCategoryList);
        await dbContext.SaveChangesAsync();

        var categoryRepository = 
            new Repository.CategoryRepository(_fixture.CreateDbContext());

        var dbCategory = await categoryRepository.Get(exampleCategory.Id, CancellationToken.None);

        dbCategory.Should().NotBeNull();
        dbCategory.Id.Should().Be(exampleCategory.Id);
        dbCategory!.Name.Should().Be(exampleCategory.Name);
        dbCategory.Description.Should().Be(exampleCategory.Description);
        dbCategory.IsActive.Should().Be(exampleCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }


    [Fact(DisplayName = nameof(GetThrowIfNotFound))]
    [Trait("Integration/Infra.Data.EF", "CategoryRepository - Repositories")]
    public async Task GetThrowIfNotFound()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleId = Guid.NewGuid();

        await dbContext.AddRangeAsync(_fixture.GetExampleCategoriesList());
        await dbContext.SaveChangesAsync();

        var categoryRepository =
            new Repository.CategoryRepository(_fixture.CreateDbContext());

        var task = async () => await categoryRepository
            .Get(exampleId, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{exampleId}' not found");
    }


    [Fact(DisplayName = nameof(Update))]
    [Trait("Integration/Infra.Data.EF", "CategoryRepository - Repositories")]
    public async Task Update()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();

        var newCategoryValues = _fixture.GetExampleCategory();

        exampleCategory.Update(
            newCategoryValues.Name, 
            newCategoryValues.Description
        );

        await dbContext.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        
        var categoryRepository = 
            new Repository.CategoryRepository(dbContext);

        await categoryRepository.Update(
            exampleCategory, 
            CancellationToken.None
        );

        var dbCategory = await (_fixture.CreateDbContext())
            .Categories.FindAsync(
                exampleCategory.Id,
                CancellationToken.None
            );

        dbCategory.Should().NotBeNull();
        dbCategory!.Id.Should().Be(exampleCategory.Id);
        dbCategory.Name.Should().Be(exampleCategory.Name);
        dbCategory.Description.Should().Be(exampleCategory.Description);
        dbCategory.IsActive.Should().Be(exampleCategory.IsActive);
        dbCategory.CreatedAt.Should().Be(exampleCategory.CreatedAt);
    }

    [Fact(DisplayName = nameof(Delete))]
    [Trait("Integration/Infra.Data.EF", "CategoryRepository - Repositories")]
    public async Task Delete()
    {
        CodeflixCatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();

        await dbContext.AddAsync(exampleCategory);
        await dbContext.SaveChangesAsync();

        var categoryRepository = new Repository.CategoryRepository(dbContext);

        await categoryRepository.Delete(
            exampleCategory, 
            CancellationToken.None
        );
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext())
            .Categories.FindAsync(
                exampleCategory.Id,
                CancellationToken.None
            );

        dbCategory.Should().BeNull();    
    }
}
