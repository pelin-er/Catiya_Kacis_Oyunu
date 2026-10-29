using UnityEngine;
using UnityEngine.AI;

public class BasaDondurucu : MonoBehaviour
{
    public Transform donusNoktasi;    
    public int gofrikCarpmaSayisi = 0; 
    ZamanliLazer lazer;
    BoxCollider kutu;

    void Start()
    {
        lazer = GetComponent<ZamanliLazer>();
        kutu = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (lazer.AktifMi == false)
        {
            return;
        }

        Collider[] icindekiler = Physics.OverlapBox(kutu.bounds.center, kutu.bounds.extents);

        for (int i = 0; i < icindekiler.Length; i++)
        {
            Collider c = icindekiler[i];

            if (c.CompareTag("Player"))
            {
                OyuncuyuGeriGonder(c);
            }

            if (c.CompareTag("Companion"))
            {
                GofrigiGeriGonder(c);
            }
        }
    }

    void OyuncuyuGeriGonder(Collider c)
    {
        CharacterController kontrolcu = c.GetComponent<CharacterController>();

        kontrolcu.enabled = false;
        c.transform.position = donusNoktasi.position + Vector3.up;
        kontrolcu.enabled = true;

        Debug.Log("Oyuncu lazere çarptý, baþa döndü");
    }

    void GofrigiGeriGonder(Collider c)
    {
        NavMeshAgent ajan = c.GetComponent<NavMeshAgent>();

        ajan.Warp(donusNoktasi.position);
        gofrikCarpmaSayisi = gofrikCarpmaSayisi + 1;

        Debug.Log("Gofrik lazere çarptý, baþa döndü");
    }
}