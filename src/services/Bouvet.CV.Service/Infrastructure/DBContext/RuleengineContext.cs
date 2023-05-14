using fields.Entities.Base.Fields;
using fields.Entities.Base.FieldsConfiguration;
using fields.Factories;
using libraries.Extentions.BRulesEngine.Rules.Core;
using libraries.Extentions.BRulesEngine.Rules.Core.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bouvet.CV.Service.Infrastructure;
public class RuleengineContext : DbContext {
  public RuleengineContext(DbContextOptions<RuleengineContext> configuration) : base(configuration) {
  }
  public DbSet<BRule> Rules { get; set; } = null!;
  public DbSet<BWorkflow> Workflows { get; set; } = null!;
  public DbSet<RuleDataField> RuleDataFields { get; set; } = null!;

  //public DbSet<BWorkflow> Workflows { get; set; } = null!;
  protected override void OnModelCreating(ModelBuilder modelBuilder) {

    modelBuilder.Entity<BRule>(entity => {
      entity.HasKey(x => x.Id);
      entity.HasOne(x => x.Data);

    });
    modelBuilder.Entity<BWorkflow>(entity => {
      entity.HasKey(x => x.Id);
      entity.OwnsOne(x => x.WorkflowName);
      entity.HasMany(x => x.Rules);
    });
    modelBuilder.Entity<RuleDataField>(entity => {
      entity.HasKey(x => x.Id);
      entity.OwnsOne(x => x.RuleName);
      entity.OwnsOne(x => x.Expression);
      entity.OwnsOne(x => x.Operator);
      entity.OwnsOne(x => x.ErrorMessage);
      entity.OwnsOne(x => x.SuccessEvent);
      entity.OwnsOne(x => x.Enabled);
      entity.OwnsOne(p => p.Author);
      entity.OwnsOne(p => p.SuccessCount);
      entity.OwnsOne(p => p.CreatedAt);
      entity.OwnsOne(p => p.ModifiedAt);
      entity.OwnsOne(p => p.FailureCount);
    });
    base.OnModelCreating(modelBuilder);
  }
}

