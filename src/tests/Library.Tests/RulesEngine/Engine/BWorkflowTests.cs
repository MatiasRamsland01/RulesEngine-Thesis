// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-24-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-25-2023
// ***********************************************************************
// <copyright file="BWorkflowTests.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using AutoFixture;
using fields.Factories;
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.RulesEngine.Engine {
  /// <summary>
  /// Class BWorkflowTests.
  /// </summary>
  public class BWorkflowTests {
    #region Unit Tests Constructors
    /// <summary>
    /// Defines the test method ShouldBePossibleToCreateAWorkflow.
    /// </summary>
    [Fact]
    public void ShouldBePossibleToCreateAWorkflow() {
      var workflow = RulesEngineTestFactory.CreateValidWorkflow();
      workflow.Should().NotBeNull().And.BeOfType<BWorkflow>();
      workflow.DoValidate().IsValid.Should().BeTrue();
      workflow.Rules.First().Data.DoValidate().IsValid.Should().BeTrue();
    }
    #endregion

    /// <summary>
    /// Defines the test method ShouldBePossibleToAddARule.
    /// </summary>
    [Fact]
    public void ShouldBePossibleToAddARule() {
      var workflow = RulesEngineTestFactory.CreateValidWorkflow();
      workflow.AddRule(RulesEngineTestFactory.CreateValidRule());
      workflow.Rules.Should().HaveCount(2).And.HaveSameCount(workflow.Rules);
    }
    /// <summary>
    /// Defines the test method ShouldBePossibleToRemoveARule.
    /// </summary>
    [Fact]
    public void ShouldBePossibleToRemoveARule() {
      var workflow = RulesEngineTestFactory.CreateValidWorkflow();
      var ruleToRemove = RulesEngineTestFactory.CreateValidRule();
      workflow.AddRule(ruleToRemove);
      workflow.DeleteRule(ruleToRemove);
      workflow.Rules.Should().HaveCount(1);
      workflow.Rules.First().Data.RuleName.Value.Should().NotBe(ruleToRemove.Data.RuleName.Value);
    }
    /// <summary>
    /// Defines the test method ShouldBePossibleToModifyARule.
    /// </summary>
    // [Fact]
    // public void ShouldBePossibleToModifyARule() {
    //   var fixture = new Fixture();
    //   var workflow = RulesEngineTestFactory.CreateValidWorkflow();
    //   var rule = workflow.Rules.First();
    //   var newSuccessEvent = fixture.Create<string>("NewSuccessEvent");
    //   rule.Data.SuccessEvent = FieldsFactory.StringField(newSuccessEvent);
    //   workflow.ModifyRule(rule);
    //   workflow.Rules.Should().HaveCount(1);
    //   workflow.Rules.First().Data.SuccessEvent.Value.Should().Be(newSuccessEvent);
    // }
  }
}
