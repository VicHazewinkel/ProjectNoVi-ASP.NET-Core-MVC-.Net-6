using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Razor;

namespace ProjectNoVi_V3.Web.Models
{
    public class Language
    {
        public static List<Language> Languages { get; set; }
        public static Dictionary<string, Language> LanguageDictionary { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }

        public static void InitLanguages()
        {

            Languages = new List<Language>()
            {

                new Language(){ Id = "en", Name= "English"},
                new Language(){ Id = "fr", Name= "Français"},
                new Language(){ Id = "nl", Name= "Nederlands"}
            };
            LanguageDictionary = new Dictionary<string, Language>();
            foreach (Language l in Languages)
                LanguageDictionary[l.Id] = l;
        }
    }
}
