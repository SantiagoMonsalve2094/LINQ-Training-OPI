using LinqTraining.Data;
using LinqTraining.Models;

List<Book> books = SampleData.GetBooks();

Console.Write("Write part of the book title: ");
string partialName = Console.ReadLine()?.Trim() ?? string.Empty;

var matchingBooks = books
    .Where(book => book.Title.Contains(partialName, StringComparison.OrdinalIgnoreCase))
    .OrderBy(book => book.Title)
    .ToList();

Console.WriteLine("\nSearch results:");

if (!matchingBooks.Any())
{
    Console.WriteLine("No books found.");
}
else
{
    foreach (var book in matchingBooks)
    {
        Console.WriteLine($"- {book.Title}");
    }
}
