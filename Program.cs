using LinqTraining.Data;
using LinqTraining.Models;

List<Order> orders = SampleData.GetOrders();

DateTime oneMonthAgo = DateTime.Today.AddMonths(-1);

var ordersByStatus = orders
    .Where(order => order.Date >= oneMonthAgo)
    .GroupBy(order => order.Status)
    .Select(group => new
    {
        Status = group.Key,
        TotalOrders = group.Count(),
        TotalAmount = group.Sum(order => order.Amount)
    })
    .OrderBy(result => result.Status)
    .ToList();

Console.WriteLine("Orders from the last month grouped by status:");

foreach (var item in ordersByStatus)
{
    Console.WriteLine($"Status: {item.Status} | Orders: {item.TotalOrders} | Amount: {item.TotalAmount:C}");
}
