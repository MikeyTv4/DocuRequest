using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace DocuRequest.Models;

public static class EnumExtensions
{
    public static string DisplayName(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
        return member?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
    }

    public static string BadgeClass(this RequestStatus status) => status switch
    {
        RequestStatus.Pending => "bg-warning-subtle text-warning-emphasis",
        RequestStatus.Processing => "bg-info-subtle text-info-emphasis",
        RequestStatus.ReadyForPickup => "bg-primary-subtle text-primary-emphasis",
        RequestStatus.Released => "bg-success-subtle text-success-emphasis",
        RequestStatus.Rejected => "bg-danger-subtle text-danger-emphasis",
        _ => "bg-secondary-subtle text-secondary-emphasis"
    };

    /// <summary>Position on the progress timeline (0-3). Rejected returns -1.</summary>
    public static int StepIndex(this RequestStatus status) => status switch
    {
        RequestStatus.Pending => 0,
        RequestStatus.Processing => 1,
        RequestStatus.ReadyForPickup => 2,
        RequestStatus.Released => 3,
        _ => -1
    };
}
