using UnityEngine;

public class ZamanliLazer : MonoBehaviour, ITehlike
{
    public float acikSure = 3f;
    public float kapaliSure = 3f;
    public GameObject isin;             
    public AudioSource lazerSesi;       

    public bool acikMi = true;
    float sayac;

    public bool AktifMi
    {
        get { return acikMi; }
    }

    void Start()
    {
        LazeriAc();
    }

    void Update()
    {
        sayac = sayac - Time.deltaTime;

        if (sayac > 0f)
        {
            return;
        }

        if (acikMi == true)
        {
            LazeriKapat();
        }
        else
        {
            LazeriAc();
        }
    }

    void LazeriAc()
    {
        acikMi = true;
        sayac = acikSure;
        isin.SetActive(true);

        if (lazerSesi != null)
        {
            lazerSesi.Play();
        }
    }

    void LazeriKapat()
    {
        acikMi = false;
        sayac = kapaliSure;
        isin.SetActive(false);

        if (lazerSesi != null)
        {
            lazerSesi.Stop();
        }
    }

    public bool YakindaAktifOlacakMi(float saniye)
    {
        if (acikMi == true)
        {
            return true;
        }

        if (sayac <= saniye)
        {
            return true;
        }

        return false;
    }
}