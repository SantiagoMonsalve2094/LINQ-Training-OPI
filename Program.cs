using LinqTraining.Data;
using LinqTraining.Models;

List<Order> orders = SampleData.GetOrders();

var customersWithNoCompletedOrders = orders
    .GroupBy(order => order.CustomerName)
    .Where(group => !group.Any(order => order.Status == "Completed"))
    .Select(group => group.Key)
    .OrderBy(customerName => customerName)
    .ToList();

Console.WriteLine("Customers with no completed orders:");

foreach (var customerName in customersWithNoCompletedOrders)
{
    Console.WriteLine($"- {customerName}");
}
