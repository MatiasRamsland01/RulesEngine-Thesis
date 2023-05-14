// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-25-2023
// ***********************************************************************
// <copyright file="RulesEngineFactory.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoFixture;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;
using libraries.Extentions.BRulesEngine.Rules.Core.Factory;
using fields.Entities.HttpServices.CVPartnerService.CvHttpClient;
using System.Text.Json;

namespace tests.Factories {
  /// <summary>
  /// Class RulesEngineTestFactory.
  /// </summary>
  public class RulesEngineTestFactory {
    /// <summary>
    /// The fixture
    /// </summary>
    private static Fixture fixture = new Fixture();
    /// <summary>
    /// Creates the b rule engine.
    /// </summary>
    /// <param name="workflows">The workflows.</param>
    /// <returns>BRuleEngine.</returns>
    public static BRuleEngine CreateBRuleEngine() => new BRuleEngine();
    /// <summary>
    /// Creates the valid input name property.
    /// </summary>
    /// <returns>BInputs.</returns>
    public static BInputs CreateValidInputNameProp() => new BInputs(new Response() { Name = "TestName" });
    /// <summary>
    /// Creates the valid rule.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>BRule.</returns>
    public static BRule CreateValidRule(string expression = "input.name != null") => RuleEngineFactory.CreateRule(fixture.Create("RuleName"), fixture.Create("SuccessEvent"), fixture.Create("ErrorNullRuleViolated"), expression, "AND");
    public static BWorkflow CreateEmptyWorkflow() => new BWorkflow();

    /// <summary>
    /// Creates the valid workflow.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>BWorkflow.</returns>
    public static BWorkflow CreateValidWorkflow(string expression = "input.name != null") => RuleEngineFactory.CreateWorkflow(new List<BRule> { CreateValidRule(expression) }, fixture.Create("WorkflowName"));
    /// <summary>
    /// Creates the in valid workflow.
    /// </summary>
    /// <returns>BWorkflow.</returns>
    public static BWorkflow CreateInValidWorkflow() => RuleEngineFactory.CreateWorkflow(new List<BRule> { CreateValidRule("input.name == null") }, fixture.Create("WorkflowName2"));
    /// <summary>
    /// Creates the valid engine with workflow.
    /// </summary>
    /// <returns>BRuleEngine.</returns>
    public static BRuleEngine CreateValidEngineWithWorkflow() => new BRuleEngine();

    /// <summary>
    /// Creates the valid cv.
    /// </summary>
    /// <param name="jsonstring">The jsonstring.</param>
    /// <returns>CV.</returns>
    /// <exception cref="Newtonsoft.Json.JsonException">Could not convert jsonstring to object</exception>
    public static Response CreateValidCv(string jsonstring) {
      var settings = new JsonSerializerOptions {
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
      };
      return JsonSerializer.Deserialize<Response>(System.IO.File.ReadAllText(jsonstring), settings) ?? throw new JsonException("Could not convert jsonstring to object");
    }
  }
}
