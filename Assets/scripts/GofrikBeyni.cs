using UnityEngine;
using UnityEngine.AI;

public class GofrikBeyni : MonoBehaviour
{
    public Transform oyuncu;
    public NavMeshAgent ajan;
    public float takipMesafesi = 2.5f;
    public float sensorMesafesi = 3.0f;

    private IEtkilesilebilir suAnkiHedef;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, sensorMesafesi))
        {
            if (hit.collider.CompareTag("Lazer") && hit.collider.gameObject.activeSelf)
            {
                ajan.isStopped = true;
                return;
            }
        }
        ajan.isStopped = false;

        if (suAnkiHedef == null)
        {
            if (oyuncu != null && Vector3.Distance(transform.position, oyuncu.position) > takipMesafesi)
                ajan.SetDestination(oyuncu.position);
            else ajan.ResetPath();
        }
        else
        {
            ajan.SetDestination(suAnkiHedef.EtkilesimNoktasi.position);
            if (!ajan.pathPending && ajan.remainingDistance < 0.8f)
            {
                suAnkiHedef.EtkilesimeGir();
                suAnkiHedef = null; 
            }
        }
    }

    public void EtkilesimEmriVer(IEtkilesilebilir hedef)
    {
        suAnkiHedef = hedef;
    }
}