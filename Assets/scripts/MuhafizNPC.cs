using UnityEngine;

public class MuhafizNPC : MonoBehaviour
{
    public OllamaBaglantisi baglanti;
    public CikisKapisi catiKapisi;

    async void Start()
    {
        MuhafizCevap yanit = await baglanti.MuhafizlaKonus(
            "Çatýya çýkmama izin verir misin?"
        );

        Debug.Log("TEST - Muhafýz: " + yanit.mesaj);
        Debug.Log("TEST - Aksiyon: " + yanit.aksiyon);
    }

    public async void SohbetEt(string oyuncuSozu)
    {
        Debug.Log("Muhafýz düþünüyor...");

        
        MuhafizCevap yanit = await baglanti.MuhafizlaKonus(oyuncuSozu);

        Debug.Log("Muhafýz: " + yanit.mesaj);

        if (yanit.aksiyon == "kapiyi_ac")
        {
            catiKapisi.KapiyiAc();
            Debug.Log("Muhafýz ikna oldu, kapý açýlýyor.");
        }
    }
}