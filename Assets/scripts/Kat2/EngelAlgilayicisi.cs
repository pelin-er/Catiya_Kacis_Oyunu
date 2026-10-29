using UnityEngine;

public class EngelAlgilayici : MonoBehaviour
{
    public bool sensorAcik = true;
    public float menzil = 4f;             
    public float gerekenBosSure = 2f;      
    public float cesaretSuresi = 1.8f;     
    public float yakinMesafe = 0.8f;      
    float[] acilar = { -30f, -15f, 0f, 15f, 30f };
    float cesaretBitis = 0f;
    int katman;

    void Start()
    {
        katman = LayerMask.GetMask("Hazard");
    }

    public bool YolEngelliMi()
    {
        if (sensorAcik == false)
        {
            return false;
        }

        if (Time.time < cesaretBitis)
        {
            return false;
        }

        Vector3 baslangic = transform.position + Vector3.up * 0.3f;
        ITehlike enYakinTehlike = null;
        float enYakinMesafe = 1000f;

        for (int i = 0; i < acilar.Length; i++)
        {
            Vector3 yon = Quaternion.AngleAxis(acilar[i], Vector3.up) * transform.forward;
            RaycastHit vurus;

            bool carptiMi = Physics.Raycast(baslangic, yon, out vurus, menzil, katman,
                                            QueryTriggerInteraction.Collide);

            if (carptiMi == false)
            {
                Debug.DrawRay(baslangic, yon * menzil, Color.green);
                continue;
            }

            Debug.DrawRay(baslangic, yon * vurus.distance, Color.red);

            ITehlike tehlike = vurus.collider.GetComponentInParent<ITehlike>();

            if (tehlike != null && vurus.distance < enYakinMesafe)
            {
                enYakinTehlike = tehlike;
                enYakinMesafe = vurus.distance;
            }
        }

        if (enYakinTehlike == null)
        {
            return false;
        }

        if (enYakinMesafe < yakinMesafe)
        {
            return false;
        }

        if (enYakinTehlike.YakindaAktifOlacakMi(gerekenBosSure) == true)
        {
            return true;
        }

        cesaretBitis = Time.time + cesaretSuresi;
        return false;
    }
}