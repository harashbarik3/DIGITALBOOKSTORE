﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CommonLib.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Guid RoleId { get; set; }

    public string? Email { get; set; }

    public string? UserType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; } = new List<Book>();

    public virtual Role Role { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();
}
