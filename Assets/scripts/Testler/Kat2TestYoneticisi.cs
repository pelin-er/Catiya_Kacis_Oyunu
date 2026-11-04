using System.IO;
using UnityEngine;

public class Kat2TestYoneticisi : MonoBehaviour
{
    public YardimciKarakter gofrik;
    public BasincPlakasi plaka;
    public EngelAlgilayici sensor;
    public BasaDondurucu lazer;
    public Transform[] baslangicNoktalari;

    public int denemeSayisi = 50;
    public float zamanAsimi = 40f;   //bir denemenin en fazla kaç saniye süreceði
    public float zamanHizi = 3f;     

    bool testCaliyorMu = false;
    bool sensorAcikMod = true;     
    bool denemeBasladiMi = false;
    int denemeNo = 1;
    float baslangicZamani;
    float bekleme = 0f;

    int basariliSayisi = 0;
    float toplamSure = 0f;
    int baslangictakiCarpma = 0;
    string csv = "";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && testCaliyorMu == false)
        {
            TestiBaslat();
        }

        if (testCaliyorMu == false)
        {
            return;
        }

        // Ýki deneme arasýnda kýsa bekleme
        if (bekleme > 0f)
        {
            bekleme = bekleme - Time.deltaTime;
            return;
        }

        if (denemeBasladiMi == false)
        {
            DenemeyiBaslat();
            return;
        }

        // plaka basýldý mý ya da süre doldu mu bakýyor
        float gecenSure = Time.time - baslangicZamani;

        if (plaka.basildiMi == true || gecenSure >= zamanAsimi)
        {
            DenemeyiBitir(gecenSure);
        }
    }

    void TestiBaslat()
    {
        testCaliyorMu = true;
        Time.timeScale = zamanHizi;
        csv = "mod,deneme,basarili,saniye\n";

        sensorAcikMod = true;  
        ModuHazirla();
    }

    void ModuHazirla()
    {
        sensor.sensorAcik = sensorAcikMod;
        denemeNo = 1;
        basariliSayisi = 0;
        toplamSure = 0f;
        baslangictakiCarpma = lazer.gofrikCarpmaSayisi;
        denemeBasladiMi = false;
    }

    void DenemeyiBaslat()
    {
        plaka.Sifirla();

        // Rastgele bir baþlangýç noktasý seçip gofrik'i oraya ýþýnlar
        int sira = Random.Range(0, baslangicNoktalari.Length);
        gofrik.ajan.Warp(baslangicNoktalari[sira].position);

        baslangicZamani = Time.time;
        gofrik.EtkilesimEmriVer(plaka);
        denemeBasladiMi = true;
    }

    void DenemeyiBitir(float gecenSure)
    {
        if (plaka.basildiMi == true)
        {
            basariliSayisi = basariliSayisi + 1;
            toplamSure = toplamSure + gecenSure;
        }

        csv = csv + ModAdi() + "," + denemeNo + "," + plaka.basildiMi + "," +
              gecenSure.ToString("F2").Replace(",", ".") + "\n";

        denemeNo = denemeNo + 1;
        denemeBasladiMi = false;
        bekleme = 0.5f;

        // Bu moddaki denemeler bitti mi bakýyor
        if (denemeNo > denemeSayisi)
        {
            ModuBitir();
        }
    }

    void ModuBitir()
    {
        float basariOrani = 100f * basariliSayisi / denemeSayisi;
        float ortalamaSure = 0f;

        if (basariliSayisi > 0)
        {
            ortalamaSure = toplamSure / basariliSayisi;
        }

        int carpma = lazer.gofrikCarpmaSayisi - baslangictakiCarpma;

        Debug.Log("[Test] " + ModAdi() + ": basari %" + basariOrani.ToString("F0") +
                  " (" + basariliSayisi + "/" + denemeSayisi + "), ort. sure " +
                  ortalamaSure.ToString("F1") + " sn, lazere carpma " + carpma);

        if (sensorAcikMod == true)
        {
            //sensör kapalýyken
            sensorAcikMod = false;
            ModuHazirla();
        }
        else
        {
            TestiBitir();
        }
    }

    void TestiBitir()
    {
        sensor.sensorAcik = true;
        Time.timeScale = 1f;
        testCaliyorMu = false;

        string yol = Path.Combine(Application.persistentDataPath, "kat2_test.csv");
        File.WriteAllText(yol, csv);
        Debug.Log("[Test] Bitti. CSV yazildi: " + yol);
    }

    string ModAdi()
    {
        if (sensorAcikMod == true)
        {
            return "sensor_acik";
        }
        return "sensor_kapali";
    }
}