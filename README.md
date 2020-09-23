# "ExecuteScalarSqlInterpolated" Sample

This repository describe how to implement "ExecuteScalarSqlInterpolatedAsync" extension method that is scalar version of the "ExecuteSqlInterpolatedAsync" extension method in the EFCore 3.x.

```csharp
var name = "Foo";
var id = await db.Database.ExecuteScalarSqlInterpolatedAsync<int>($@"
  INSERT INTO People(Name)
  OUTPUT INSERTED.Id 
  VALUES({name})");
```

To show the implementation of the "ExecuteScalarSqlInterpolatedAsync", click the following link.

- https://github.com/sample-by-jsakamoto/EFCore-ExecuteScalarSqlInterpolatedSample/blob/master/ExecuteScalarSqlInterpolatedSample/EFCoreExtensions.cs#L13

## License

[MIT License](LICENSE)