// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-16-2023
// ***********************************************************************
// <copyright file="CVDto.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using FluentValidation;
/// <summary>
/// Class CVDto.
/// </summary>
public class CVDto {
  /// <summary>
  /// Initializes a new instance of the <see cref="CVDto" /> class.
  /// </summary>
  /// <param name="fields">The fields.</param>
  public CVDto(Dictionary<string, string> fields) {
    Fields = new Dictionary<string, string>();
    foreach (var keyValue in fields) {
      Fields[keyValue.Key] = keyValue.Value;
      Fields[keyValue.Value] = keyValue.Key;
    }
  }
  /// <summary>
  /// Gets the fields.
  /// </summary>
  /// <value>The fields.</value>
  public Dictionary<string, string> Fields { get; }
}
/// <summary>
/// Class CVValidator.
/// Implements the <see cref="AbstractValidator{CVDto}" />
/// </summary>
/// <seealso cref="AbstractValidator{CVDto}" />
public class CVValidator : AbstractValidator<CVDto> {
  /// <summary>
  /// Initializes a new instance of the <see cref="CVValidator" /> class.
  /// </summary>
  public CVValidator() =>
RuleFor(m => m.Fields)
.Must(check)
.WithMessage("The CV should at least contain a single field that should be changed");
  /// <summary>
  /// Checks the specified fields.
  /// </summary>
  /// <param name="fields">The fields.</param>
  /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
  /// <exception cref="System.NotImplementedException"></exception>
  private bool check(Dictionary<string, string> fields) {
    throw new NotImplementedException("");
  }
}
