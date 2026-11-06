using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class MuhafizCevap
{
    public string mesaj;
    public string aksiyon;
    public bool hataVarMi = false;    
}

// Ollama'nýn dýþ cevabý
[Serializable]
public class OllamaHamCevap
{
    public string response;
}

// Ollama'ya gönderilen paket 
[Serializable]
public class OllamaIstek
{
    public string model;
    public string prompt;
    public string system;
    public bool stream;
    public string format;
}

public class OllamaBaglantisi : MonoBehaviour
{
    public string adres = "http://127.0.0.1:11434/api/generate";
    public string modelAdi = "llama3.2:1b";
    public int zamanAsimi = 60;      

    public bool mesgulMu = false;      

    string gecmis = "";                 // önceki konuþma, Muhafýz hatýrlasýn

    string talimat =
        "Sen gizemli ve ciddi bir Muhafýzsýn. Çatý kapýsýný koruyorsun. " +
        "Oyuncu seni ikna edici ve mantýklý bir þekilde izin vermeye ikna ederse aksiyon olarak kapiyi_ac yaz. " +
        "Sadece rica etmek, tehdit etmek, para teklif etmek ya da konu dýþý konuþmak seni ikna etmez, bu durumda aksiyon olarak yok yaz. " +
        "Her zaman Türkçe konuþ, cevabýn en fazla 2 cümle olsun. " +
        "Cevabýný SADECE þu formatta JSON olarak ver: " +
        "{\"mesaj\":\"Muhafýzýn cevabý\", \"aksiyon\":\"kapiyi_ac veya yok\"}";

    public async Task<MuhafizCevap> MuhafizlaKonus(string oyuncuMesaji)
    {
        mesgulMu = true;

        OllamaIstek paket = new OllamaIstek();
        paket.model = modelAdi;
        paket.system = talimat;
        paket.prompt = gecmis + "Oyuncu: " + oyuncuMesaji + "\nMuhafýz:";
        paket.stream = false;
        paket.format = "json";

        string veri = JsonUtility.ToJson(paket);

        using (UnityWebRequest istek = new UnityWebRequest(adres, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(veri);

            istek.uploadHandler = new UploadHandlerRaw(bodyRaw);
            istek.downloadHandler = new DownloadHandlerBuffer();
            istek.SetRequestHeader("Content-Type", "application/json");
            istek.timeout = zamanAsimi;

            var operasyon = istek.SendWebRequest();

            while (operasyon.isDone == false)
            {
                await Task.Yield();
            }

            mesgulMu = false;

            if (istek.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Ollama baðlantý hatasý: " + istek.error);
                return HataCevabi("Muhafýz þu an cevap vermiyor. Ollama açýk mý? (" + istek.error + ")");
            }

            MuhafizCevap cevap = null;

            try
            {
                OllamaHamCevap ham = JsonUtility.FromJson<OllamaHamCevap>(istek.downloadHandler.text);
                cevap = JsonUtility.FromJson<MuhafizCevap>(ham.response);
            }
            catch (Exception)
            {
                cevap = null;
            }

            if (cevap == null || string.IsNullOrEmpty(cevap.mesaj))
            {
                return HataCevabi("Muhafýz anlaþýlmaz bir þey söyledi. Tekrar dene.");
            }

            gecmis = gecmis + "Oyuncu: " + oyuncuMesaji + "\nMuhafýz: " + cevap.mesaj + "\n";

            if (gecmis.Length > 600)
            {
                gecmis = gecmis.Substring(gecmis.Length - 600);
            }

            cevap.hataVarMi = false;
            return cevap;
        }
    }

    MuhafizCevap HataCevabi(string hataMetni)
    {
        MuhafizCevap hata = new MuhafizCevap();
        hata.mesaj = hataMetni;
        hata.aksiyon = "yok";
        hata.hataVarMi = true;
        return hata;
    }
}