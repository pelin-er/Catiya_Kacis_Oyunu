using System;
using UnityEngine;

public class Salter : MonoBehaviour, IEtkilesilebilir
{
    public Transform etkilesimNoktasi;
    public event Action SalterCekildi; // Kapýya haber gönderecek olay
    public bool kullanildiMi = false;

    // Arayüzdeki (Interface) kuralý uyguluyoruz
    public Transform EtkilesimNoktasi
    {
        get { return etkilesimNoktasi; }
    }

    public void EtkilesimeGir()
    {
        // Eðer zaten kullanýldýysa hiçbir þey yapma
        if (kullanildiMi == true)
        {
            return;
        }

        kullanildiMi = true;
        transform.Rotate(0f, 0f, 45f); // Þalterin eðilmesi (görsel)

        // Kapýya haber uçur (Eðer kapý bunu dinliyorsa)
        if (SalterCekildi != null)
        {
            SalterCekildi();
        }
    }
}