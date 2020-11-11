using ConsoleApp1.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.Configure(app =>
                {
                    var repo = MemoryRepository.Instance;
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapGet("api/todoitem", async context =>
                        {
                            var description = context.Request.Query["description"].FirstOrDefault();
                            var doneString = context.Request.Query["done"].FirstOrDefault();
                            var done = string.IsNullOrEmpty(doneString) ? (bool?)null : bool.Parse(doneString);

                            var models = await repo.QueryAsync(description, done);

                            var json = JsonConvert.SerializeObject(models, Formatting.Indented);
                            await context.Response.WriteAsync(json);
                        });

                        endpoints.MapMethods("api/todoitem/{id}", new[] { "PATCH" }, async context =>
                          {
                              using (var reader = new StreamReader(context.Request.Body))
                              {
                                  //get input
                                  var json = await reader.ReadToEndAsync();
                                  var model = JsonConvert.DeserializeObject<ToDoItemUpdateModel>(json);
                                  context.Request.RouteValues.TryGetValue("id", out var id);

                                  //check id
                                  if (string.IsNullOrEmpty(id.ToString()))
                                  {
                                      context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                      await context.Response.WriteAsync("Id is required");
                                      return;
                                  }

                                  var modelInDb = await repo.GetAsync(id.ToString());
                                  if (modelInDb == null)
                                  {
                                      context.Response.StatusCode = StatusCodes.Status404NotFound;
                                      await context.Response.WriteAsync($"Can't find [{id}]");
                                      return;
                                  }

                                  //update
                                  var updated = await repo.UpdateAsync(id.ToString(), model);

                                  //return
                                  var responseJson = JsonConvert.SerializeObject(updated, Formatting.Indented);
                                  await context.Response.WriteAsync(responseJson);
                              }
                          });

                    });
                });
            });
            builder.Build().Run();
        }


    }
}

/*
                        endpoints.MapDelete("api/todoitem/{id}", async context =>
                        {
                            context.Request.RouteValues.TryGetValue("id", out var id);
                            if (string.IsNullOrEmpty(id?.ToString()))
                            {
                                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                await context.Response.WriteAsync("Id is required");
                                return;
                            }
                            await repo.DeleteAsync(id.ToString());
                            context.Response.StatusCode = StatusCodes.Status204NoContent;
                        });

                        endpoints.MapPut("api/todoitem", async context =>
                        {
                            using (var reader = new StreamReader(context.Request.Body))
                            {
                                var json = await reader.ReadToEndAsync();
                                var model = JsonConvert.DeserializeObject<ToDoItem>(json);
                                if (string.IsNullOrEmpty(model?.Id))
                                {
                                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                    await context.Response.WriteAsync("Id is required");
                                    return;
                                }
                                await repo.UpsertAsync(model);
                            }
                        });
 */
