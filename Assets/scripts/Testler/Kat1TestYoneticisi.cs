using UnityEngine;

public class Kat1TestYoneticisi : MonoBehaviour
{
    public YardimciKarakter yardimci;
    public Salter salter;
    public YanginKapisi kapi;
    public Transform[] baslangicNoktalari;

    public int denemeSayisi = 20;
    public float zamanAsimi = 40f;

    int durum = 0;
    int denemeNo = 0;
    int basariliSayisi = 0;
    float toplamSure = 0f;
    float baslangicZamani = 0f;
    float molaBitisi = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6) && durum == 0)
        {
            denemeNo = 0;
            basariliSayisi = 0;
            toplamSure = 0f;
            Time.timeScale = 2f;
            YeniDenemeHazirla();
            return;
        }

        // Durum 1: ýþýnlanma.  Þalter emrini verir.
        if (durum == 1)
        {
            yardimci.EtkilesimEmriVer(salter);
            baslangicZamani = Time.time;
            durum = 2;
            return;
        }

        // Durum 2: kapý açýlana ya da süre dolana kadar bekleme.
        if (durum == 2)
        {
            float gecen = Time.time - baslangicZamani;

            if (kapi.kapiAcikMi == true)
            {
                basariliSayisi = basariliSayisi + 1;
                toplamSure = toplamSure + gecen;
                Debug.Log("[Kat1 Test] deneme " + denemeNo + ": BAÞARILI, " + gecen.ToString("F1") + " sn");
                durum = 3;
                molaBitisi = Time.time + 0.5f;
            }
            else if (gecen > zamanAsimi)
            {
                Debug.Log("[Kat1 Test] deneme " + denemeNo + ": BAÞARISIZ (süre doldu)");
                durum = 3;
                molaBitisi = Time.time + 0.5f;
            }
            return;
        }

        // Durum 3: yeni deneme.
        if (durum == 3)
        {
            if (Time.time > molaBitisi)
            {
                YeniDenemeHazirla();
            }
        }
    }

    void YeniDenemeHazirla()
    {
        denemeNo = denemeNo + 1;

        if (denemeNo > denemeSayisi)
        {
            TestiBitir();
            return;
        }

        salter.Sifirla();
        kapi.Sifirla();

        int secilen = Random.Range(0, baslangicNoktalari.Length);
        yardimci.ajan.Warp(baslangicNoktalari[secilen].position);
        yardimci.DurumDegistir(new TakipEtmeDurumu());

        durum = 1;
    }

    void TestiBitir()
    {
        Time.timeScale = 1f;
        durum = 0;

        float yuzde = 100f * basariliSayisi / denemeSayisi;
        float ortalama = 0f;
        if (basariliSayisi > 0)
        {
            ortalama = toplamSure / basariliSayisi;
        }

        Debug.Log("[Kat1 Test] SONUÇ: Baþarý %" + yuzde.ToString("F0") + " (" + basariliSayisi + "/" + denemeSayisi + "), ortalama süre " + ortalama.ToString("F1") + " sn");
    }
}