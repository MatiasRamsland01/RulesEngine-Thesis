// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IBInputs.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Interface IBInputs
  /// </summary>
  public interface IBInputs {
    public Guid Id { get; set; }
    /// <summary>
    /// Gets the inputs.
    /// </summary>
    /// <value>The inputs.</value>
    public dynamic Inputs { get; set; }
    public InputDataField DataField { get; set; }
  }
}
