// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-01-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-09-2023
// ***********************************************************************
// <copyright file="OperationResult.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace libraries.Extentions.DotNetCore.ExceptionHandling {
  /// <summary>
  /// Interface IOperationResult
  /// </summary>
  public interface IOperationResult {
    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>The errors.</value>
    IEnumerable<Error> Errors { get; set; }
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="IOperationResult"/> is success.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    bool Success { get; set; }
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    string Message { get; set; }
    /// <summary>
    /// Gets or sets the HTTP status code.
    /// </summary>
    /// <value>The HTTP status code.</value>
    int HttpStatusCode { get; set; }
  }
  public interface IOperationResult<T> : IOperationResult {
    T Value { get; set; }
  }

  /// <summary>
  /// Class OperationResult.
  /// Implements the <see cref="Bouvet.Rule.Engine.Service.ExceptionHandling.IOperationResult" />
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <seealso cref="Bouvet.Rule.Engine.Service.ExceptionHandling.IOperationResult" />
  public class OperationResult<T> : IOperationResult<T> {
    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>The errors.</value>
    [JsonPropertyName("errors")]
    public IEnumerable<Error> Errors { get; set; } = new Error[0];
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [JsonPropertyName("value")]
    public T Value { get; set; } = default!;
    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="OperationResult{T}"/> is success.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>The message.</value>
    [JsonPropertyName("message")]
    public string Message { get; set; } = " ";
    /// <summary>
    /// Gets or sets the HTTP status code.
    /// </summary>
    /// <value>The HTTP status code.</value>
    [JsonPropertyName("httpstatuscode")]

    public int HttpStatusCode { get; set; }

    /// <summary>
    /// Creates the success.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="message">The message.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <returns>OperationResult&lt;T&gt;.</returns>
    public static OperationResult<T> CreateSuccess(T result, string message = "", int httpStatusCode = 200) {
      return new OperationResult<T>() { Success = true, Value = result, Message = message, HttpStatusCode = httpStatusCode };
    }
    /// <summary>
    /// Creates the failure.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="displayMessage">The display message.</param>
    /// <param name="message">The message.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <returns>OperationResult&lt;T&gt;.</returns>
    public static OperationResult<T> CreateFailure(T result, Exception exception, string? displayMessage = "", string message = "", int httpStatusCode = 400) {
      Error error = Error.FromException(exception, displayMessage);
      return new OperationResult<T>() { Value = result, Errors = new[] { error }, Message = exception.ToString(), Success = false, HttpStatusCode = httpStatusCode };
    }
  }
}
