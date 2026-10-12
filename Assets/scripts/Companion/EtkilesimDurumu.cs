using UnityEngine;

public class EtkilesimDurumu : IDurum
{
    public IEtkilesilebilir hedefNesne;
    public float beklemeSuresi = 1.0f;

    public EtkilesimDurumu(IEtkilesilebilir secilenHedef)
    {
        hedefNesne = secilenHedef;
    }

    public void Giris(YardimciKarakter karakter)
    {
        karakter.ajan.ResetPath();
        hedefNesne.EtkilesimeGir();
    }

    public void Guncelle(YardimciKarakter karakter)
    {
        beklemeSuresi = beklemeSuresi - Time.deltaTime;

        if (beklemeSuresi <= 0.0f)
        {
            karakter.DurumDegistir(new TakipEtmeDurumu());
        }
    }

    public void Cikis(YardimciKarakter karakter)
    {
    }
}