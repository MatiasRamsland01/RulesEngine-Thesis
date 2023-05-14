// ***********************************************************************
// Assembly         : Bouvet.Rule.Engine.Service
// Author           : matias.ramsland
// Created          : 03-01-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-03-2023
// ***********************************************************************
// <copyright file="BackgroundTaskQueue.cs" company="Bouvet.Rule.Engine.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Bouvet.Rule.Engine.Service.BackroundService;
using System.Threading.Channels;

/// <summary>
/// Interface IBackgroundTaskQueue
/// </summary>
public interface IBackgroundTaskQueue {
  /// <summary>
  /// Queues the background work item asynchronous.
  /// </summary>
  /// <param name="workItem">The work item.</param>
  /// <returns>ValueTask.</returns>
  ValueTask QueueBackgroundWorkItemAsync(BackgroundModel workItem);

  /// <summary>
  /// Dequeues the asynchronous.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
  /// <returns>ValueTask&lt;BackgroundModel&gt;.</returns>
  ValueTask<BackgroundModel> DequeueAsync(CancellationToken cancellationToken);
}
/// <summary>
/// Class DefaultBackgroundTaskQueue. This class cannot be inherited.
/// Implements the <see cref="IBackgroundTaskQueue" />
/// </summary>
/// <seealso cref="IBackgroundTaskQueue" />
public sealed class DefaultBackgroundTaskQueue : IBackgroundTaskQueue {
  /// <summary>
  /// The queue
  /// </summary>
  private readonly Channel<BackgroundModel> _queue;

  /// <summary>
  /// Initializes a new instance of the <see cref="DefaultBackgroundTaskQueue"/> class.
  /// </summary>
  /// <param name="capacity">The capacity.</param>
  public DefaultBackgroundTaskQueue(int capacity) {
    BoundedChannelOptions options = new(capacity) {
      FullMode = BoundedChannelFullMode.Wait
    };
    _queue = Channel.CreateBounded<BackgroundModel>(options);
  }

  /// <summary>
  /// Dequeue as an asynchronous operation.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
  /// <returns>A Task&lt;BackgroundModel&gt; representing the asynchronous operation.</returns>
  public async ValueTask<BackgroundModel> DequeueAsync(CancellationToken cancellationToken) {
    BackgroundModel workItem = await _queue.Reader.ReadAsync(cancellationToken);
    return workItem;
  }

  /// <summary>
  /// Queue background work item as an asynchronous operation.
  /// </summary>
  /// <param name="workItem">The work item.</param>
  /// <returns>A Task&lt;ValueTask&gt; representing the asynchronous operation.</returns>
  /// <exception cref="System.ArgumentNullException">workItem</exception>
  public async ValueTask QueueBackgroundWorkItemAsync(BackgroundModel workItem) {
    if (workItem is null) {
      throw new ArgumentNullException(nameof(workItem));
    }

    await _queue.Writer.WriteAsync(workItem);
  }
}
