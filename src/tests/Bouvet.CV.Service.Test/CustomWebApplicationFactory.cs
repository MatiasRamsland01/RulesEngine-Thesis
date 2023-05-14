using Bouvet.CV.Service.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class {
  protected override void ConfigureWebHost(IWebHostBuilder builder) {
    builder.ConfigureServices(services => {
      var dbContextDescriptor = services.SingleOrDefault(
          d => d.ServiceType ==
              typeof(DbContextOptions<RuleengineContext>));
      services.Remove(dbContextDescriptor!);
      var dbConnectionDescriptor = services.SingleOrDefault(
          d => d.ServiceType ==
              typeof(DbConnection));
      services.Remove(dbConnectionDescriptor!);
      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(container => {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
      });

      services.AddDbContext<RuleengineContext>((container, options) => {
        var connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
      });
      //   var daprMock = new Mock<DaprClient>();

      //   // Create a list with a single Response object
      //   var responseList = new List<Response> { new Response { Name = "Test" } };

      //   // Wrap the list in a StateEntry object
      //   var stateEntry = new StateEntry<List<Response>>(daprMock.Object, "stateStoreName", "stateKeyName", responseList, "etag");

      //   // Set up the mock to return the stateEntry when GetStateEntryAsync is called
      //   daprMock.Setup(d => d.GetStateEntryAsync<List<Response>>(
      //       It.IsAny<string>(),
      //       It.IsAny<string>(),
      //       It.IsAny<ConsistencyMode>(),
      //       It.IsAny<Dictionary<string, string>>(),
      //       It.IsAny<CancellationToken>()))
      //       .ReturnsAsync(stateEntry);
      //   services.AddSingleton<DaprClient>(daprMock.Object);
    });

    builder.UseEnvironment("Development");
  }
}
