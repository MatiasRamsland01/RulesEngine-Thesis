// ***********************************************************************
// Assembly         : libraries
// Author           : matias.ramsland
// Created          : 02-22-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 02-27-2023
// ***********************************************************************
// <copyright file="CvMustNotBeNull.cs" company="Bouvet">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using fields.Entities.Base.Fields.Custom;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;

namespace libraries.Extentions.BRulesEngine.Rules.BusinessRules {
  /// <summary>
  /// Class CvMustNotBeNull.
  /// Implements the <see cref="IBouvetRule" />
  /// </summary>
  /// <seealso cref="IBouvetRule" />
  public class CvMustNotBeNull : IBouvetRule {
    /// <inheritdoc/>
    public IdField<Guid> Id { get; set; }
    /// <summary>
    /// Gets the workflows.
    /// </summary>
    /// <value>The workflows.</value>
    public BRule Rule { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CvMustNotBeNull" /> class.
    /// </summary>
    /// <param name="stringFieldValidator">The string field validator.</param>
    public CvMustNotBeNull() {
      Id = FieldsFactory.IdField(Guid.NewGuid());
      Rule = new BRule(FieldsFactory.StringField("Not Null"),
        FieldsFactory.StringField("SuccessEvent-None"),
        FieldsFactory.StringField("input where null"),
        FieldsFactory.StringField("input != null"),
        FieldsFactory.StringField("AND"),
        FieldsFactory.BoolField(true),
        FieldsFactory.StringField("Rules Engine Team"));
    }
  }
}
