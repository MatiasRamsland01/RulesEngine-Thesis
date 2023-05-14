// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="RuleDataField.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Validation;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using Prometheus;
namespace libraries.Extentions.BRulesEngine.Rules.Core.Fields {
  /// <summary>
  /// Interface IRuleDataField
  /// </summary>
  public interface IRuleDataField {
    #region Properties
    public Guid Id { get; set; }
    public StringField RuleName { get; set; }
    public StringField Expression { get; set; }
    public StringField Operator { get; set; }
    public StringField ErrorMessage { get; set; }
    public StringField SuccessEvent { get; set; }
    public BoolField Enabled { get; set; }
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public DateField CreatedAt { get; set; }
    /// <summary>
    /// Gets the modified at.
    /// </summary>
    /// <value>The modified at.</value>
    public DateField ModifiedAt { get; set; }
    /// <summary>
    /// Gets the author.
    /// </summary>
    /// <value>The author.</value>
    public StringField Author { get; set; }
    /// <summary>
    /// Gets the failure count.
    /// </summary>
    /// <value>The failure count.</value>
    public IntegerField FailureCount { get; set; }
    /// <summary>
    /// Gets the success count.
    /// </summary>
    /// <value>The success count.</value>
    public IntegerField SuccessCount { get; set; }
    #endregion
  }

  /// <summary>
  /// Class RuleDataField.
  /// Implements the <see cref="libraries.Extentions.BRulesEngine.Rules.Core.Fields.IRuleDataField" />
  /// </summary>
  /// <seealso cref="libraries.Extentions.BRulesEngine.Rules.Core.Fields.IRuleDataField" />
  public class RuleDataField : IRuleDataField, Validate {
    #region Fields
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IRuleDataField> _validator;
    #endregion
    #region Properties
    public Guid Id { get; set; }
    public StringField RuleName { get; set; }
    public StringField Expression { get; set; }
    public StringField Operator { get; set; }
    public StringField ErrorMessage { get; set; }
    public StringField SuccessEvent { get; set; }
    public BoolField Enabled { get; set; }
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public DateField CreatedAt { get; set; }
    /// <summary>
    /// Gets the modified at.
    /// </summary>
    /// <value>The modified at.</value>
    public DateField ModifiedAt { get; set; }
    /// <summary>
    /// Gets the author.
    /// </summary>
    /// <value>The author.</value>
    public StringField Author { get; set; }
    /// <summary>
    /// Gets the failure count.
    /// </summary>
    /// <value>The failure count.</value>
    public IntegerField FailureCount { get; set; }
    /// <summary>
    /// Gets the success count.
    /// </summary>
    /// <value>The success count.</value>
    public IntegerField SuccessCount { get; set; }
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleDataField" /> class.
    /// </summary>
    public RuleDataField(StringField ruleName, StringField expression, StringField @operator, StringField errorMessage, StringField successEvent, BoolField enabled, StringField author, IValidator<IRuleDataField>? validator = null) {
      _validator ??= RuleEngineFactory.RuleDataFieldValidator();
      Id = Guid.NewGuid();
      RuleName = ruleName;
      Expression = expression;
      Operator = @operator;
      ErrorMessage = errorMessage;
      SuccessEvent = successEvent;
      Enabled = enabled;
      Author = author;
      CreatedAt = FieldsFactory.DateField(DateTime.Now.ToString());
      FailureCount = FieldsFactory.IntegerField(0);
      SuccessCount = FieldsFactory.IntegerField(0);
      ModifiedAt = FieldsFactory.DateField(DateTime.Now.ToString());
      DoValidateAndThrow();
    }
    public RuleDataField() {
      _validator = RuleEngineFactory.RuleDataFieldValidator();
      Id = Guid.NewGuid();
      RuleName = FieldsFactory.StringField("RuleName not set");
      Expression = FieldsFactory.StringField("Expression not set");
      Operator = FieldsFactory.StringField("Operator not set");
      ErrorMessage = FieldsFactory.StringField("ErrorMessage not set");
      SuccessEvent = FieldsFactory.StringField("SuccessEvent not set");
      Enabled = FieldsFactory.BoolField(true);
      CreatedAt = FieldsFactory.DateField(DateTime.Now.ToString());
      FailureCount = FieldsFactory.IntegerField(0);
      SuccessCount = FieldsFactory.IntegerField(0);
      Author = FieldsFactory.StringField("Author not set");
      ModifiedAt = FieldsFactory.DateField(DateTime.Now.ToString());
    }
    #endregion
    public void IncrementSuccess() {
      SuccessCount.Value++;
    }
    public void IncrementFailure() {
      FailureCount.Value++;
    }
    #region Validation-Methods
    /// <summary>
    /// Does the validate.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidate() => _validator.Validate(this, options => options.IncludeAllRuleSets());
    /// <summary>
    /// Does the validate and throw.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidateAndThrow() => _validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    #endregion
  }
}
