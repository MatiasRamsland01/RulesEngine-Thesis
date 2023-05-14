// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-24-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-25-2023
// ***********************************************************************
// <copyright file="BRuleTests.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentAssertions;
using libraries.Extentions.BRulesEngine.Rules.Core;
using tests.Factories;

namespace tests.RulesEngine.Engine {
  /// <summary>
  /// Class BRuleTests.
  /// </summary>
  public class BRuleTests {
    /// <summary>
    /// Defines the test method ShouldBePossibleToCreateARule.
    /// </summary>
    [Fact]
    public void ShouldBePossibleToCreateARule() {
      var rule = RulesEngineTestFactory.CreateValidRule();
      rule.Should().NotBeNull().And.BeOfType<BRule>();
      rule.DoValidate().IsValid.Should().BeTrue();
    }

  }
}
