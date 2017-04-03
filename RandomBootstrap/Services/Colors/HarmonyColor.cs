using System.Collections.Generic;

namespace RandomBootstrap.Services.Colors
{
    public class HarmonyColor
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Hex { get; set; }
        public string ContrastColor { get; set; }
        public string[][] Harmony { get; set; }
        public string[] Similiar { get; set; }
        public string[] Shades { get; set; }
        public string[] Suggested { get; set; }

        public string[] AllHarmonyColors()
        {
            var colors = new List<string>();
            foreach (var s in Harmony)
            {
                colors.AddRange(s);
            }

            return colors.ToArray();
        }
    }
}