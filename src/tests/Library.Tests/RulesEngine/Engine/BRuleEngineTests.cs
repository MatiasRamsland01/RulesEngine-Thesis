// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-25-2023
// ***********************************************************************
// <copyright file="BRuleEngineTests.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Engine;
using Microsoft.Extensions.Logging;
using Moq;
using tests.Factories;

namespace tests.RulesEngine.Engine {
  /// <summary>
  /// Class BRuleEngineTests.
  /// </summary>
  public class BRuleEngineTests {
    /// <summary>
    /// Defines the test method ShouldBePossibleToCreateABRuleEngineClassWithASingleWorkflow.
    /// </summary>
    [Fact]
    public void ShouldBePossibleToCreateABRuleEngineClassWithASingleWorkflow() {
      var engine = RulesEngineTestFactory.CreateValidEngineWithWorkflow();
      engine.Should().NotBeNull().And.BeOfType<BRuleEngine>();
    }
    /// <summary>
    /// Defines the test method ShouldBePossibleToRunAWorkflow.
    /// </summary>
    [Fact]
    public async void ShouldBePossibleToRunAWorkflow() {
      var workflow = RulesEngineTestFactory.CreateValidWorkflow();
      var engine = RulesEngineTestFactory.CreateBRuleEngine();
      var loggerMock = new Mock<ILogger>();
      var result = await engine.ExecuteWorkflowAsync(workflow, RulesEngineTestFactory.CreateValidInputNameProp(), loggerMock.Object);
      result.Should().BeOfType<PipelineResult>();
      result.outcome.Value.Should().BeTrue();
    }
  }
}
