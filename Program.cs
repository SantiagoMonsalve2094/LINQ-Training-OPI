using LinqTraining.Data;
using LinqTraining.Models;

List<Book> books = SampleData.GetBooks();
List<Order> orders = SampleData.GetOrders();

PrintOrdersFromLastMonthGroupedByStatus(orders);
PrintTop5BooksByGenre(books);
SearchBookByPartialTitle(books);
PrintTop3CustomersWhoBoughtMost(orders);
PrintCustomersWithNoCompletedOrders(orders);

static void PrintOrdersFromLastMonthGroupedByStatus(List<Order> orders)
{
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

    Console.WriteLine("\n1) Orders from the last month grouped by status:");

    foreach (var item in ordersByStatus)
    {
        Console.WriteLine($"- Status: {item.Status} | Orders: {item.TotalOrders} | Amount: {item.TotalAmount:C}");
    }
}

static void PrintTop5BooksByGenre(List<Book> books)
{
    var topBooksByGenre = books
        .GroupBy(book => book.Genre)
        .OrderBy(group => group.Key)
        .Select(group => new
        {
            Genre = group.Key,
            TopBooks = group
                .OrderByDescending(book => book.UnitsSold)
                .Take(5)
                .ToList()
        })
        .ToList();

    Console.WriteLine("\n2) Top 5 most sold books by genre:");

    foreach (var genreGroup in topBooksByGenre)
    {
        Console.WriteLine($"\n- Genre: {genreGroup.Genre}");

        foreach (var book in genreGroup.TopBooks)
        {
            Console.WriteLine($"  {book.Title} | Units sold: {book.UnitsSold}");
        }
    }
}

static void SearchBookByPartialTitle(List<Book> books)
{
    Console.Write("\n3) Write part of the book title: ");
    string partialName = Console.ReadLine()?.Trim() ?? string.Empty;

    var matchingBooks = books
        .Where(book => book.Title.Contains(partialName, StringComparison.OrdinalIgnoreCase))
        .OrderBy(book => book.Title)
        .ToList();

    Console.WriteLine("Search results:");

    if (!matchingBooks.Any())
    {
        Console.WriteLine("- No books found.");
        return;
    }

    foreach (var book in matchingBooks)
    {
        Console.WriteLine($"- {book.Title}");
    }
}

static void PrintTop3CustomersWhoBoughtMost(List<Order> orders)
{
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

    Console.WriteLine("\n4) Top 3 customers who bought the most:");

    foreach (var customer in topCustomers)
    {
        Console.WriteLine($"- {customer.Customer} | Amount: {customer.TotalAmount:C} | Orders: {customer.TotalOrders}");
    }
}

static void PrintCustomersWithNoCompletedOrders(List<Order> orders)
{
    var customersWithNoCompletedOrders = orders
        .GroupBy(order => order.CustomerName)
        .Where(group => !group.Any(order => order.Status == "Completed"))
        .Select(group => group.Key)
        .OrderBy(customerName => customerName)
        .ToList();

    Console.WriteLine("\n5) Customers with no completed orders:");

    foreach (var customerName in customersWithNoCompletedOrders)
    {
        Console.WriteLine($"- {customerName}");
    }
}
