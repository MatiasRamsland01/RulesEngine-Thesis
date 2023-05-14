// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="InputDataFieldValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using FluentValidation;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class RuleDataFieldValidator.
  /// Implements the <see cref="AbstractValidator{IRuleDataField}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{IRuleDataField}" />
  public class InputDataFieldValidator : AbstractValidator<IInputDataField> {
    /// <summary>
    /// Initializes a new instance of the <see cref="RuleDataFieldValidator" /> class.
    /// </summary>
    /// <param name="integerFieldValidator">The integer field validator.</param>
    /// <param name="datefieldValidator">The datefield validator.</param>
    /// <param name="stringfieldValidator">The stringfield validator.</param>
    /// <param name="idfieldValidator">The idfield validator.</param>
    public InputDataFieldValidator(IValidator<IIntegerField> integerFieldValidator, IValidator<IDateField> datefieldValidator, IValidator<IStringField> stringfieldValidator, IValidator<IIdField<Guid>> idfieldValidator) {
      RuleFor(x => x.Id).SetValidator(idfieldValidator);
      RuleFor(x => x.CreatedAt).SetValidator(datefieldValidator);
      RuleFor(x => x.UpdatedAt).SetValidator(datefieldValidator);
      RuleFor(x => x.InputDescription).SetValidator(stringfieldValidator);
    }
  }
}
