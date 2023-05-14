// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BWorkflow.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.Validation;
using fields.Factories;
using FluentValidation;
using FluentValidation.Results;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Class BWorkflow.
  /// Implements the <see cref="IBWorkflow" />
  /// </summary>
  /// <seealso cref="IBWorkflow" />
  public class BWorkflow : IBWorkflow, Validate {
    #region Fields
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<BWorkflow> validator = RuleEngineFactory.BWorkflowValidator();
    /// <inheritdoc/>
    #endregion
    #region Properties
    public Guid Id { get; set; }
    public List<BRule> Rules { get; set; }
    /// <inheritdoc/>
    public StringField WorkflowName { get; set; }
    /// <summary>
    /// Gets the workflow.
    /// </summary>
    /// <value>The workflow.</value>
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BWorkflow" /> class.
    /// </summary>
    /// <param name="rules">The rules.</param>
    /// <param name="workFlowName">Name of the work flow.</param>
    /// <param name="workflowValidator">The workflow validator.</param>
    public BWorkflow(List<BRule> rules, StringField workFlowName, IValidator<BWorkflow>? workflowValidator = null) {
      WorkflowName = workFlowName;
      if (workflowValidator != null) { validator = workflowValidator; }
      Rules = rules;
      DoValidateAndThrow();
    }
    /// <inheritdoc/>
    public BWorkflow() {
      Rules = new List<BRule>();
      WorkflowName = FieldsFactory.StringField("WorkflowName");
    }

    #endregion

    #region Set-Methods
    /// <summary>
    /// Changes the name of the workflow.
    /// </summary>
    /// <param name="name">The name.</param>
    public void ChangeWorkflowName(StringField name) {
      name.DoValidateAndThrow();
      WorkflowName = name;
      DoValidateAndThrow();
    }
    /// <summary>
    /// Adds the rule.
    /// </summary>
    /// <param name="ruleToAdd">The rule to add.</param>
    public void AddRule(BRule ruleToAdd) {
      //ruleToAdd.DoValidateAndThrow();
      Rules.Add(ruleToAdd);
      //DoValidateAndThrow();
    }
    /// <summary>
    /// Deletes the rule.
    /// </summary>
    /// <param name="rule">The rule.</param>
    public void DeleteRule(BRule rule) {
      rule.DoValidateAndThrow();
      Rules.Remove(rule);
      DoValidateAndThrow();
    }

    /// <summary>
    /// Modifies the rule.
    /// </summary>
    /// <param name="newRule">The new rule.</param>
    /// <exception cref="System.ArgumentException">Rule not found</exception>
    // public void ModifyRule(BRule newRule) {
    //   newRule.DoValidateAndThrow();
    //   var index = Rules.FindIndex(x => x.Id == newRule.Id);
    //   if (index != -1) {
    //     Rules[index] = newRule;
    //   }
    //   else {
    //     throw new ArgumentException("Rule not found");
    //   }
    //   DoValidateAndThrow();
    // }
    #endregion
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
