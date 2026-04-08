using LinqTraining.Data;
using LinqTraining.Models;

List<Book> books = SampleData.GetBooks();
List<Order> orders = SampleData.GetOrders();

var OrdersLastMonthByStatus = orders
    .Where(x => x.Date >= DateTime.Today.AddMonths(-1))
    .GroupBy(x => x.Status)
    .Select(g => new
    {
        Status = g.Key,
        TotalOrders = g.Count(),
        TotalAmount = g.Sum(x => x.Amount)
    })
    .OrderBy(x => x.Status)
    .ToList();

Console.WriteLine("1) Orders last month by status");
foreach (var item in OrdersLastMonthByStatus)
{
    Console.WriteLine($"- {item.Status} | Orders: {item.TotalOrders} | Amount: {item.TotalAmount:C}");
}

var Top5ByGenre = books
    .GroupBy(x => x.Genre)
    .OrderBy(x => x.Key)
    .Select(g => new
    {
        Genre = g.Key,
        Books = g.OrderByDescending(x => x.UnitsSold).Take(5).ToList()
    })
    .ToList();

Console.WriteLine("\n2) Top 5 by genre");
foreach (var group in Top5ByGenre)
{
    Console.WriteLine($"\n- {group.Genre}");
    foreach (var book in group.Books)
    {
        Console.WriteLine($"  {book.Title} | Sold: {book.UnitsSold}");
    }
}

Console.Write("\n3) Search title (partial text): ");
string text = Console.ReadLine()?.Trim() ?? string.Empty;

var SearchTitle = books
    .Where(x => x.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
    .OrderBy(x => x.Title)
    .ToList();

Console.WriteLine("Results:");
if (!SearchTitle.Any())
{
    Console.WriteLine("- No books found");
}
else
{
    foreach (var book in SearchTitle)
    {
        Console.WriteLine($"- {book.Title}");
    }
}

var Top3Customers = orders
    .GroupBy(x => x.CustomerName)
    .Select(g => new
    {
        Customer = g.Key,
        TotalAmount = g.Sum(x => x.Amount),
        TotalOrders = g.Count()
    })
    .OrderByDescending(x => x.TotalAmount)
    .Take(3)
    .ToList();

Console.WriteLine("\n4) Top 3 customers");
foreach (var item in Top3Customers)
{
    Console.WriteLine($"- {item.Customer} | Amount: {item.TotalAmount:C} | Orders: {item.TotalOrders}");
}

var NoCompletedOrders = orders
    .GroupBy(x => x.CustomerName)
    .Where(g => !g.Any(x => x.Status == "Completed"))
    .Select(g => g.Key)
    .OrderBy(x => x)
    .ToList();

Console.WriteLine("\n5) Customers with no completed orders");
foreach (var customer in NoCompletedOrders)
{
    Console.WriteLine($"- {customer}");
}
