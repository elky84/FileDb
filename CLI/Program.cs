
string databaseFilePath = "mydatabase.db";

var database = new FileDb.FileDatabase(databaseFilePath);

database.Set("key1", "value1");
database.Set("key2", "value2");
database.Set("key3", "value3");

var value1 = database.Get("key1");
Console.WriteLine($"[Value for key 'key1': {value1}]");

database.Set("key2", "newvalue2");
Console.WriteLine($"[insert key2 and PrintAll]");
database.PrintAll();

database.Delete("key1");
Console.WriteLine($"[delete key1 and PrintAll]");

database.PrintAll();

List<string> queryResult = database.Query(kvp => kvp.Value.EndsWith("3"));
Console.WriteLine("[Query result]");

foreach (string result in queryResult)
{
    Console.WriteLine(result);
}