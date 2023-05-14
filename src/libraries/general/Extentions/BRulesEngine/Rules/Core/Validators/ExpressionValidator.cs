// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="ExpressionValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using FluentValidation;
namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class ExpressionValidator.
  /// Implements the <see cref="FluentValidation.AbstractValidator{IStringField}" />
  /// </summary>
  /// <seealso cref="FluentValidation.AbstractValidator{IStringField}" />
  public class ExpressionValidator : AbstractValidator<IStringField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionValidator" /> class.
    /// </summary>
    /// <param name="validator">The validator.</param>
    public ExpressionValidator(IValidator<IStringField> validator) {
      RuleFor(m => m).SetValidator(validator);
    }
  }
}
