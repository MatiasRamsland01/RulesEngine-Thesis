// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="BRuleValidator.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using fields.Entities.Base.FieldsConfiguration;
using fields.Entities.Base.Validators;
using fields.Factories;
using fields.Messages;
using FluentValidation;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
using libraries.Extentions.BRulesEngine.Rules.Core.Interfaces;
using Microsoft.Extensions.Options;

namespace libraries.Extentions.BRulesEngine.Rules.Core.Validators {
  public class BRuleValidator : AbstractValidator<IBRule> {
    public BRuleValidator(IValidator<IRuleDataField> ruleDataValidator) {
      RuleFor(x => x.Data).SetValidator(ruleDataValidator);
    }
  }
}
