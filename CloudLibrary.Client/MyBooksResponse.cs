using System.Text.Json.Serialization;

namespace CloudLibrary.Client;

public class MyBooksResponse
{
    public MyBookListItem[] Value { get; init; } = [];

    public record MyBookListItem
    {
        public int Id { get; init; }

        [JsonPropertyName("book_id")]
        public int BookId { get; init; }

        public string Title { get; init; } = string.Empty;

        public int? Pages { get; init; }

        public int? Year { get; init; }

        public string Authors { get; init; } = string.Empty;

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } = string.Empty;
    }
}