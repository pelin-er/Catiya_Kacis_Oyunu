using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;

[Serializable]
public class MuhafizCevap
{
    public string mesaj;
    public string aksiyon;
}

// Ollama'nýn API'den döndürdüðü dýþ JSON yapýsý
[Serializable]
public class OllamaHamCevap
{
    public string response;
}

public class OllamaBaglantisi : MonoBehaviour
{
    public async Task<MuhafizCevap> MuhafizlaKonus(string oyuncuMesaji)
    {
        string url = "http://127.0.0.1:11434/api/generate";

        string talimat =
            "Sen gizemli ve ciddi bir Muhafýzsýn. Çatý kapýsýný koruyorsun. " +
            "Oyuncu seni ikna edici ve mantýklý bir þekilde izin vermeye ikna ederse aksiyon olarak kapiyi_ac yaz. " +
            "Ýkna olmadýysan aksiyon olarak yok yaz. " +
            "Cevabýný SADECE þu formatta JSON olarak ver: " +
            "{\"mesaj\":\"Muhafýzýn cevabý\", \"aksiyon\":\"kapiyi_ac veya yok\"}";

        string veri =
            "{\"model\":\"llama3.2:1b\"," +
            "\"prompt\":\"" + oyuncuMesaji + "\"," +
            "\"system\":\"" + talimat.Replace("\"", "\\\"") + "\"," +
            "\"stream\":false," +
            "\"format\":\"json\"}";

        using (UnityWebRequest istek = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(veri);

            istek.uploadHandler = new UploadHandlerRaw(bodyRaw);
            istek.downloadHandler = new DownloadHandlerBuffer();
            istek.SetRequestHeader("Content-Type", "application/json");

            var operasyon = istek.SendWebRequest();

            // Ollama cevap verirken Unity'nin donmasýný engeller
            while (!operasyon.isDone)
                await Task.Yield();

            if (istek.result == UnityWebRequest.Result.Success)
            {
                string hamCevap = istek.downloadHandler.text;

                // Önce Ollama'nýn dýþ cevabýný oku
                OllamaHamCevap ollamaCevabi =
                    JsonUtility.FromJson<OllamaHamCevap>(hamCevap);

                // Ardýndan response içindeki JSON'u MuhafizCevap'a dönüþtür
                return JsonUtility.FromJson<MuhafizCevap>(
                    ollamaCevabi.response
                );
            }

            Debug.LogError("Ollama baðlantý hatasý: " + istek.error);

            return new MuhafizCevap
            {
                mesaj = "Muhafýz seninle konuþmak istemiyor...",
                aksiyon = "yok"
            };
        }
    }
}