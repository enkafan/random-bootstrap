namespace RandomBootstrap.Services.Fonts
{
    public class FontPair
    {
        public FontPair(string heading, string body)
        {
            Heading = heading;
            Body = body;
        }

        public string Heading { get; }
        public string Body { get; }

        public string HeadingForUrl => ForUrl(Heading);
        public string BodyForUrl => ForUrl(Body);
        public string HeadingForCss => ForCss(Heading);
        public string BodyForCss => ForCss(Body);

        private static string ForUrl(string original)
        {
            return original.Replace(" ", "+");
        }

        private static string ForCss(string original)
        {
            return original.Contains(" ") ? $"'{original}'" : original;
        }
    }
}