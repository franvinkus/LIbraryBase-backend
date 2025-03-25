using System;
using System.Collections.Generic;

namespace Library.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string? Description { get; set; }

    public bool? Availability { get; set; }

    public DateTime? AvailabilityDate { get; set; }

    public string? Img { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
