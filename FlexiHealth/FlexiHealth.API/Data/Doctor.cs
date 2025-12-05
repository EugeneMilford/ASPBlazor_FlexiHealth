using System;
using System.Collections.Generic;

namespace FlexiHealth.API.Data;

public partial class Doctor
{
    public int DoctorId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public DateTime? DateJoined { get; set; }

    public string? Bio { get; set; }

    public string? ProfileImageUrl { get; set; }
}
