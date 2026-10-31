using UnityEngine;

public class YanginKapisi : MonoBehaviour
{
    public bool kapiAcikMi = false;
    public float acilmaHizi = 2f;
    public float acilmaYuksekligi = 3f;     

    float baslangicY;                

    void Start()
    {
        baslangicY = transform.position.y; 
    }

    public void KapiyiAc()
    {
        kapiAcikMi = true;
    }

    void Update()
    {
        if (kapiAcikMi == true)
        {
            if (transform.position.y < baslangicY + acilmaYuksekligi)
            {
                transform.Translate(0, acilmaHizi * Time.deltaTime, 0);
            }
        }
    }
}