// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BInputsValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Messages;
using FluentValidation;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class BInputsValidator.
  /// Implements the <see cref="AbstractValidator{IBInputs}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{IBInputs}" />
  public class BInputsValidator : AbstractValidator<IBInputs> {
    /// <summary>
    /// Initializes a new instance of the <see cref="BInputsValidator" /> class.
    /// </summary>
    public BInputsValidator(IValidator<IInputDataField> dataFieldValidator) {
      RuleFor(x => x.DataField).SetValidator(dataFieldValidator);
    }
  }
}
