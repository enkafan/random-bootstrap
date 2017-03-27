using Newtonsoft.Json;

namespace RandomBootstrap.Services.Colors
{
    public class MaterialDesignColors
    {
        public Red red { get; set; }
        public Pink pink { get; set; }
        public Purple purple { get; set; }
        public Deeppurple deeppurple { get; set; }
        public Indigo indigo { get; set; }
        public Blue blue { get; set; }
        public Lightblue lightblue { get; set; }
        public Cyan cyan { get; set; }
        public Teal teal { get; set; }
        public Green green { get; set; }
        public Lightgreen lightgreen { get; set; }
        public Lime lime { get; set; }
        public Yellow yellow { get; set; }
        public Amber amber { get; set; }
        public Orange orange { get; set; }
        public Deeporange deeporange { get; set; }
        public Brown brown { get; set; }
        public Grey grey { get; set; }
        public Bluegrey bluegrey { get; set; }

        public interface IColor
        {
            [JsonProperty("50")]
            string _50 { get; set; }

            [JsonProperty("100")]
            string _100 { get; set; }

            [JsonProperty("200")]
            string _200 { get; set; }

            [JsonProperty("300")]
            string _300 { get; set; }

            [JsonProperty("400")]
            string _400 { get; set; }

            [JsonProperty("500")]
            string _500 { get; set; }

            [JsonProperty("600")]
            string _600 { get; set; }

            [JsonProperty("700")]
            string _700 { get; set; }

            [JsonProperty("800")]
            string _800 { get; set; }

            [JsonProperty("900")]
            string _900 { get; set; }
        }

        public class Red : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Pink : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Purple : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Deeppurple : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Indigo : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Blue : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Lightblue : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Cyan : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Teal : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Green : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Lightgreen : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Lime : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Yellow : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Amber : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Orange : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Deeporange : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
            public string a100 { get; set; }
            public string a200 { get; set; }
            public string a400 { get; set; }
            public string a700 { get; set; }
        }

        public class Brown : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
        }

        public class Grey : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
        }

        public class Bluegrey : IColor
        {
            public string _50 { get; set; }
            public string _100 { get; set; }
            public string _200 { get; set; }
            public string _300 { get; set; }
            public string _400 { get; set; }
            public string _500 { get; set; }
            public string _600 { get; set; }
            public string _700 { get; set; }
            public string _800 { get; set; }
            public string _900 { get; set; }
        }
    }
}