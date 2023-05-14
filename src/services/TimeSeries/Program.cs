// ***********************************************************************
// Assembly         : CV.Partner.Service
// Author           : matias.ramsland
// Created          : 02-02-2023
//
// Last Modified By : matias.ramsland
// Last Modified On : 03-14-2023
// ***********************************************************************
// <copyright file="Program.cs" company="CV.Partner.Service">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
WebApplication? app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => Results.LocalRedirect("~/swagger"));
app.MapControllers();
app.UseCors("CorsPolicy");
app.Run();
