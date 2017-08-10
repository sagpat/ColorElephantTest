using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobPortal.Models
{
    public class Index
    {
            [Required]
            [Display(Name = "Name")]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.EmailAddress)]            
            [Display(Name = "Mail Address")]
            public string Website { get; set; }
      
            [Display(Name = "Cover Letter")]
            public string CoverLetter { get; set; }

            [Display(Name="Please Upload a Resume")]
            public HttpPostedFileBase File { get; set; }

            [Required(ErrorMessage = "A pdf is required"), FileExtensions(ErrorMessage = "Please upload a pdf file.")]
            public string FileName
            {
                get
                {
                    if (File != null)
                        return File.FileName;
                    else
                        return String.Empty;
                }
            }


            [Display(Name = "Do you like working with us?")]
            public bool WorkWithUs { get; set; }
        
    }
}