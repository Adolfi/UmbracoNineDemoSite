using Our.Umbraco.DictionaryLocalizer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoTenDemoSite.Core.Features.Contact
{
    public class ContactFormModel : DictionaryDataAnnotationBaseModel
    {
        [Required(ErrorMessage = "#RequiredErrorMessage.name")]
        [Display(Name = "#nameLabel")]
        public string? FirstAndLastName { get; set; }

        [Required(ErrorMessage = "#RequiredErrorMessage.email")]
        [EmailAddress(ErrorMessage = "#InvalidErrorMessage.email")]
        [Display(Name = "#emailLabel")]
        public string? Email { get; set; }

        [Display(Name = "#subjectLabel")]
        [Required(ErrorMessage = "#RequiredErrorMessage.subject")]
        public string? Subject { get; set; }

        [Display(Name = "#messageLabel")]
        [Required(ErrorMessage = "#RequiredErrorMessage.message")]
        public string? Message { get; set; }

        public bool DataConsent { get; set; }
        [Required(ErrorMessage = "#RequiredErrorMessage.dataConsent")]
        public string? DataConsentText { get; set; }
    }
}
