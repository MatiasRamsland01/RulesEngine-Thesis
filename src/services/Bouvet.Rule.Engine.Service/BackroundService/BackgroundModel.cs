// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-03-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-09-2023
// ***********************************************************************
// <copyright file="BackgroundModel.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace Bouvet.Rule.Engine.Service.BackroundService {
  /// <summary>
  /// Class BackgroundModel.
  /// </summary>
  public class BackgroundModel {
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public IdField<Guid> id { get; }
    /// <summary>
    /// Gets or sets the name of the workflow.
    /// </summary>
    /// <value>The name of the workflow.</value>
    public BWorkflow Workflow { get; set; }
    /// <summary>
    /// Gets or sets the cv identifier.
    /// </summary>
    /// <value>The cv identifier.</value>
    public BInputs Input { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="BackgroundModel"/> class.
    /// </summary>
    /// <param name="workflow"></param>
    /// <param name="input"></param>
    /// <param name="correlationId">The correlation identifier.</param>
    public BackgroundModel(BWorkflow workflow, BInputs input, IdField<Guid> correlationId) {
      Workflow = workflow;
      Input = input;
      id = correlationId;
    }
  }
}
