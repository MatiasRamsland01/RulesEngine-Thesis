// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BRule.cs" company="Bouvet">
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
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;
using libraries.Extentions.BRulesEngine.Rules.Core.Validators;

namespace libraries.Extentions.BRulesEngine.Rules.Core {
  /// <summary>
  /// Class BRule.
  /// Implements the <see cref="IBRule" />
  /// </summary>
  /// <seealso cref="IBRule" />
  public class BRule : IBRule, Validate {
    #region Fields
    /// <summary>
    /// The validator
    /// </summary>
    private readonly IValidator<IBRule> validator;
    #endregion
    #region Properties
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the rule data.
    /// </summary>
    /// <value>The rule data.</value>
    public RuleDataField Data { get; set; }
    #endregion
    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="BRule" /> class.
    /// </summary>
    /// <param name="ruleName">Name of the rule.</param>
    /// <param name="successEvent">The success event.</param>
    /// <param name="errorMsg">The error MSG.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="rulevalidator">The rulevalidator.</param>
    public BRule(StringField ruleName, StringField successEvent, StringField errorMsg, StringField expression, StringField @operator, BoolField enabled, StringField author, IValidator<IBRule> rulevalidator = null!) {
      Id = Guid.NewGuid();
      Data = new RuleDataField(ruleName, expression, @operator, errorMsg, successEvent, enabled, author);
      validator = rulevalidator ?? RuleEngineFactory.BRuleValidator();
    }
    public BRule() {
      Id = Guid.NewGuid();
      Data = new RuleDataField();
      validator = RuleEngineFactory.BRuleValidator();

    }
    /// <inheritdoc/>
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
