using MatthiWare.CommandLine.Core.Attributes;

namespace ConsoleApp
{
    public class ProgramOptions
    {
        [Required]
        [Name("fn", "firstname")]
        [Description("User's first name")]
        public string FirstName { get; set; }

        [Required]
        [Name("ln", "lastname")]
        [Description("User's last name")]
        public string LastName { get; set; }
    }
}
