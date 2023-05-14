// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="PipelineResult.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using RulesEngine.Models;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Class PipelineResult.
  /// Implements the <see cref="FluentResults.Result{PipelineResult}" />
  /// </summary>
  /// <seealso cref="FluentResults.Result{PipelineResult}" />
  public class PipelineResult {
    public Guid Id { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineResult" /> class.
    /// </summary>
    public StringField ContactPerson { get; set; } = FieldsFactory.StringField("Matias Ramsland or Roger Bærheim");
    public StringField ContactPersonEmail { get; set; } = FieldsFactory.StringField("matias.ramsland@bouvet.no or roger.berheim@bouvet.no");
    public List<StringField> RulesRan { get; set; } = new List<StringField>();
    public List<StringField> RulesFailed { get; set; } = new List<StringField>();
    public PipelineResult(List<RuleResultTree> resultTrees) {
      Id = Guid.NewGuid();
      outcome.Value = resultTrees.TrueForAll(r => r.IsSuccess);
      resultTrees.ForEach(r => {
        RulesRan.Add(FieldsFactory.StringField(r.Rule.RuleName));
        if (!r.IsSuccess) {
          RulesFailed.Add(FieldsFactory.StringField(r.Rule.RuleName));
          errorMsg.Add(FieldsFactory.StringField(r.ExceptionMessage));
        }
      });
    }
    public PipelineResult(BWorkflow bworkflow, BoolField outcome, List<StringField> errorMsg) {
      Id = Guid.NewGuid();
      this.outcome = outcome;
      this.errorMsg = errorMsg;
    }
    public PipelineResult() {
      Id = Guid.NewGuid();
    }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="PipelineResult" /> is outcome.
    /// </summary>
    /// <value><c>true</c> if outcome; otherwise, <c>false</c>.</value>
    public BoolField outcome { get; set; } = FieldsFactory.BoolField(false);
    /// <summary>
    /// Gets or sets the error MSG.
    /// </summary>
    /// <value>The error MSG.</value>
    public List<StringField> errorMsg { get; set; } = new List<StringField>();
  }
}
