using UnityEngine;

public class YanginKapisi : MonoBehaviour
{
    public Salter salter;

    public bool kapiAcikMi = false;
    public float acilmaHizi = 2f;

    void OnEnable()
    {
        salter.SalterCekildi += KapiyiAc;
    }

    void OnDisable()
    {
        salter.SalterCekildi -= KapiyiAc;
    }

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