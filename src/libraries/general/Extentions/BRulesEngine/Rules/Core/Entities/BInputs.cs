// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BInputs.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using System.Text.Json.Serialization;
using fields.Entities.Base.Fields;
using fields.Entities.Base.Validation;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.BRulesEngine.Rules.Core.Validators;

namespace libraries.Extentions.BRulesEngine.Rules.Core {

  /// <summary>
  /// Class BInputs.
  /// Implements the <see cref="libraries.Extentions.BRulesEngine.Rules.Core.IBInputs" />
  /// Implements the <see cref="Validate" />
  /// </summary>
  /// <seealso cref="libraries.Extentions.BRulesEngine.Rules.Core.IBInputs" />
  /// <seealso cref="Validate" />
  public class BInputs : IBInputs, Validate {
    #region Fields
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IBInputs> validator = RuleEngineFactory.BInputsValidator();
    #endregion
    #region Properties
    public Guid Id { get; set; }
    /// <summary>
    /// Gets the inputs.
    /// </summary>
    /// <value>The inputs.</value>
    public dynamic Inputs { get; set; }
    /// <summary>
    /// Gets or sets the data field.
    /// </summary>
    /// <value>The data field.</value>
    public InputDataField DataField { get; set; }
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BInputs" /> class.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="inputValidator">The input validator.</param>
    public BInputs(dynamic input, IValidator<IBInputs>? inputValidator = null) {
      if (inputValidator != null) { validator = inputValidator; }
      Id = Guid.NewGuid();
      Inputs = input;
      DataField = new InputDataField();
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
