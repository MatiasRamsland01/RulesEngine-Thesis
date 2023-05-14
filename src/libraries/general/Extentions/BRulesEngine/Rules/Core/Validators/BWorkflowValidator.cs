// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BWorkflowValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using fields.Messages;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  /// <summary>
  /// Class BWorkflowValidator.
  /// Implements the <see cref="AbstractValidator{BWorkflow}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{BWorkflow}" />
  public class BWorkflowValidator : AbstractValidator<IBWorkflow> {
    /// <summary>
    /// Initializes a new instance of the <see cref="BWorkflowValidator" /> class.
    /// </summary>
    public BWorkflowValidator(IValidator<IStringField> stringValidator, IValidator<IBRule> ruleValidator) {
      RuleFor(x => x.Rules)
      .Must(x => x.Count > 0)
      .ForEach(x => x.SetValidator(ruleValidator))
      .WithMessage(SystemMessages.Validation.CollectionEmpty);
      RuleFor(x => x.WorkflowName).SetValidator(stringValidator);

    }
  }
}
