// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BuisnessRuleFactory.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;
using fields.Entities.Base.Fields.Primitive;
using fields.Factories;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;
using libraries.Extentions.BRulesEngine.Rules.Core.Validators;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Factory {
  /// <summary>
  /// Class RuleEngineFactory.
  /// </summary>
  public class RuleEngineFactory {

    /// <summary>
    /// bs the workflow validator.
    /// </summary>
    /// <returns>IValidator&lt;BWorkflow&gt;.</returns>
    public static IValidator<BWorkflow> BWorkflowValidator() => new BWorkflowValidator(FieldsFactory.StringValidator(), BRuleValidator());
    public static IValidator<IBInputs> BInputsValidator() => new BInputsValidator(InputDataFieldValidator());
    /// <summary>
    /// bs the rule validator.
    /// </summary>
    /// <returns>IValidator&lt;IBRule&gt;.</returns>
    public static IValidator<IBRule> BRuleValidator() => new BRuleValidator(RuleDataFieldValidator());
    /// <summary>
    /// Creates the inputs.
    /// </summary>
    /// <returns>BInputs.</returns>
    public static IValidator<IInputDataField> InputDataFieldValidator() => new InputDataFieldValidator(FieldsFactory.IntegerValidator(), FieldsFactory.DateValidator(), FieldsFactory.StringValidator(), FieldsFactory.IdValidatorGuid());
    /// <summary>
    /// Inputs the data field validator.
    /// </summary>
    /// <param name="integerFieldValidator">The integer field validator.</param>
    /// <param name="datefieldValidator">The datefield validator.</param>
    /// <param name="stringfieldValidator">The stringfield validator.</param>
    /// <param name="idfieldValidator">The idfield validator.</param>
    /// <returns>IValidator&lt;IInputDataField&gt;.</returns>
    public static IValidator<IInputDataField> InputDataFieldValidator(IValidator<IIntegerField> integerFieldValidator, IValidator<IDateField> datefieldValidator, IValidator<IStringField> stringfieldValidator, IValidator<IIdField<Guid>> idfieldValidator) =>
  new InputDataFieldValidator(integerFieldValidator, datefieldValidator, stringfieldValidator, idfieldValidator);
    /// <summary>
    /// Rules the data field validator.
    /// </summary>
    /// <returns>IValidator&lt;IRuleDataField&gt;.</returns>
    public static IValidator<IRuleDataField> RuleDataFieldValidator() => new RuleDataFieldValidator(FieldsFactory.IntegerValidator(), FieldsFactory.DateValidator(), FieldsFactory.StringValidator(), FieldsFactory.BoolValidator());
    /// <summary>
    /// Rules the data field validator.
    /// </summary>
    /// <param name="integerFieldValidator">The integer field validator.</param>
    /// <param name="datefieldValidator">The datefield validator.</param>
    /// <param name="stringfieldValidator">The stringfield validator.</param>
    /// <param name="idfieldValidator">The idfield validator.</param>
    /// <returns>IValidator&lt;IRuleDataField&gt;.</returns>
    public static IValidator<IRuleDataField> RuleDataFieldValidator(IValidator<IIntegerField> integerFieldValidator, IValidator<IDateField> datefieldValidator, IValidator<IStringField> stringfieldValidator, IValidator<IBoolField> boolfieldValidator) =>
  new RuleDataFieldValidator(integerFieldValidator, datefieldValidator, stringfieldValidator, boolfieldValidator);
    /// <summary>
    /// Creates the inputs.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>BInputs.</returns>
    public static BInputs CreateInputs(dynamic obj) => new BInputs(obj);


    /// <summary>
    /// Creates the rule.
    /// </summary>
    /// <param name="ruleName">Name of the rule.</param>
    /// <param name="successEvent">The success event.</param>
    /// <param name="errorMsg">The error MSG.</param>
    /// <param name="expression">The expression.</param>
    /// <param name="stringFieldValidator">The string field validator.</param>
    /// <returns>BRule.</returns>
    public static BRule CreateRule(string ruleName, string successEvent, string errorMsg, string expression, string @operator, IValidator<IStringField>? stringFieldValidator = null) {
      return new BRule(
        FieldsFactory.StringField(ruleName, stringFieldValidator),
        FieldsFactory.StringField(successEvent, stringFieldValidator),
        FieldsFactory.StringField(errorMsg, stringFieldValidator),
        FieldsFactory.StringField(expression, stringFieldValidator),
        FieldsFactory.StringField(@operator, stringFieldValidator),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));

    }
    /// <summary>
    /// Creates the workflow.
    /// </summary>
    /// <param name="rule">The rule.</param>
    /// <param name="workflowName">Name of the workflow.</param>
    /// <param name="stringValidator">The string validator.</param>
    /// <returns>BWorkflow.</returns>
    public static BWorkflow CreateWorkflow(List<BRule> rule, string workflowName, IValidator<IStringField>? stringValidator = null) {
      return new BWorkflow(rule, FieldsFactory.StringField(workflowName, stringValidator));
    }

  }
}
