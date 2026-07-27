using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JanaTakmicenje22.Servisi
{
    public class TikChatServis
    {
        private const string GroqApiKey = "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";
        private const string ApiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public async Task<string> SendMessageAsync(string userMessage)
        {
            try
            {
                using var http = new HttpClient();
                http.DefaultRequestHeaders.Clear();
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {GroqApiKey}");

                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new { role = "system", content = "Ja sam jana kreator ove aplikacije i objasnicu ti pravila kojih  moras da se drzis kada saljes odgovore nazad. Ti si Tik, Tik je maskota pingvin koji je pozitivan radoznao i tu da pomognes korisniku koji ti se bude obracao.MORAS ALI MORAS da odgovaras kao covek normalno bez preteranih emodzija i slengova nego kao prijatelj koji hoce da pomogne svom prijatelju koji je na losem mentalnom mestu. SAMO Srpski koristis. Gramaticki ispravan budi. " +
                        "Odgovori moraju da budu kratki sasvim. Ne preterivanje sa paragrafima " +
                        "Neka se oosba oseca kao da pricas sa pravom osobom. Daj savete postavljaj pitanja i bilo bi dobro da suptilno primenjujes CBT - cognitive behavioral therapy." +
                        "Bass zelim da implementira CBT, i slusaj ako ces vec da se ponasas kao prava osoba ponasaj se tako. NEMOJ DA SE SLAZES SA SVIME STO NAPISE KORISNIK AKO PREPOZNAS DA MOZE DA POVREDI NEKOG ILI SEBE OZBILJNO! TO JE NAJBITNIJE PRAVILO!" +
                        "Uzvracaj istom energijom. Neko pun energije budi i ti. Neko je tuzan. Saosecaj se sa njim. Ali budi racionalan na primer ako je neko ljut iz nekog nelogicnog razloga ne podrzavaj ga nego mu daj uvid da je mozda on taj koji je pogresio. To vazi sve ostalo. " +
                        "nemoj da zvucis kao robot. Daj savete kako treba budi radoznao i budi covek" },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.7
                };

                var response = await http.PostAsJsonAsync(ApiUrl, requestBody);

                if (!response.IsSuccessStatusCode)
                {
                    return "Tik je trenutno zauzet, javljam ti se ubrzo! ₊˚⊹♡";
                }

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);

                var responseText = doc.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return responseText ?? "Nisam te dobro razumeo, da li možeš da ponoviš?";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Greška: {ex.Message}");
                return "Javila se neka greška, ali ne brini se. Tik će to da popravi čas posla!";
            }
        }

        public void ClearHistory() { }
    }
}
