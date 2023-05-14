// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-23-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-25-2023
// ***********************************************************************
// <copyright file="RulesEngineDesignTests.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoFixture;
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.Core;
using Microsoft.Extensions.Logging;
using Moq;
using tests.Factories;

namespace tests.RulesEngine.Engine {
  /// <summary>
  /// Class RulesEngineDesignTests.
  /// </summary>
  public class RulesEngineDesignTests {
    /// <summary>
    /// Should be possible to create and run a workflow with a single rule as an asynchronous operation.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [Theory]
    [InlineData("input.name != null", true)]
    [InlineData("input.name == null", false)]
    public async void ShouldBePossibleToCreateAndRunAWorkflowWithASingleRuleAsync(string expression, bool expected) {
      // Arrange
      var input = RulesEngineTestFactory.CreateValidInputNameProp();
      var workflow = RulesEngineTestFactory.CreateValidWorkflow(expression);
      var engine = RulesEngineTestFactory.CreateBRuleEngine();
      var loggerMock = new Mock<ILogger>();

      // Act
      var result = await engine.ExecuteWorkflowAsync(workflow, input, loggerMock.Object);
      // Assert
      result.Should().NotBeNull().And.BeOfType<PipelineResult>();
      result.outcome.Value.Should().Be(expected);
    }
    /// <summary>
    /// Should be possible to create and run a workflow with multiple rules as an asynchronous operation.
    /// </summary>
    /// <param name="expressionInput1">The expression input1.</param>
    /// <param name="expressionInput2">The expression input2.</param>
    /// <param name="expected">if set to <c>true</c> [expected].</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    [Theory]
    [InlineData("input.name != null", "input.name.Contains(\"TestName\")", true)]
    [InlineData("input.name != null", "input.name.Contains(\"NameDoesNotContainThisString\")", false)]
    public async void ShouldBePossibleToCreateAndRunAWorkflowWithMultipleRulesAsync(string expressionInput1, string expressionInput2, bool expected) {
      // Arrange
      var fixture = new Fixture();
      var input = RulesEngineTestFactory.CreateValidInputNameProp();
      var rule2 = RulesEngineTestFactory.CreateValidRule(expressionInput2);
      var workflow = RulesEngineTestFactory.CreateValidWorkflow(expressionInput1);
      workflow.AddRule(rule2);
      workflow.Rules.Should().HaveCount(2);
      var loggerMock = new Mock<ILogger>();
      var engine = RulesEngineTestFactory.CreateBRuleEngine();

      // Act
      var result = await engine.ExecuteWorkflowAsync(workflow, input, loggerMock.Object);

      // Assert
      result.Should().NotBeNull().And.BeOfType<PipelineResult>();
      result.outcome.Value.Should().Be(expected);
    }
  }
}
