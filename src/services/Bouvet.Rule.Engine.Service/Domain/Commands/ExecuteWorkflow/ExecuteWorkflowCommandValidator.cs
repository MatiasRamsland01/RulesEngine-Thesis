// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="ExecuteWorkflowHandlerValidator.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;

namespace Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow {
  /// <summary>
  /// Class ExecuteWorkflowCommandValidator.
  /// Implements the <see cref="AbstractValidator{ExecuteWorkflowCommand}" />
  /// </summary>
  /// <seealso cref="AbstractValidator{ExecuteWorkflowCommand}" />
  public class ExecuteWorkflowCommandValidator : AbstractValidator<ExecuteWorkflowCommand> {
    /// <summary>
    /// Initializes a new instance of the <see cref="ExecuteWorkflowCommandValidator"/> class.
    /// </summary>
    public ExecuteWorkflowCommandValidator(IValidator<IBWorkflow> workflowValidator, IValidator<IBInputs> inputValidator, IValidator<IIdField<Guid>> idValidator) {
      RuleFor(x => x.workflowId).SetValidator(idValidator);
      RuleFor(x => x.workflow).SetValidator(workflowValidator);
      RuleFor(x => x.input).SetValidator(inputValidator);
    }
  }
}
