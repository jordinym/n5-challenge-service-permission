using FluentValidation.Results;

namespace N5Permission.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void Return_EmptyErrorDictionary_When_ConstructorIsDefault()
    {
        var actual = new ValidationException().Errors;

        //Assert
        Assert.Equal(actual.Keys, Array.Empty<string>());
    }

    [Fact]
    public void Return_SingleElementErrorDictionary_When_SingleValidationFailure()
    {
        string propertyName = "EmployeeForename";
        string errorMessage = "This field required";
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure(propertyName, errorMessage),
            };

        var actual = new ValidationException(failures).Errors;

        //Assert
        Assert.Equal(actual.Keys, new string[] { propertyName });
        Assert.Equal(actual[propertyName], new string[] { errorMessage });
    }

    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        string propertyNameOne = "EmployeeForename";
        string propertyNameTwo = "EmployeeSurname";
        string errorMessageOne = "This EmployeeForename field required";
        string errorMessageTwo = "This EmployeeSurname field required ";
        var failures = new List<ValidationFailure>
            {
                new ValidationFailure(propertyNameOne, errorMessageOne),
                new ValidationFailure(propertyNameOne, errorMessageTwo),
                new ValidationFailure(propertyNameTwo, errorMessageTwo)
            };

        var actual = new ValidationException(failures).Errors;

        //Assert
        Assert.Equal(actual.Keys, new string[] { propertyNameOne, propertyNameTwo });
        Assert.Equal(actual[propertyNameOne], new string[]
        {errorMessageOne,  errorMessageTwo  });
    }
}
