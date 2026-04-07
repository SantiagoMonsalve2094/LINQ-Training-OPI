using LinqTraining.Data;
using LinqTraining.Models;

List<Book> books = SampleData.GetBooks();

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

Console.WriteLine("Top 5 most sold books by genre:");

foreach (var genreGroup in topBooksByGenre)
{
    Console.WriteLine($"\nGenre: {genreGroup.Genre}");

    foreach (var book in genreGroup.TopBooks)
    {
        Console.WriteLine($"- {book.Title} | Units sold: {book.UnitsSold}");
    }
}
