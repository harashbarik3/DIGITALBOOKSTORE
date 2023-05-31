using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CommonLib.Models;

public partial class Publisher
{
    public Guid PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
