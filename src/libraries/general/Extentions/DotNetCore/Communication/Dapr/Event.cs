// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 03-13-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-13-2023
// ***********************************************************************
// <copyright file="Event.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Dates;
using fields.Entities.Base.Fields.Custom;

namespace libraries.Extentions.DotNetCore.Communication.Dapr {

  /// <summary>
  /// Interface IEvent
  /// </summary>
  public interface IEvent<T> {
    /// <summary>
    /// Gets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public IdField<T> EventId { get; }

    /// <summary>
    /// Gets the creation date.
    /// </summary>
    /// <value>The creation date.</value>
    public DateField CreationDate { get; }
  }
}
