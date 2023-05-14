// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="IBRule.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Interfaces {
  /// <summary>
  /// Interface IBRule
  /// </summary>
  public interface IBRule {
    public Guid Id { get; set; }

    public RuleDataField Data { get; set; }
  }
}
