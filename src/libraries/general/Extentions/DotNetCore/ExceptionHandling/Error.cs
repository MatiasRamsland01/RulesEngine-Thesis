// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-01-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-01-2023
// ***********************************************************************
// <copyright file="Error.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace libraries.Extentions.DotNetCore.ExceptionHandling {
  /// <summary>
  /// Class Error.
  /// </summary>
  public class Error {
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    /// <value>The error code.</value>
    public string? ErrorCode { get; set; }
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    public string? Message { get; set; }
    /// <summary>
    /// Gets or sets the display message.
    /// </summary>
    /// <value>The display message.</value>
    public string? DisplayMessage { get; set; }
    public Error() {
    }

    /// <summary>
    /// Froms the exception.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="displayMessage">The display message.</param>
    /// <returns>Error.</returns>
    public static Error FromException(Exception exception, string? displayMessage = null) {
      return new Error {
        Message = exception.Message,
        ErrorCode = exception.HResult.ToString(),
        DisplayMessage = string.IsNullOrWhiteSpace(displayMessage) ? "An unknown error has occurred" : displayMessage
      };
    }
  }

}

