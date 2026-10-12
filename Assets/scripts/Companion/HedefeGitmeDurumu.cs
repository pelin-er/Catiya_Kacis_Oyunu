using UnityEngine;
using UnityEngine.AI;

public class HedefeGitmeDurumu : IDurum
{
    public float yaklasmaSiniri = 0.5f;
    public IEtkilesilebilir hedef;
    public float zamanAsimi = 20f;

    public HedefeGitmeDurumu(IEtkilesilebilir yeniHedef)
    {
        hedef = yeniHedef;
    }

    public void Giris(YardimciKarakter karakter)
    {
        karakter.ajan.isStopped = false;

        bool yolVarMi = karakter.ajan.SetDestination(hedef.EtkilesimNoktasi.position);

        if (yolVarMi == false)
        {
            karakter.DurumDegistir(new TakipEtmeDurumu());
        }
    }

    public void Guncelle(YardimciKarakter karakter)
    {
        zamanAsimi = zamanAsimi - Time.deltaTime;

        if (zamanAsimi <= 0f)
        {
            karakter.DurumDegistir(new TakipEtmeDurumu());
            return;
        }

        if (karakter.ajan.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            karakter.DurumDegistir(new TakipEtmeDurumu());
            return;
        }

        if (karakter.ajan.pathPending == false)
        {
            if (karakter.ajan.remainingDistance <= karakter.ajan.stoppingDistance + yaklasmaSiniri)
            {
                karakter.DurumDegistir(new EtkilesimDurumu(hedef));
            }
        }
    }

    public void Cikis(YardimciKarakter karakter)
    {
    }
}