using UnityEngine;

public class YanginKapisi : MonoBehaviour
{
    // Artýk Lever deðil, yeni oluþturduðumuz Salter sýnýfýný kullanýyoruz
    public Salter salter;

    public bool kapiAcikMi = false;
    public float acilmaHizi = 2f;

    void OnEnable()
    {
        // Þalter çekildiðinde KapiyiAc fonksiyonu tetiklenecek
        salter.SalterCekildi += KapiyiAc;
    }

    void OnDisable()
    {
        // Obje kapanýrsa veya silinirse dinlemeyi býrak
        salter.SalterCekildi -= KapiyiAc;
    }

    public void KapiyiAc()
    {
        kapiAcikMi = true;
    }

    void Update()
    {
        // Kapý açýldýysa her karede yukarý doðru hareket ettir
        if (kapiAcikMi == true)
        {
            transform.Translate(0, acilmaHizi * Time.deltaTime, 0);
        }
    }
}