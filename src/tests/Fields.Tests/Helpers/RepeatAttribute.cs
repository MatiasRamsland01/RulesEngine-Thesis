// ***********************************************************************
// Assembly         : tests
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-16-2023
// ***********************************************************************
// <copyright file="RepeatAttribute.cs" company="tests">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace Tests.Helpers {
  /// <summary>
  /// Class RepeatAttribute. This class cannot be inherited.
  /// Implements the <see cref="Xunit.Sdk.DataAttribute" />
  /// </summary>
  /// <seealso cref="Xunit.Sdk.DataAttribute" />
  public sealed class RepeatAttribute : Xunit.Sdk.DataAttribute {
    /// <summary>
    /// The count
    /// </summary>
    private readonly int count;
    /// <summary>
    /// Initializes a new instance of the <see cref="RepeatAttribute" /> class.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public RepeatAttribute(int count) {
      if (count < 1) {
        throw new ArgumentOutOfRangeException(
            paramName: nameof(count),
            message: "Repeat count must be greater than 0."
            );
      }
      this.count = count;
    }
    /// <summary>
    /// Returns the data to be used to test the theory.
    /// </summary>
    /// <param name="testMethod">The method that is being tested</param>
    /// <returns>One or more sets of theory data. Each invocation of the test method
    /// is represented by a single object array.</returns>
    public override System.Collections.Generic.IEnumerable<object[]> GetData(System.Reflection.MethodInfo testMethod) {
      foreach (var iterationNumber in Enumerable.Range(start: 1, count: this.count)) {
        yield return new object[] { iterationNumber };
      }
    }
  }
}
