using UnityEngine;

public class OyuncuKomut : MonoBehaviour
{
    public YardimciKarakter yardimci;
    public Salter hedefSalter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (yardimci != null && hedefSalter != null)
            {
                yardimci.EtkilesimEmriVer(hedefSalter);
            }
        }
    }
}