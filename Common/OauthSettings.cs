using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class OauthSettings
    {
        public const string Key = "Oauth";

        [Required]
        public string AudienceId { get; set; }

        [Required]
        public string Issuer { get; set; }

        [Required]
        public string SecretKey { get; set; }

        [Range(1, 10)]
        public int AccessTokenExpirationInDays { get; set; }
    }
}
