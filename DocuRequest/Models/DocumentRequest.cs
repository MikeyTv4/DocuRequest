using System.ComponentModel.DataAnnotations;

namespace DocuRequest.Models;

public enum DocumentType
{
    [Display(Name = "Transcript of Records")] TranscriptOfRecords,
    [Display(Name = "Certificate of Enrollment")] CertificateOfEnrollment,
    [Display(Name = "Certificate of Good Moral Character")] GoodMoral,
    [Display(Name = "Certificate of Grades")] CertificateOfGrades,
    [Display(Name = "Honorable Dismissal")] HonorableDismissal,
    [Display(Name = "Diploma (Certified True Copy)")] DiplomaCopy
}

public enum RequestStatus
{
    Pending,
    Processing,
    [Display(Name = "Ready for Pickup")] ReadyForPickup,
    Released,
    Rejected
}

public class DocumentRequest
{
    public int Id { get; set; }

    [StringLength(20)]
    public string ReferenceNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "Student ID is required."), StringLength(20)]
    [Display(Name = "Student ID")]
    public string StudentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full name is required."), StringLength(100)]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required."), EmailAddress, StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Course / program is required."), StringLength(100)]
    [Display(Name = "Course / Program")]
    public string Course { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please choose a document.")]
    [Display(Name = "Document")]
    public DocumentType? DocumentType { get; set; }

    [Range(1, 10, ErrorMessage = "Copies must be between 1 and 10.")]
    public int Copies { get; set; } = 1;

    [Required(ErrorMessage = "Please state the purpose."), StringLength(250)]
    public string Purpose { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Preferred pickup date")]
    public DateTime? PickupDate { get; set; }

    public RequestStatus Status { get; set; } = RequestStatus.Pending;

    [StringLength(500)]
    public string? StaffRemarks { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
