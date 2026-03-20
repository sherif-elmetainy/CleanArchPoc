namespace CodeArt.Poc.Primitives.Tests;

public class PersonNameTest
{
    [Fact]
    public void Should_Accept_English_Names()
    {
        var result = PersonName.TryFrom("John");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_English_Name_With_Space()
    {
        var result = PersonName.TryFrom("John Doe");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_English_Name_With_SingleQuote()
    {
        var result = PersonName.TryFrom("O'Connor");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_English_Name_With_Dash()
    {
        var result = PersonName.TryFrom("O-Connor");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_Arabic_Name()
    {
        var result = PersonName.TryFrom("شريف");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_MaxLengthName()
    {
        var result = PersonName.TryFrom(new string('a', PersonName.MaxLength));
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_MaxLengthNameWithExtraSpaces()
    {
        var result = PersonName.TryFrom(" " + new string('a', PersonName.MaxLength) + " ");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Accept_Russian_Name()
    {
        var result = PersonName.TryFrom("Владимир");
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public void Should_Trim_Spaces()
    {
        var result = PersonName.TryFrom(" John Doe  ");
        Assert.True(result.IsSuccess);
        Assert.Equal("John Doe", result.ValueObject.Value);
    }
    
    [Fact]
    public void Should_Reject_Empty()
    {
        var result = PersonName.TryFrom("");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameCannotBeEmpty);
    }
    
    [Fact]
    public void Should_Reject_Whitespace()
    {
        var result = PersonName.TryFrom(" ");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameCannotBeEmpty);
    }
    
    [Fact]
    public void Should_Reject_TooLong()
    {
        var result = PersonName.TryFrom(new string('a', PersonName.MaxLength + 1));
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, string.Format(ValidationErrors.PersonNameTooLong, PersonName.MaxLength));
    }
    
    [Fact]
    public void Should_Reject_Invalid()
    {
        var result = PersonName.TryFrom("?");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_Numbers()
    {
        var result = PersonName.TryFrom("a1");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_InitialDash()
    {
        var result = PersonName.TryFrom("-john");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_FinalDash()
    {
        var result = PersonName.TryFrom("john-");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_InitialSingleQuote()
    {
        var result = PersonName.TryFrom("'john");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_FinalSingleQuote()
    {
        var result = PersonName.TryFrom("john'");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_DoubleDash()
    {
        var result = PersonName.TryFrom("john--doe");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
    
    [Fact]
    public void Should_Reject_DoubleSingleQuote()
    {
        var result = PersonName.TryFrom("john''doe");
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error.ErrorMessage, ValidationErrors.PersonNameInvalid);
    }
}