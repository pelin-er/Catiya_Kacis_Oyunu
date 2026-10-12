using UnityEngine;
using UnityEngine.AI;

public class YardimciKarakter : MonoBehaviour
{
    public Transform oyuncu;
    public NavMeshAgent ajan;
    public IDurum suAnkiDurum;

    void Start()
    {
        ajan = GetComponent<NavMeshAgent>();

        // Oyun baþlayýnca ilk durum takip etme olsun
        suAnkiDurum = new TakipEtmeDurumu();
        suAnkiDurum.Giris(this);
    }

    void Update()
    {
        // Ajan navmesh üzerinde deðilse hiçbir þey yapma
        if (ajan.isOnNavMesh == false)
        {
            return;
        }

        // Eðer bir durum atanmýþsa onu sürekli güncelle
        if (suAnkiDurum != null)
        {
            suAnkiDurum.Guncelle(this);
        }
    }

    public void DurumDegistir(IDurum yeniDurum)
    {
        // Eski durumdan çýk
        if (suAnkiDurum != null)
        {
            suAnkiDurum.Cikis(this);
        }

        // Yeni duruma geç
        suAnkiDurum = yeniDurum;
        Debug.Log("Yeni duruma geçildi.");
        suAnkiDurum.Giris(this);
    }

    public void EtkilesimEmriVer(IEtkilesilebilir hedef)
    {
        DurumDegistir(new HedefeGitmeDurumu(hedef));
    }
}