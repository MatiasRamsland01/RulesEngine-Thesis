// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="InputDataField.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Validation;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Interface IInputDataField
  /// </summary>
  public interface IInputDataField {
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    IdField<Guid> Id { get; set; }
    /// <summary>
    /// Gets the updated at.
    /// </summary>
    /// <value>The updated at.</value>
    DateField UpdatedAt { get; set; }
    /// <summary>
    /// Gets the created at.
    /// </summary>
    /// <value>The created at.</value>
    DateField CreatedAt { get; set; }
    /// <summary>
    /// Gets the input description.
    /// </summary>
    /// <value>The input description.</value>
    StringField InputDescription { get; set; }
    /// <summary>
    /// Sets the description.
    /// </summary>
    /// <param name="description">The description.</param>
    public void SetDescription(StringField description);
    /// <summary>
    /// Sets the new updated date.
    /// </summary>
    /// <param name="datetime">The datetime.</param>
    public void SetNewUpdatedDate(DateField datetime);
  }
  /// <summary>
  /// Class InputDataField.
  /// Implements the <see cref="libraries.Extentions.BRulesEngine.Rules.Core.IInputDataField" />
  /// Implements the <see cref="Validate" />
  /// </summary>
  /// <seealso cref="libraries.Extentions.BRulesEngine.Rules.Core.IInputDataField" />
  /// <seealso cref="Validate" />
  public class InputDataField : IInputDataField, Validate {
    #region Fields
    /// <summary>
    /// The validator
    /// </summary>
    private IValidator<IInputDataField> validator = RuleEngineFactory.InputDataFieldValidator();
    #endregion
    #region Properties
    public IdField<Guid> Id { get; set; }
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    /// <summary>
    /// Gets the updated at.
    /// </summary>
    /// <value>The updated at.</value>
    public DateField UpdatedAt { get; set; }
    /// <summary>
    /// Gets the created at.
    /// </summary>
    /// <value>The created at.</value>
    public DateField CreatedAt { get; set; }
    /// <summary>
    /// Gets the input description.
    /// </summary>
    /// <value>The input description.</value>
    public StringField InputDescription { get; set; }
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="InputDataField" /> class.
    /// </summary>
    public InputDataField() {
      Id = FieldsFactory.IdField(Guid.NewGuid()); //Will need to find out what we do with Ids
      CreatedAt = FieldsFactory.DateField(DateTime.UtcNow.ToString());
      UpdatedAt = FieldsFactory.DateField(DateTime.UtcNow.ToString());
      InputDescription = FieldsFactory.StringField("Description not set");
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="InputDataField" /> class.
    /// </summary>
    /// <param name="idValidator">The identifier validator.</param>
    /// <param name="dateValidator">The date validator.</param>
    /// <param name="stringValidator">The string validator.</param>
    public InputDataField(IValidator<IIdField<Guid>> idValidator, IValidator<IDateField> dateValidator, IValidator<IStringField> stringValidator) {
      Id = FieldsFactory.IdField(Guid.NewGuid(), idValidator);
      CreatedAt = FieldsFactory.DateField(DateTime.UtcNow.ToString(), dateValidator);
      UpdatedAt = FieldsFactory.DateField(DateTime.UtcNow.ToString(), dateValidator);
      InputDescription = FieldsFactory.StringField("Description not set", stringValidator);
    }
    #endregion
    #region Set-Methods
    /// <summary>
    /// Sets the description.
    /// </summary>
    /// <param name="description">The description.</param>
    public void SetDescription(StringField description) {
      description.DoValidateAndThrow();
      InputDescription = description;
      DoValidateAndThrow();
    }
    /// <summary>
    /// Sets the new updated date.
    /// </summary>
    /// <param name="datetime">The datetime.</param>
    public void SetNewUpdatedDate(DateField datetime) {
      datetime.DoValidateAndThrow();
      UpdatedAt = datetime;
      DoValidateAndThrow();
    }
    #endregion
    #region Validation-Methods
    /// <summary>
    /// Does the validate.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidate() => validator.Validate(this, options => options.IncludeAllRuleSets());
    /// <summary>
    /// Does the validate and throw.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidateAndThrow() => validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());
    #endregion
  }
}
