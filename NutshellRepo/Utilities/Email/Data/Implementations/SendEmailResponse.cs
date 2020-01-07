using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutshellRepo.Utilities.Email.Data.Implementations
{
    /// <summary>
    /// Successful if no errors occured. Contains List of Errors if applicable.
    /// </summary>
    public class SendEmailResponse    {
        public bool Successful => !(Errors?.Count > 0);
        public List<string> Errors { get; set; }
    }
}
