﻿using FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.Category.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 5)
    {
        var fixture = new UpdateCategoryTestFixture();
        for (int index = 0; index < times; index++)
        {
            var exampleCategory = fixture.GetExampleCategory();
            var exampleInput = fixture.GetValidInput(exampleCategory.Id);

            yield return new object[]
            {
                exampleCategory,
                exampleInput
            };
        }
    }

    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new UpdateCategoryTestFixture();
        var invalidInputList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    //nome não pode ser menor que 3 caracteres
                    invalidInputList.Add(new object[] {
                            fixture.GetInvalidInputShortName(),
                            "Name should not be less than 3 characters long"
                        });
                    break;
                case 1:
                    //nome não pode ser maior do que 255 caracteres
                    invalidInputList.Add(new object[] {
                            fixture.GetInvalidInputTooLongName(),
                            "Name should not be greater than 255 characters long"
                        });
                    break;
                case 2:
                    //description ser maior do que 10.000 caracteres
                    invalidInputList.Add(new object[] {
                            fixture.GetInvalidInputDescriptionTooLong(),
                            "Description should not be greater than 10000 characters long"
                        });
                    break;
                default: break;
            }
        }

        return invalidInputList;
    }
}
