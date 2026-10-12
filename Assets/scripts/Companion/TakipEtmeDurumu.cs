using UnityEngine;

public class TakipEtmeDurumu : IDurum
{
    public float takipMesafesi = 2.5f;

    public void Giris(YardimciKarakter karakter)
    {
        karakter.ajan.isStopped = false;
    }

    public void Guncelle(YardimciKarakter karakter)
    {
        float mesafe = Vector3.Distance(karakter.transform.position, karakter.oyuncu.position);

        if (mesafe > takipMesafesi)
        {
            karakter.ajan.SetDestination(karakter.oyuncu.position);
        }
        else
        {
            karakter.ajan.ResetPath(); // Dur
        }
    }

    public void Cikis(YardimciKarakter karakter)
    {
        
    }
}