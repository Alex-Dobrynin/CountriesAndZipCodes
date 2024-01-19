using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1;

class Program
{
    class C
    {
        public string Name { get; set; }
        public string Alpha2Code { get; set; }
        public string NativeName { get; set; }
    }

    public static async Task Main(string[] args)
    {
        var countriesPath = @"..\..\..\RestCountries.json";
        var countriesPathSmall = @"..\..\..\RestCountriesSmall.json";

        var text = await File.ReadAllTextAsync(countriesPath);

        var countries = JsonSerializer.Deserialize<List<Country>>(text, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var smallList = new List<CountrySmall>();

        foreach (var country in countries)
        {
            smallList.Add(new CountrySmall
            {
                Cca2 = country.Cca2,
                Cca3 = country.Cca3,
                Idd = country.Idd,
                Flag = country.Flags.Png,
                Name = country.Name.Common,
                NativeName = country.Name.CommonNativeName,
                Zip = country.Zip
            });
        }

        await JsonSerializer.SerializeAsync(File.OpenWrite(countriesPathSmall), smallList, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
    }

    //public static void Main(string[] args)
    //{
    //    var countriesPath = @"..\..\..\RestCountries.json";
    //    var postalCodesPath = @"..\..\..\WorldPostalCodes.json";

    //    var text = File.ReadAllText(countriesPath);
    //    var countries = JsonSerializer.Deserialize<List<Country>>(text, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    //    text = File.ReadAllText(postalCodesPath);
    //    var zipCodes = JsonSerializer.Deserialize<List<WorldPostalCode>>(text, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

    //    foreach (var country in countries)
    //    {
    //        var zip = zipCodes.Find(z => z.Cca2 == country.Cca2);
    //        if (zip is not null)
    //        {
    //            country.PostalCode = string.IsNullOrWhiteSpace(zip.Regex)
    //                ? null
    //                : new PostalCode()
    //                {
    //                    Cca2 = zip.Cca2,
    //                    Format = zip.Format,
    //                    Note = zip.Note,
    //                    Regex = zip.Regex
    //                };
    //        }
    //    }

    //    JsonSerializer.Serialize(File.OpenWrite(countriesPath), countries, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
    //}

    //public static void Main(string[] args)
    //{
    //    var countriesPath = @"..\..\..\RestCountries.json";
    //    var countriesPathNew = @"..\..\..\RestCountriesNew.json";
    //    var usPostalCodesCsv = @"..\..\..\zip_code_database.csv";
    //    var usPostalCodesJson = @"..\..\..\USPostalCodes.json";

    //    var usPostalCodes = new List<UsPostalCode>();
    //    foreach (var line in File.ReadLines(usPostalCodesCsv))
    //    {
    //        var splitted = line.Split(',');
    //        var zip = splitted[0];
    //        var city = splitted[1];
    //        var state = splitted[2];

    //        usPostalCodes.Add(new UsPostalCode()
    //        {
    //            City = city,
    //            State = state,
    //            Zip = zip
    //        });
    //    }

    //    usPostalCodes = usPostalCodes.OrderBy(c => c.State).ToList();

    //    JsonSerializer.Serialize(File.OpenWrite(usPostalCodesJson), usPostalCodes, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
    //}
}