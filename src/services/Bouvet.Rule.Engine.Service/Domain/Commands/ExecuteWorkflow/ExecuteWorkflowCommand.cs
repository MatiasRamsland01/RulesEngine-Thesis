// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-10-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-10-2023
// ***********************************************************************
// <copyright file="ExecuteWorkflowCommand.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using libraries.Contracts.Bouvet.Rule.Engine.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;

namespace Bouvet.Rule.Engine.Service.Domain.Commands.ExecuteWorkflow {
  /// <summary>
  /// Class ExecuteWorkflowCommand.
  /// Implements the <see cref="IRequest{OperationResult}" />
  /// Implements the <see cref="IBaseRequest" />
  /// Implements the <see cref="IEquatable{ExecuteWorkflowCommand}" />
  /// </summary>
  /// <seealso cref="IRequest{OperationResult}" />
  /// <seealso cref="IBaseRequest" />
  /// <seealso cref="IEquatable{ExecuteWorkflowCommand}" />
  public record ExecuteWorkflowCommand(IdField<Guid> workflowId, BWorkflow workflow, BInputs input) : IRequest<OperationResult<ExecuteWorkflowDTO>>;


}
