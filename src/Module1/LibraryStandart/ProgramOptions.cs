using MatthiWare.CommandLine.Core.Attributes;

namespace LibraryStandard.Options
{
    /// <summary>
    /// Options for command line arguments.
    /// </summary>
    internal class ProgramOptions
    {
        /// <summary>
        /// Gets or sets username.
        /// </summary>
        [Required]
        [Name("fn", "firstname")]
        [Description("User's first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets lastname.
        /// </summary>
        [Required]
        [Name("ln", "lastname")]
        [Description("User's last name")]
        public string LastName { get; set; }
    }
}
