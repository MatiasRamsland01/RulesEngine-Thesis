using System.Text.Json;
using Bouvet.CV.Service.Infrastructure;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Contracts.Bouvet.Rule.Engine.Service;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.CV.Service.Domain.Queries.GetRule {
  public class GetRuleHandler : IRequestHandler<GetRuleQuery, OperationResult<RuleDTO>> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetRuleHandler> _logger;
    private readonly RuleengineContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRuleHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="db"></param>
    public GetRuleHandler(ILogger<GetRuleHandler> logger, RuleengineContext db) {
      _logger = logger;
      _db = db;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<RuleDTO>> Handle(GetRuleQuery query, CancellationToken cancellationToken) {
      BRule rule = await _db.Rules.Include(x => x.Data)
          .Where(x => x.Data.RuleName.Value == query.ruleName.Value)
          .FirstAsync(cancellationToken) ?? throw new ArgumentException($"Rule with name {query.ruleName.Value} not found");
      return OperationResult<RuleDTO>.CreateSuccess(new RuleDTO(rule), $"Rule fetched successfully", 200);
    }
  }
}
