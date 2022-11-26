using System.ComponentModel.DataAnnotations;
using Common.Enums;


namespace Common
{
    public class CustomerApiSettings
    {
        public const string Key = "CustomerApi";

        [Required]
        public string Url { get; set; }

        [Required]
        public string ApiKey { get; set; }

    }
}
