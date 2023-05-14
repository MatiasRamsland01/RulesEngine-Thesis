// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BRuleEngine.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Validation;
using FluentValidation;
using FluentValidation.Results;
using libraries.Extentions.BRulesEngine.Rules.Core.Validators;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RulesEngine.Models;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Engine {
  /// <summary>
  /// Class BRuleEngine.
  /// Implements the <see cref="libraries.Extentions.BRulesEngine.Rules.Core.Engine.IBRuleEngine" />
  /// Implements the <see cref="Validate" />
  /// </summary>
  /// <seealso cref="libraries.Extentions.BRulesEngine.Rules.Core.Engine.IBRuleEngine" />
  /// <seealso cref="Validate" />
  public class BRuleEngine : Validate {
    #region Fields

    public RulesEngine.RulesEngine rulesengine = new RulesEngine.RulesEngine(reSettings: GetReSettings());
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<BRuleEngine> validator = new BRuleEngineValidator();
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BRuleEngine" /> class.
    /// </summary>
    /// <param name="workflow">The workflow.</param>
    public BRuleEngine() {
    }
    #endregion
    #region Execute-Methods
    public async Task<PipelineResult> ExecuteWorkflowAsync(BWorkflow bworkflow, BInputs inputs, ILogger logger) {
      //bworkflow.DoValidateAndThrow();
      //inputs.DoValidateAndThrow();
      var rules = new List<Rule>();
      bworkflow.Rules.ToList().ForEach(x => {
        var ruleToAdd = new Rule() {
          RuleName = x.Data.RuleName.Value,
          Expression = x.Data.Expression.Value,
          Enabled = x.Data.Enabled.Value,
          Operator = x.Data.Operator.Value,
          ErrorMessage = x.Data.ErrorMessage.Value,
          RuleExpressionType = RuleExpressionType.LambdaExpression,
          SuccessEvent = x.Data.SuccessEvent.Value,
          WorkflowsToInject = new List<string>() { bworkflow.WorkflowName.Value }
        };
        rules.Add(ruleToAdd);
      });
      if (rules.Count == 0) {
        throw new Exception("No rules found");
      }
      var workflow = new Workflow() {
        WorkflowName = bworkflow.WorkflowName.Value,
        Rules = rules
      };
      rulesengine.AddWorkflow(workflow);
      logger.LogCritical("Workflow to be ran: {WORKFLOW}, and inputs: {INPUTS}", JsonConvert.SerializeObject(workflow), JsonConvert.SerializeObject(inputs));
      var resultList = await rulesengine.ExecuteAllRulesAsync(workflow.WorkflowName, new RuleParameter("input", inputs.Inputs));
      var PipelineResult = new PipelineResult(resultList);
      return PipelineResult;
    }
    #endregion
    private static ReSettings GetReSettings() {
      var reSettings = new ReSettings {
        CustomTypes = new Type[] { typeof(CustomRules) },
      };
      return reSettings;
    }
    #region Validation-Methods
    /// <summary>
    /// Does the validate.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidate() => validator.Validate(this, options => options.IncludeAllRuleSets());
    /// <summary>
    /// Does the validate and throw.
    /// </summary>
    /// <returns>ValidationResult.</returns>
    public ValidationResult DoValidateAndThrow() => validator.Validate(this, options => options.IncludeAllRuleSets().ThrowOnFailures());

    #endregion
  }

}
