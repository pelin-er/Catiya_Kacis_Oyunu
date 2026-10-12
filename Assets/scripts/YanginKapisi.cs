using UnityEngine;

public class YanginKapisi : MonoBehaviour
{
    public bool kapiAcikMi = false;
    public float acilmaHizi = 2f;

    public void KapiyiAc()
    {
        kapiAcikMi = true;
    }

    void Update()
    {
        if (kapiAcikMi == true)
        {
            transform.Translate(0, acilmaHizi * Time.deltaTime, 0);
        }
    }
}