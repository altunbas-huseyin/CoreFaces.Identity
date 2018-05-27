using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Identity.Models.Models
{
    public class IdentitySettings
    {
        public string FileUploadFolderPath { get; set; } = "";
        public string IdentityLicenseDomain { get; set; } = "";
        public string IdentityLicenseKey { get; set; } = "";
    }
}
