using UnityEngine;

public class MuhafizNPC : MonoBehaviour
{
    public OllamaBaglantisi baglanti;
    public YanginKapisi catiKapisi; 

    public async void SohbetEt(string oyuncuSozu)
    {
        Debug.Log("Muhafýz düþünüyor...");

        // AI'dan cevap bekleniyor
        MuhafizCevap yanit = await baglanti.MuhafizlaKonus(oyuncuSozu);

        Debug.Log("Muhafýz: " + yanit.mesaj);

        if (yanit.aksiyon == "kapiyi_ac")
        {
            catiKapisi.KapiyiAc();
            Debug.Log("Muhafýz ikna oldu, kapý açýlýyor.");
        }
    }
}