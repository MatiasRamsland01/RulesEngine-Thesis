using Bouvet.CV.Service.Infrastructure;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bouvet.CV.Service.Domain.Commands.ModifyRule {
  public class ModifyRuleHandler : IRequestHandler<ModifyRuleCommand, OperationResult<ModifyRuleCommand>> {
    private readonly ILogger<ModifyRuleHandler> _logger;
    private readonly RuleengineContext _db;
    public ModifyRuleHandler(ILogger<ModifyRuleHandler> logger, RuleengineContext db) {
      _logger = logger;
      _db = db;
    }
    public async Task<OperationResult<ModifyRuleCommand>> Handle(ModifyRuleCommand command, CancellationToken cancellationToken) {
      var data = await _db.Rules.Where(x => x.Id == command.rule.Id).FirstAsync(cancellationToken);
      data.Data = command.rule.Data;
      _db.Update(data);
      await _db.SaveChangesAsync(cancellationToken);
      return OperationResult<ModifyRuleCommand>.CreateSuccess(command, $"Rule modified", 200);
    }
  }
}

