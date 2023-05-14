// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BRuleEngineValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Messages;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class BRuleEngineValidator.
  /// Implements the <see cref="AbstractValidator{BRuleEngine}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{BRuleEngine}" />
  public class BRuleEngineValidator : AbstractValidator<BRuleEngine> {
    /// <summary>
    /// Initializes a new instance of the <see cref="BRuleEngineValidator" /> class.
    /// </summary>
    public BRuleEngineValidator() {
      RuleFor(x => x.rulesengine)
      .NotEmpty()
      .NotNull()
      .WithMessage(SystemMessages.Validation.NullOrEmptyMessage);
    }
  }
}
