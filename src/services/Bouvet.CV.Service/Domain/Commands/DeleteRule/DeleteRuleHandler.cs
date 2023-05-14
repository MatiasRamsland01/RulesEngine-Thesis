using Bouvet.CV.Service.Infrastructure;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.CV.Service.Domain.Commands.DeleteRule {
  public class AddRuleHandler : IRequestHandler<DeleteRuleCommand, OperationResult<DeleteRuleCommand>> {
    private readonly ILogger<AddRuleHandler> _logger;
    private readonly RuleengineContext _db;
    public AddRuleHandler(ILogger<AddRuleHandler> logger, RuleengineContext db) {
      _logger = logger;
      _db = db;
    }
    public async Task<OperationResult<DeleteRuleCommand>> Handle(DeleteRuleCommand command, CancellationToken cancellationToken) {
      var ruleToDelete = await _db.Rules.SingleOrDefaultAsync(r => r.Data.RuleName.Value == command.ruleName.Value, cancellationToken);
      if (ruleToDelete != null) {
        _db.Rules.Remove(ruleToDelete);
        await _db.SaveChangesAsync(cancellationToken);
      }
      await _db.SaveChangesAsync(cancellationToken);
      return OperationResult<DeleteRuleCommand>.CreateSuccess(command, $"Rule deleted", 200);
    }
  }
}

