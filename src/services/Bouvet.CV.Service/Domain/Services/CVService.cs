using Bouvet.CV.Service.Domain.Commands.ModifyRule;
using Bouvet.CV.Service.Domain.Queries.GetRule;
using Bouvet.CV.Service.MetricQueries;
using fields.Entities.Base.Fields;
using fields.Entities.Base.Fields.Primitive;
using libraries.Contracts.Bouvet.CV.Service.DTOs;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Prometheus;

namespace Bouvet.CV.Service.Domain.Services {
  public interface ICVService {
    Task UpdateCVFailureCount(StringField ruleName);
    Task UpdateCVSuccessCount(StringField ruleName);
  }
  public class CVService : ICVService {
    private readonly IMediator _mediator;
    private readonly ILogger<CVService> _logger;


    public CVService(IMediator mediator, ILogger<CVService> logger) {
      _mediator = mediator;
      _logger = logger;
    }
    public async Task UpdateCVFailureCount(StringField ruleName) {
      OperationResult<RuleDTO> fetchedRuleResult = await _mediator.Send(new GetRuleQuery(ruleName));
      if (!fetchedRuleResult.Success) {
        _logger.LogError("Rule with name: {RULENAME} not found", ruleName.Value);
        return;
      }
      BRule rule = fetchedRuleResult.Value.Rule;
      rule.Data.FailureCount.Value = rule.Data.FailureCount.Value + 1;
      CVMetrics.RulesExecution.WithLabels(rule.Data.RuleName.Value.Replace(" ", ""), "failure").IncTo(rule.Data.FailureCount.Value);
      await _mediator.Send(new ModifyRuleCommand(rule));
    }
    public async Task UpdateCVSuccessCount(StringField ruleName) {
      OperationResult<RuleDTO> fetchedRuleResult = await _mediator.Send(new GetRuleQuery(ruleName));
      if (!fetchedRuleResult.Success) {
        _logger.LogError("Rule with name: {RULENAME} not found", ruleName.Value);
        return;
      }
      BRule rule = fetchedRuleResult.Value.Rule;
      rule.Data.SuccessCount.Value = rule.Data.SuccessCount.Value + 1;
      CVMetrics.RulesExecution.WithLabels(rule.Data.RuleName.Value.Replace(" ", ""), "success").IncTo(rule.Data.SuccessCount.Value);
      await _mediator.Send(new ModifyRuleCommand(rule));
    }
  }
}
