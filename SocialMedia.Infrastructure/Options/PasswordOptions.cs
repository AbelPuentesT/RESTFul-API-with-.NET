namespace SocialMedia.Infrastructure.Options
{
    public class PasswordsOptions
    {
        public int SaltSize { get; set; }
        public int KeySize { get; set; }
        public int Iterations { get; set; }
    }
}
