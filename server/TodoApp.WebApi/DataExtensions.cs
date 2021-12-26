using Microsoft.EntityFrameworkCore;
using TodoApp.Db;

namespace TodoApp.WebApi
{
  public static class DataExtensions
  {
    public static WebApplication ApplyMigrations(this WebApplication app)
    {
      using var scope = app.Services.CreateScope();
      var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
      db.Database.Migrate();
      return app;
    }
  }
}
