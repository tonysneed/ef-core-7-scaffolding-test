# EF Core 7 Scaffolding Test

EF Core 7 default scaffolding test

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0).
- [EF Core 7 CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet).
- Create NorthwindSlim database in [LocalDb](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16).
- Clone https://github.com/TrackableEntities/northwind-slim
- Run NorthwindSlim.sql to create and populate tables.

### Steps

1. Create a .NET 7 Class Library
2. Add EF Core packages
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Design
3. Open a command prompt at the project root.
4. Run the EF scaffolding command.

```
dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=NorthwindSlim; Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -o Models -c NorthwindSlimContext -f --context-dir Contexts
```

### Result

- NorthwindSlimContext

```csharp
public partial class NorthwindSlimContext : DbContext
{
    public NorthwindSlimContext()
    {
    }

    public NorthwindSlimContext(DbContextOptions<NorthwindSlimContext> options)
        : base(options)
    {
    }
```