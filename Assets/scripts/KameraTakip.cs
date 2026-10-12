using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    // Deðiþkenleri Inspector'da görmek için public yaptýk
    public Transform hedef; // Buraya Hierarchy'den 'oyuncu'yu sürükleyeceðiz
    public Vector3 mesafe = new Vector3(0f, 12f, -10f); // Kameranýn duracaðý uzaklýk
    public float yumusaklik = 8f; // Takip yumuþaklýðý

    void LateUpdate()
    {
        if (hedef == null)
        {
            return;
        }

        // Kameranýn gitmesi gereken hedef pozisyonu hesapla
        Vector3 gidilecekYer = hedef.position + mesafe;

        // Kamerayý mevcut yerinden hedef yere yumuþak bir þekilde kaydýr
        transform.position = Vector3.Lerp(transform.position, gidilecekYer, yumusaklik * Time.deltaTime);

        // Kameranýn her zaman oyuncuya bakmasýný saðla
        transform.LookAt(hedef.position + Vector3.up);
    }
}