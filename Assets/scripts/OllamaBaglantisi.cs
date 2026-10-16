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

public class OllamaBaglantisi : MonoBehaviour
{
    public async Task<MuhafizCevap> MuhafizlaKonus(string oyuncuMesaji)
    {
        string url = "http://localhost:11434/api/generate";

        string talimat = "Sen gizemli ve ciddi bir Muhafýzsýn. Çatý kapýsýný koruyorsun. " +
                         "Cevabýný SADECE þu formatta JSON olarak ver: " +
                         "{\"mesaj\":\"Muhafýzýn cevabý\", \"aksiyon\":\"kapiyi_ac veya yok\"}";

        string veri = "{\"model\": \"llama3.2:1b\", \"prompt\": \"" + oyuncuMesaji + "\", " +
                      "\"system\": \"" + talimat + "\", \"stream\": false, \"format\": \"json\"}";

        using (UnityWebRequest istek = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(veri);
            istek.uploadHandler = new UploadHandlerRaw(bodyRaw);
            istek.downloadHandler = new DownloadHandlerBuffer();
            istek.SetRequestHeader("Content-Type", "application/json");

            var operasyon = istek.SendWebRequest();
            while (!operasyon.isDone) await Task.Yield();

            if (istek.result == UnityWebRequest.Result.Success)
            {
                string hamCevap = istek.downloadHandler.text;
                return JsonUtility.FromJson<MuhafizCevap>(hamCevap);
            }
            return new MuhafizCevap { mesaj = "Muhafýz seninle konuþmak istemiyor...", aksiyon = "yok" };
        }
    }
}