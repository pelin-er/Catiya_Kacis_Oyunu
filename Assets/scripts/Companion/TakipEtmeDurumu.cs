using UnityEngine;

public class TakipEtmeDurumu : IDurum
{
    public float baslamaMesafesi = 3f;     
    public float durmaMesafesi = 1.8f;   
    bool hareketEdiyor = false;

    public void Giris(YardimciKarakter karakter)
    {
        karakter.ajan.isStopped = false;
        hareketEdiyor = false;
    }

    public void Guncelle(YardimciKarakter karakter)
    {
        if (karakter.oyuncu == null)
        {
            return;
        }

        float mesafe = Vector3.Distance(karakter.transform.position, karakter.oyuncu.position);

        if (hareketEdiyor == false && mesafe > baslamaMesafesi)
        {
            hareketEdiyor = true;
        }
        else if (hareketEdiyor == true && mesafe < durmaMesafesi)
        {
            hareketEdiyor = false;
            karakter.ajan.ResetPath();
        }

        if (hareketEdiyor == false)
        {
            return;
        }

        if (karakter.TehlikeVarMi() == true)
        {
            karakter.DurumDegistir(new BeklemeDurumu(this));
            return;
        }

        karakter.ajan.SetDestination(karakter.oyuncu.position);
    }

    public void Cikis(YardimciKarakter karakter)
    {
    }
}