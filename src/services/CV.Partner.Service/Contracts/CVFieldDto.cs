// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-16-2023
// ***********************************************************************
// <copyright file="CVFieldDto.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
/// <summary>
/// Class CVFieldDto.
/// </summary>
public class CVFieldDto {
  /// <summary>
  /// Initializes a new instance of the <see cref="CVFieldDto" /> class.
  /// </summary>
  public CVFieldDto() {
    Type = typeof(int);
    Name = string.Empty;
    Value = string.Empty;
  }
  /// <summary>
  /// Gets or sets the type.
  /// </summary>
  /// <value>The type.</value>
  public Type Type { get; set; }
  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  /// <value>The name.</value>
  public string Name { get; set; }
  /// <summary>
  /// Gets or sets the value.
  /// </summary>
  /// <value>The value.</value>
  public string Value { get; set; }
}
