using UnityEngine;
using UnityEngine.AI;

public class YardimciKarakter : MonoBehaviour
{
    public Transform oyuncu;
    public NavMeshAgent ajan;
    public IDurum suAnkiDurum;
    public float algilamaYaricapi = 40f;

    EngelAlgilayici sensor;
    HedefSecici hedefSecici;

    void Start()
    {
        ajan = GetComponent<NavMeshAgent>();
        sensor = GetComponent<EngelAlgilayici>();

        hedefSecici = new HedefSecici();
        hedefSecici.algilamaYaricapi = algilamaYaricapi;

        suAnkiDurum = new TakipEtmeDurumu();
        suAnkiDurum.Giris(this);
    }

    void Update()
    {
        if (ajan.isOnNavMesh == false)
        {
            return;
        }

        if (suAnkiDurum != null)
        {
            suAnkiDurum.Guncelle(this);
        }
    }

    public void DurumDegistir(IDurum yeniDurum)
    {
        if (suAnkiDurum != null)
        {
            suAnkiDurum.Cikis(this);
        }

        suAnkiDurum = yeniDurum;
        Debug.Log("Yeni durum: " + yeniDurum.GetType().Name);
        suAnkiDurum.Giris(this);
    }

    public void EtkilesimEmriVer(IEtkilesilebilir hedef)
    {
        DurumDegistir(new HedefeGitmeDurumu(hedef));
    }

    public void GenelEmirVer()
    {
        IEtkilesilebilir hedef = hedefSecici.EnIyiHedefiSec(this);

        if (hedef == null)
        {
            Debug.Log("Cozulecek engel bulamadim");
            return;
        }

        MonoBehaviour hedefParcasi = hedef as MonoBehaviour;
        Debug.Log("Hedef secildi: " + hedefParcasi.name);

        DurumDegistir(new HedefeGitmeDurumu(hedef));
    }

    public bool TehlikeVarMi()
    {
        if (sensor == null)
        {
            return false;
        }

        return sensor.YolEngelliMi();
    }
}