using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MtgRandomCard
{
    // Models to deserialize the Scryfall API response
    public class MtgCard
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("mana_cost")]
        public string? ManaCost { get; set; }

        [JsonPropertyName("cmc")]
        public double Cmc { get; set; }

        [JsonPropertyName("type_line")]
        public string? TypeLine { get; set; }

        [JsonPropertyName("rarity")]
        public string? Rarity { get; set; }

        [JsonPropertyName("set_name")]
        public string? SetName { get; set; }

        [JsonPropertyName("set")]
        public string? Set { get; set; }

        [JsonPropertyName("artist")]
        public string? Artist { get; set; }

        [JsonPropertyName("oracle_text")]
        public string? OracleText { get; set; }

        [JsonPropertyName("flavor_text")]
        public string? FlavorText { get; set; }

        [JsonPropertyName("power")]
        public string? Power { get; set; }

        [JsonPropertyName("toughness")]
        public string? Toughness { get; set; }

        [JsonPropertyName("loyalty")]
        public int? Loyalty { get; set; }

        [JsonPropertyName("colors")]
        public string[]? Colors { get; set; }

        [JsonPropertyName("color_identity")]
        public string[]? ColorIdentity { get; set; }

        [JsonPropertyName("keywords")]
        public string[]? Keywords { get; set; }

        [JsonPropertyName("image_uris")]
        public ImageUris? ImageUris { get; set; }
    }

    public class ImageUris
    {
        [JsonPropertyName("normal")]
        public string? Normal { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            await GetRandomMtgCard();
        }

        static async Task GetRandomMtgCard()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Set User-Agent header (required by Scryfall API)
                    client.DefaultRequestHeaders.Add("User-Agent", "MtgRandomCard/1.0");

                    string url = "https://api.scryfall.com/cards/random";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    MtgCard? card = JsonSerializer.Deserialize<MtgCard>(jsonContent, options);

                    DisplayCardDetails(card);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Error fetching card: {e.Message}");
                }
                catch (JsonException e)
                {
                    Console.WriteLine($"Error parsing response: {e.Message}");
                }
            }
        }

        static void DisplayCardDetails(MtgCard? card)
        {            if (card == null) return;
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Card Name: {card.Name ?? "N/A"}");
            Console.WriteLine($"Mana Cost: {card.ManaCost ?? "N/A"}");
            Console.WriteLine($"Converted Mana Cost: {card.Cmc}");
            Console.WriteLine($"Type Line: {card.TypeLine ?? "N/A"}");
            Console.WriteLine($"Rarity: {card.Rarity ?? "N/A"}");
            Console.WriteLine($"Set: {card.SetName ?? "N/A"} ({card.Set ?? "N/A"})");
            Console.WriteLine($"Artist: {card.Artist ?? "N/A"}");

            // Oracle text (card rules)
            if (!string.IsNullOrEmpty(card.OracleText))
            {
                Console.WriteLine($"\nOracle Text:\n{card.OracleText}");
            }

            // Flavor text
            if (!string.IsNullOrEmpty(card.FlavorText))
            {
                Console.WriteLine($"\nFlavor Text:\n{card.FlavorText}");
            }

            // Power/Toughness for creatures
            if (!string.IsNullOrEmpty(card.Power) && !string.IsNullOrEmpty(card.Toughness))
            {
                Console.WriteLine($"\nPower/Toughness: {card.Power}/{card.Toughness}");
            }

            // Loyalty for planeswalkers
            if (card.Loyalty.HasValue)
            {
                Console.WriteLine($"Loyalty: {card.Loyalty}");
            }

            // Colors
            if (card.Colors != null && card.Colors.Length > 0)
            {
                Console.WriteLine($"Colors: {string.Join(", ", card.Colors)}");
            }

            // Color identity
            if (card.ColorIdentity != null && card.ColorIdentity.Length > 0)
            {
                Console.WriteLine($"Color Identity: {string.Join(", ", card.ColorIdentity)}");
            }

            // Keywords
            if (card.Keywords != null && card.Keywords.Length > 0)
            {
                Console.WriteLine($"Keywords: {string.Join(", ", card.Keywords)}");
            }

            // Image URL
            if (card.ImageUris != null && !string.IsNullOrEmpty(card.ImageUris.Normal))
            {
                Console.WriteLine($"\nImage URL: {card.ImageUris.Normal}");
            }

            Console.WriteLine(new string('=', 50));
        }
    }
}
