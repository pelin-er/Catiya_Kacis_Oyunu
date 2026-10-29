using UnityEngine;
using UnityEngine.AI;

public class CikisKapisi : MonoBehaviour
{
    public NavMeshObstacle engel;
    public float acilmaHizi = 2f;
    public float acilmaYuksekligi = 3f;

    public bool acilsinMi = false;
    Vector3 hedefYer;

    public void KapiyiAc()
    {
        if (acilsinMi == true)
        {
            return;
        }

        acilsinMi = true;
        hedefYer = transform.position + Vector3.up * acilmaYuksekligi;

        engel.enabled = false;
    }

    void Update()
    {
        if (acilsinMi == true)
        {
            float adim = acilmaHizi * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hedefYer, adim);
        }
    }
}