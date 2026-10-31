using UnityEngine;
using UnityEngine.AI;

public class HedefSecici
{
    public float algilamaYaricapi = 40f;

    public IEtkilesilebilir EnIyiHedefiSec(YardimciKarakter karakter)
    {
        IEtkilesilebilir enIyiHedef = null;
        float enKisaYol = 100000f;

        MonoBehaviour[] hepsi = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        for (int i = 0; i < hepsi.Length; i++)
        {
            IEtkilesilebilir aday = hepsi[i] as IEtkilesilebilir;
            if (aday == null)
            {
                continue;
            }

            if (aday.Kullanilabilir == false)
            {
                continue;
            }

            Vector3 yon = aday.EtkilesimNoktasi.position - karakter.transform.position;
            if (yon.magnitude > algilamaYaricapi)
            {
                continue;
            }

            NavMeshPath yol = new NavMeshPath();
            bool yolVarMi = karakter.ajan.CalculatePath(aday.EtkilesimNoktasi.position, yol);

            if (yolVarMi == false || yol.status != NavMeshPathStatus.PathComplete)
            {
                continue;
            }

            float uzunluk = YolUzunlugu(yol);
            float aci = Vector3.SignedAngle(karakter.transform.forward, yon, Vector3.up);

            Debug.Log("[Algi] " + hepsi[i].name + ": yol " + uzunluk.ToString("F1") +
                      " m, aci " + aci.ToString("F0") + " derece");

            if (uzunluk < enKisaYol)
            {
                enKisaYol = uzunluk;
                enIyiHedef = aday;
            }
        }

        return enIyiHedef;
    }

    float YolUzunlugu(NavMeshPath yol)
    {
        float toplam = 0f;

        for (int i = 1; i < yol.corners.Length; i++)
        {
            toplam = toplam + Vector3.Distance(yol.corners[i - 1], yol.corners[i]);
        }

        return toplam;
    }
}