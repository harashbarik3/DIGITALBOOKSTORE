using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CommonLib.Models;

public partial class Category
{
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
