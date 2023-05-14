using Microsoft.AspNetCore.Builder;

namespace fields.Extensions {
  public static class FieldsExtention {
    public static void AddFields(this WebApplicationBuilder builder) {
      //Adds Fields to IOC with given validator and with config in config file
      builder.Services.AddIntegerFieldToIOC(builder.Configuration);
      builder.Services.AddStringFieldToIOC(builder.Configuration);
      builder.Services.AddBoolFieldToIOC(builder.Configuration);
      builder.Services.AddCharFieldToIOC(builder.Configuration);
      builder.Services.AddAgeFieldToIOC(builder.Configuration);
      builder.Services.AddDateFieldToIOC(builder.Configuration);
      builder.Services.AddDateRangeFieldToIOC(builder.Configuration);
      builder.Services.AddLimitFieldToIOC(builder.Configuration);
      builder.Services.AddIdFieldToIOC(builder.Configuration);
      builder.Services.AddNameFieldToIOC(builder.Configuration);
      builder.Services.AddUriFieldToIOC(builder.Configuration);
      builder.Services.AddEmailFieldToIOC(builder.Configuration);
    }
  }
}
