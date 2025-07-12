using System.ComponentModel.DataAnnotations;

namespace UmbracoDemoSite.Core.Features.Shared.Components.ContactForm
{
    
    public class ContactFormModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string? Comment { get; set; }
    }
}
