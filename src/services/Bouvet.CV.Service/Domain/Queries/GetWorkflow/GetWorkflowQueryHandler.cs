using System.Text.Json;
using System.Text.Json.Nodes;
using Bouvet.CV.Service.Infrastructure;
using fields.Factories;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.CV.Service.Domain.Queries.GetWorkflow {
  public class GetWorkflowHandler : IRequestHandler<GetWorkflowQuery, OperationResult<WorkflowDTO>> {
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GetWorkflowHandler> _logger;
    private readonly RuleengineContext _db;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddRuleHandler" /> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="db"></param>
    public GetWorkflowHandler(ILogger<GetWorkflowHandler> logger, RuleengineContext db) {
      _logger = logger;
      _db = db;
    }

    /// <summary>
    /// Handles a request
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response from the request</returns>
    public async Task<OperationResult<WorkflowDTO>> Handle(GetWorkflowQuery query, CancellationToken cancellationToken) {
      var workflow = await _db.Workflows
        .Where(x => x.WorkflowName.Value == query.workflowName.Value)
        .Include(x => x.Rules)
        .ThenInclude(x => x.Data)
        .FirstOrDefaultAsync(cancellationToken);

      if (workflow == null) {
        return OperationResult<WorkflowDTO>.CreateFailure(default!, new ArgumentException("Workflow name not valid"), $"Workflow not found");
      }
      return OperationResult<WorkflowDTO>.CreateSuccess(new WorkflowDTO(workflow), $"Workflow fetched successfully", 200);
    }
  }
}
