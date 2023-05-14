// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 03-14-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="DaprStateObject.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Primitive;
using System.ComponentModel.DataAnnotations;

namespace CV.Partner.Service.Models {
  /// <summary>
  /// Class DaprStateObject.
  /// </summary>
  public class DaprStateObject {
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [Key]
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the description.
    /// </summary>
    /// <value>The description.</value>
    public IStringField? Description { get; set; }
    /// <summary>
    /// Gets or sets the author.
    /// </summary>
    /// <value>The author.</value>
    public IStringField? Author { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DaprStateObject"/> class.
    /// </summary>
    public DaprStateObject() {
      Id = Guid.NewGuid();
    }
  }
}
