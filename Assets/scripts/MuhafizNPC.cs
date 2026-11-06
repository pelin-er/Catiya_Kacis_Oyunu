using TMPro;
using UnityEngine;

public class MuhafizNPC : MonoBehaviour
{
    public OllamaBaglantisi baglanti;
    public CikisKapisi catiKapisi;

    public GameObject panel;           
    public GameObject ipucu;            
    public TMP_InputField girisKutusu;
    public TMP_Text cevapYazisi;
    public TMP_Text durumYazisi;

    public float konusmaMesafesi = 3f;
    public int enFazlaHarf = 200;

    Transform oyuncu;
    OyuncuKontrol oyuncuKontrol;
    bool panelAcikMi = false;
    bool kapiAcildiMi = false;

    void Start()
    {
        GameObject oyuncuNesnesi = GameObject.FindWithTag("Player");

        if (oyuncuNesnesi != null)
        {
            oyuncu = oyuncuNesnesi.transform;
            oyuncuKontrol = oyuncuNesnesi.GetComponent<OyuncuKontrol>();
        }

        panel.SetActive(false);
        ipucu.SetActive(false);
    }

    void Update()
    {
        if (oyuncu == null)
        {
            return;
        }

        // Panel kapalýyken: yakýnsa ipucu göster, F'e basýnca aç
        if (panelAcikMi == false)
        {
            float mesafe = Vector3.Distance(transform.position, oyuncu.position);
            bool yakinMi = mesafe <= konusmaMesafesi;

            ipucu.SetActive(yakinMi);

            if (yakinMi == true && Input.GetKeyDown(KeyCode.F))
            {
                PaneliAc();
            }

            return;
        }

        // Panel açýkken
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PaneliKapat();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            MesajiGonder();
        }
    }

    void PaneliAc()
    {
        panelAcikMi = true;
        panel.SetActive(true);
        ipucu.SetActive(false);

        // Yazarken oyuncu yürümesini durdurur
        if (oyuncuKontrol != null)
        {
            oyuncuKontrol.enabled = false;
        }

        girisKutusu.text = "";
        durumYazisi.text = "";

        Invoke("KutuyuAktifEt", 0.1f);
    }

    void PaneliKapat()
    {
        panelAcikMi = false;
        panel.SetActive(false);

        if (oyuncuKontrol != null)
        {
            oyuncuKontrol.enabled = true;
        }
    }

    void KutuyuAktifEt()
    {
        girisKutusu.ActivateInputField();
    }

    async void MesajiGonder()
    {
        string yazi = girisKutusu.text.Trim();

        if (yazi == "")
        {
            durumYazisi.text = "Bir þey yazmalýsýn.";
            return;
        }

        if (baglanti.mesgulMu == true)
        {
            durumYazisi.text = "Muhafýz hâlâ düþünüyor...";
            return;
        }

        if (yazi.Length > enFazlaHarf)
        {
            yazi = yazi.Substring(0, enFazlaHarf);
        }

        girisKutusu.text = "";
        cevapYazisi.text = "Sen: " + yazi;
        durumYazisi.text = "Muhafýz düþünüyor...";

        MuhafizCevap yanit = await baglanti.MuhafizlaKonus(yazi);

        // Sistem hatasý olduysa Muhafýz'ýn sözü gibi gösterme
        if (yanit == null)
        {
            durumYazisi.text = "Beklenmedik bir sorun oldu.";
            return;
        }

        if (yanit.hataVarMi == true)
        {
            durumYazisi.text = yanit.mesaj;
            return;
        }

        durumYazisi.text = "";
        cevapYazisi.text = "Muhafýz: " + yanit.mesaj;
        Debug.Log("Muhafýz: " + yanit.mesaj + " | aksiyon: " + yanit.aksiyon);

        AksiyonuUygula(yanit.aksiyon);
        KutuyuAktifEt();
    }

    // Sadece bildiðimiz aksiyon uygulanýr, gerisi yok sayýlýr
    void AksiyonuUygula(string aksiyon)
    {
        if (aksiyon == null)
        {
            return;
        }

        aksiyon = aksiyon.Trim().ToLower();

        if (aksiyon == "kapiyi_ac")
        {
            if (kapiAcildiMi == false)
            {
                kapiAcildiMi = true;
                catiKapisi.KapiyiAc();
                durumYazisi.text = "Kapý açýldý!";
            }
        }
    }
}