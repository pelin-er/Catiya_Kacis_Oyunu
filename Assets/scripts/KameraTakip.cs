using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef; 
    public Vector3 mesafe = new Vector3(0f, 12f, -10f); 
    public float yumusaklik = 8f; 

    void LateUpdate()
    {
        if (hedef == null)
        {
            return;
        }

        Vector3 gidilecekYer = hedef.position + mesafe;

        transform.position = Vector3.Lerp(transform.position, gidilecekYer, yumusaklik * Time.deltaTime);

        transform.LookAt(hedef.position + Vector3.up);
    }
}