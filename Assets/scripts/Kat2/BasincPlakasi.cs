using UnityEngine;

public class BasincPlakasi : MonoBehaviour, IEtkilesilebilir
{
    public CikisKapisi kapi;
    public Transform etkilesimNoktasi; 
    public Renderer plakaRenderer;
    public bool basildiMi = false;

    Color eskiRenk;

    public Transform EtkilesimNoktasi
    {
        get { return etkilesimNoktasi; }
    }

    public bool Kullanilabilir
    {
        get { return basildiMi == false; }
    }

    void Start()
    {
        eskiRenk = plakaRenderer.material.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EtkilesimeGir();
        }
    }

    public void EtkilesimeGir()
    {
        if (basildiMi == true)
        {
            return;
        }

        basildiMi = true;
        plakaRenderer.material.color = Color.green;
        kapi.KapiyiAc();
        Debug.Log("Plaka basildi, kapi aciliyor.");
    }

    public void Sifirla()
    {
        basildiMi = false;
        plakaRenderer.material.color = eskiRenk;
    }
}