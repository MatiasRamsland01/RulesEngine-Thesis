// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IBWorkflow.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Interfaces {
  /// <summary>
  /// Interface IBWorkflow
  /// </summary>
  public interface IBWorkflow {
    public List<BRule> Rules { get; set; }
    public StringField WorkflowName { get; set; }
    public void DeleteRule(BRule rule);
    public void AddRule(BRule ruleToAdd);
    public void ChangeWorkflowName(StringField name);
  }
}
