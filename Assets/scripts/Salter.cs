using UnityEngine;

public class Salter : MonoBehaviour, IEtkilesilebilir
{
    public YanginKapisi kapi;

    public Transform etkilesimNoktasi;
    public bool kullanildiMi = false;

    public Transform EtkilesimNoktasi
    {
        get { return etkilesimNoktasi; }
    }

    public void EtkilesimeGir()
    {
        if (kullanildiMi == true)
        {
            return;
        }

        kullanildiMi = true;
        transform.Rotate(0f, 0f, 45f);

        if (kapi != null)
        {
            kapi.KapiyiAc();
            Debug.Log("Þalter çekildi, kapýya haber verildi.");
        }
    }
}