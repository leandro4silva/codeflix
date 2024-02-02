﻿using Bogus;
using FC.Codeflix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;


namespace FC.Codeflix.Catalog.EndToEndTests.Common;

public class BaseFixture
{
    protected Faker Faker {  get; set; }
    public ApiClient ApiClient { get; set; }
    public HttpClient HttpClient { get; set; }
    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }

    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
    }

    public CodeflixCatalogDbContext CreateDbContext()
    {
        var context = new CodeflixCatalogDbContext(
            new DbContextOptionsBuilder<CodeflixCatalogDbContext>()
            .UseInMemoryDatabase("e2e-tests-db")
            .Options
        );

        return context;
    }

    
}
