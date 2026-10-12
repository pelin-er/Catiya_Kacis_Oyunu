using System;
using UnityEngine;

public class Salter : MonoBehaviour, IEtkilesilebilir
{
    public Transform etkilesimNoktasi;
    public event Action SalterCekildi; // Kapýya haber gönderecek olay
    public bool kullanildiMi = false;

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
        transform.Rotate(0f, 0f, 45f); 

        if (SalterCekildi != null)
        {
            SalterCekildi();
        }
    }
}