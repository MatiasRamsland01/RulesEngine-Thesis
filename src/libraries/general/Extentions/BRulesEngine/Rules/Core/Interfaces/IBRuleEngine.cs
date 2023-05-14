// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IBRuleEngine.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace libraries.Extentions.BRulesEngine.Rules.Core.Engine {
  /// <summary>
  /// Interface IBRuleEngine
  /// </summary>
  public interface IBRuleEngine {

    public Task<PipelineResult> ExecuteWorkflowAsync(BWorkflow workflow, BInputs inputs);
  }
}
