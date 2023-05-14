// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="RuleDataFieldValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Fields;
using fields.Factories;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class RuleDataFieldValidator.
  /// Implements the <see cref="AbstractValidator{IRuleDataField}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{IRuleDataField}" />
  public class RuleDataFieldValidator : AbstractValidator<IRuleDataField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleDataFieldValidator" /> class.
    /// </summary>
    /// <param name="integerFieldValidator">The integer field validator.</param>
    /// <param name="datefieldValidator">The datefield validator.</param>
    /// <param name="stringfieldValidator">The stringfield validator.</param>
    /// <param name="idfieldValidator">The idfield validator.</param>
    /// <param name="boolfieldValidator"></param>
    public RuleDataFieldValidator(
      IValidator<IIntegerField> integerFieldValidator,
      IValidator<IDateField> datefieldValidator,
      IValidator<IStringField> stringfieldValidator,
      IValidator<IBoolField> boolfieldValidator) {
      RuleFor(x => x.Id)
      .NotEqual(Guid.Empty)
      .WithMessage("RuleId must be set and not an empty GUID");
      RuleFor(x => x.RuleName).SetValidator(stringfieldValidator);
      RuleFor(x => x.Operator).SetValidator(stringfieldValidator);
      RuleFor(x => x.ErrorMessage).SetValidator(stringfieldValidator);
      RuleFor(x => x.SuccessEvent).SetValidator(stringfieldValidator);
      RuleFor(x => x.Enabled).SetValidator(boolfieldValidator);
      RuleFor(x => x.CreatedAt).SetValidator(datefieldValidator);
      RuleFor(x => x.ModifiedAt).SetValidator(datefieldValidator);
      RuleFor(x => x.Author).SetValidator(stringfieldValidator);
      RuleFor(x => x.FailureCount).SetValidator(integerFieldValidator);
      RuleFor(x => x.SuccessCount).SetValidator(integerFieldValidator);
    }
  }
}
