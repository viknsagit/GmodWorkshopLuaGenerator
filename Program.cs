using System.Diagnostics;
using System.Text;
using HtmlAgilityPack;

Console.WriteLine("Enter collection URL");

var collectionUrl = Console.ReadLine();
var parser = new HtmlWeb();
var page = parser.Load(collectionUrl);
var addonsCollection = page.DocumentNode.SelectNodes("//div[@class='collectionItem']");

Console.WriteLine("Addons count: " + addonsCollection.Count);
Console.WriteLine("Generating workshop.lua file");

var builder = new StringBuilder();
foreach (var item in addonsCollection)
{
    builder.AppendLine($"resource.AddWorkshop(\"{item.Id.Split('_')[1]}\")");
}

Console.WriteLine(builder.ToString());
Console.WriteLine("Save to file? (y/n)");

var action = Console.ReadLine();
if (action is "y" or "yes" or "")
    CreateWorkshopFile(builder.ToString());
Process.Start("explorer.exe", Environment.CurrentDirectory);
return;


void CreateWorkshopFile(string text)
{
    var path = Environment.CurrentDirectory + @"\workshop.lua";
    if (File.Exists(path))
    {
        Console.WriteLine($"File {path} already exists");
        return;
    }
    File.WriteAllText(path, text);
    Console.WriteLine($"File {path} Saved");
}