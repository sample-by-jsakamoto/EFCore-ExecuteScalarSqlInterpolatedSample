using System;
using System.Threading.Tasks;
using ExecuteScalarInterpolatedSample.Data;
using Microsoft.EntityFrameworkCore;

namespace ExecuteScalarInterpolatedSample
{
    class Program
    {
        static async Task Main()
        {
            var dbOptions = new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer($"Server=(localdb)\\MSSQLLocalDb;Database={Guid.NewGuid().ToString("N")};Integrated Security=True;")
                .Options;
            using var db = new MyDbContext(dbOptions);
            await db.Database.EnsureCreatedAsync();

            try
            {
                var names = new[] { "Foo", "Bar", "Fizz", "Buzz" };
                foreach (var name in names)
                {
                    var id = await db.Database.ExecuteScalarSqlInterpolatedAsync<int>(
                        $"INSERT INTO People(Name) OUTPUT INSERTED.Id VALUES({name})");

                    Console.WriteLine($"The id of \"{name}\" is: {id}");
                }
            }
            finally
            {
                await db.Database.EnsureDeletedAsync();
            }
        }
    }
}
