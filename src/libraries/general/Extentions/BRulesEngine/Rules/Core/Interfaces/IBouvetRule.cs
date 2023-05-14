// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IBouvetRule.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Interface IBouvetRule
  /// </summary>
  public interface IBouvetRule {
    public IdField<Guid> Id { get; set; }

    /// <summary>
    /// Gets the workflows.
    /// </summary>
    /// <value>The workflows.</value>
    public BRule Rule { get; }
  }
}
