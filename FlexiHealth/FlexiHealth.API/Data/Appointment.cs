using System;
using System.Collections.Generic;

namespace FlexiHealth.API.Data;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime? AppointmentDateTime { get; set; }

    public string? AppointmentType { get; set; }

    public string? Notes { get; set; }

    public int? DoctorId { get; set; }

    public string? Status { get; set; }

    public virtual Doctor? Doctor { get; set; }
}
