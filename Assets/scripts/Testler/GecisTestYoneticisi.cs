using UnityEngine;

public class GecisTestYoneticisi : MonoBehaviour
{
    public YardimciKarakter yardimci;
    public Salter salter;
    public YanginKapisi yanginKapisi;
    public BasincPlakasi plaka;
    public Transform gecisNoktasi;      
    public Transform[] baslangicNoktalari;  

    public int denemeSayisi = 10;
    public float zamanAsimi = 40f;          
    public float gecisMesafesi = 3f;

    int durum = 0;      // 0 = kapalý, 1 = emir verilecek, 2 = bekliyor, 3 = mola
    int asama = 0;      // 0 = þalter, 1 = geçiþ, 2 = plaka
    int denemeNo = 0;
    int basariliSayisi = 0;
    int salterdeTakilan = 0;
    int gecistTakilan = 0;
    int plakadaTakilan = 0;
    float asamaBaslangici = 0f;
    float molaBitisi = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7) && durum == 0)
        {
            denemeNo = 0;
            basariliSayisi = 0;
            salterdeTakilan = 0;
            gecistTakilan = 0;
            plakadaTakilan = 0;
            YeniDenemeHazirla();
            return;
        }

        if (durum == 1)
        {
            yardimci.EtkilesimEmriVer(salter);
            asama = 0;
            asamaBaslangici = Time.time;
            durum = 2;
            return;
        }

        if (durum == 2)
        {
            AsamayiKontrolEt();
            return;
        }

        if (durum == 3)
        {
            if (Time.time > molaBitisi)
            {
                YeniDenemeHazirla();
            }
        }
    }

    void AsamayiKontrolEt()
    {
        float gecen = Time.time - asamaBaslangici;
        bool sureDoldu = gecen > zamanAsimi;

        // Aþama 0: þalter çekilip kapý açýldý mý konrtol eder
        if (asama == 0)
        {
            if (yanginKapisi.kapiAcikMi == true)
            {
                Debug.Log("[Gecis Test] deneme " + denemeNo + " - salter tamam (" + gecen.ToString("F1") + " sn)");
                asama = 1;
                asamaBaslangici = Time.time;
                yardimci.EtkilesimEmriVer(plaka);
            }
            else if (sureDoldu == true)
            {
                salterdeTakilan = salterdeTakilan + 1;
                DenemeBitti(false);
            }
        }
        // Aþama 1: Kat 2 giriþine vardý mý kontrol eder
        else if (asama == 1)
        {
            float mesafe = Vector3.Distance(yardimci.transform.position, gecisNoktasi.position);

            if (mesafe < gecisMesafesi)
            {
                Debug.Log("[Gecis Test] deneme " + denemeNo + " - Kat 2'ye gecti (" + gecen.ToString("F1") + " sn)");
                asama = 2;
                asamaBaslangici = Time.time;
            }
            else if (sureDoldu == true)
            {
                gecistTakilan = gecistTakilan + 1;
                DenemeBitti(false);
            }
        }
        // Aþama 2: plakaya bastý mý kontrol eder
        else if (asama == 2)
        {
            if (plaka.basildiMi == true)
            {
                Debug.Log("[Gecis Test] deneme " + denemeNo + " - plaka tamam (" + gecen.ToString("F1") + " sn)");
                basariliSayisi = basariliSayisi + 1;
                DenemeBitti(true);
            }
            else if (sureDoldu == true)
            {
                plakadaTakilan = plakadaTakilan + 1;
                DenemeBitti(false);
            }
        }
    }

    void DenemeBitti(bool basarili)
    {
        if (basarili == true)
        {
            Debug.Log("[Gecis Test] deneme " + denemeNo + ": BASARILI");
        }
        else
        {
            Debug.Log("[Gecis Test] deneme " + denemeNo + ": BASARISIZ (asama " + asama + ")");
        }

        durum = 3;
        molaBitisi = Time.time + 0.5f;
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
        yanginKapisi.Sifirla();
        plaka.Sifirla();

        int secilen = Random.Range(0, baslangicNoktalari.Length);
        Debug.Log("[Gecis Test] deneme " + denemeNo + " baslangic: " + baslangicNoktalari[secilen].name);
        yardimci.ajan.Warp(baslangicNoktalari[secilen].position);
        yardimci.DurumDegistir(new TakipEtmeDurumu());

        durum = 1;
    }

    void TestiBitir()
    {
        durum = 0;
        float yuzde = 100f * basariliSayisi / denemeSayisi;
        Debug.Log("[Gecis Test] SONUÇ: baþarý %" + yuzde.ToString("F0") + " (" + basariliSayisi + "/" + denemeSayisi + ")");
        Debug.Log("[Gecis Test] takýlanlar -> salter: " + salterdeTakilan + ", gecis: " + gecistTakilan + ", plaka: " + plakadaTakilan);
    }
}