using Bouvet.CV.Service.Infrastructure;
using libraries.Extentions.DotNetCore.ExceptionHandling;
using MediatR;
namespace Bouvet.CV.Service.Domain.Commands.AddRule {
  public class AddRuleHandler : IRequestHandler<AddRuleCommand, OperationResult<AddRuleCommand>> {
    private readonly ILogger<AddRuleHandler> _logger;
    private readonly RuleengineContext _db;
    public AddRuleHandler(ILogger<AddRuleHandler> logger, RuleengineContext db) {
      _logger = logger;
      _db = db;
    }
    public async Task<OperationResult<AddRuleCommand>> Handle(AddRuleCommand command, CancellationToken cancellationToken) {
      await _db.AddAsync(command.rule, cancellationToken);
      await _db.SaveChangesAsync(cancellationToken);
      return OperationResult<AddRuleCommand>.CreateSuccess(command, $"Rule added", 200);
    }
  }
}

