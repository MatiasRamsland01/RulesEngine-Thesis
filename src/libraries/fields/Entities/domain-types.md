# Entities
We want to create a domain language where Types gives meaning to the end-user, therefor we create your own types.
And, I also gives benefits for us as developer, since
- We can incapsulate concepts such as 
## How to make one 
``` C#
  public class StringValue: StringField
  { }
  public class IntegerField: IField<int>
  {
    public int Value { get; set; }
    public IValidator<IField<int>> Validator { get; set; }
    bool IField<int>.IsValid()
    {
      throw new NotImplementedException();
    }
    public override string ToString()
    {
      return Value.ToString();
    }
  }
  public class IntergerValue : IntegerField
  {}
}
```
Note! The IValidator field. Normally we add a constructor to the base baseField, 
``` C#
 public EmployeeAllowedAge(int value,IValidator<IField<int>> validator)
{
    Value = value;
    Validator = validator;
}
```
Where the field Value will get initialized and we, or more important, the IoC (ServiceCollection) will give a suitable validator to the given Domain type
And The IntegerValues have to implement the base class like this:
``` C# 
public class EmployeeAllowedAge: IntegerField
  {
    public EmployeeAllowedAge(int value,IValidator<IField<int>> validator) : base(value,validator)
    {
    }
  }
```
The validator can look like this: 
``` C# 
public class EmployeeAllowedAgeValidator: AbstractValidator<IField<int>>
  {
    public EmployeeAllowedAgeValidator()
    {
      RuleFor(x => x.Value)
      .NotNull()
      .LessThanOrEqualTo(150)
      .GreaterThanOrEqualTo(10)
      .WithMessage("The {PropertyName} does not pass the rules for a Integer value, the field value was:{PropertyValue}");
    }
  }
```
And initialize a the domain type like this (Normally a IoC will inject the type(s) for us) 
``` C# 
EmployeeAllowedAge allowedAge = new EmployeeAllowedAge(20, new EmployeeAllowedAgeValidator());
```
