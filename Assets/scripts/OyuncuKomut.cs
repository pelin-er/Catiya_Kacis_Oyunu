using UnityEngine;

public class OyuncuKomut : MonoBehaviour
{
    public YardimciKarakter yardimci;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (yardimci != null)
            {
                yardimci.GenelEmirVer();   
            }
        }
    }
}