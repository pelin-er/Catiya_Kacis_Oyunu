using UnityEngine;

public class OyuncuKomut : MonoBehaviour
{
    // Önceki adýmlarda bu sýnýfýn adýný YardimciKarakter yapmýþtýk
    public YardimciKarakter yardimci;
    public Salter hedefSalter;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Deðiþkenler boþ deðilse komut ver
            if (yardimci != null && hedefSalter != null)
            {
                yardimci.EtkilesimEmriVer(hedefSalter);
            }
        }
    }
}