using LinqTraining.Data;
using LinqTraining.Models;

List<Order> orders = SampleData.GetOrders();

var topCustomers = orders
    .GroupBy(order => order.CustomerName)
    .Select(group => new
    {
        Customer = group.Key,
        TotalAmount = group.Sum(order => order.Amount),
        TotalOrders = group.Count()
    })
    .OrderByDescending(result => result.TotalAmount)
    .Take(3)
    .ToList();

Console.WriteLine("Top 3 customers who bought the most:");

foreach (var customer in topCustomers)
{
    Console.WriteLine($"- {customer.Customer} | Amount: {customer.TotalAmount:C} | Orders: {customer.TotalOrders}");
}
