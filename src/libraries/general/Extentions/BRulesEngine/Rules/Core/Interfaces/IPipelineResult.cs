// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IPipelineResult.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Interface IPipelineResult
  /// </summary>
  public interface IPipelineResult {
    /// <summary>
    /// Gets or sets the result trees.
    /// </summary>
    /// <value>The result trees.</value>
    /// <summary>
    /// Gets or sets the outcome.
    /// </summary>
    /// <value>The outcome.</value>
    public IdField<Guid> Id { get; set; }
    public IBoolField outcome { get; set; }
    public List<IStringField> errorMsg { get; set; }


  }
}
