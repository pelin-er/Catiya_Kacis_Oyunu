using UnityEngine;
using UnityEngine.AI;

public class HedefeGitmeDurumu : IDurum
{
    public float varisMesafesi = 0.8f;  
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
       
        if (karakter.TehlikeVarMi() == true)
        {
            karakter.DurumDegistir(new BeklemeDurumu(this));
            return;
        }

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

        Vector3 fark = hedef.EtkilesimNoktasi.position - karakter.transform.position;
        fark.y = 0f;
        float mesafe = fark.magnitude;

        if (mesafe <= varisMesafesi)
        {
            karakter.DurumDegistir(new EtkilesimDurumu(hedef));
            return;
        }

        if (karakter.ajan.pathPending == false && karakter.ajan.hasPath == false)
        {
            karakter.ajan.SetDestination(hedef.EtkilesimNoktasi.position);
        }
    }

    public void Cikis(YardimciKarakter karakter)
    {
    }
}