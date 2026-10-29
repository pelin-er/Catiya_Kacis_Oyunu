using UnityEngine;

public class ZamanliLazer : MonoBehaviour, ITehlike
{
    public float acikSure = 3f;
    public float kapaliSure = 3f;
    public GameObject isin;       

    public bool acikMi = true;
    float sayac;

    public bool AktifMi
    {
        get { return acikMi; }
    }

    void Start()
    {
        sayac = acikSure;
        isin.SetActive(true);
    }

    void Update()
    {
        sayac = sayac - Time.deltaTime;

        if (sayac <= 0f)
        {
            if (acikMi == true)
            {
                acikMi = false;
                sayac = kapaliSure;
            }
            else
            {
                acikMi = true;
                sayac = acikSure;
            }

            isin.SetActive(acikMi);
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