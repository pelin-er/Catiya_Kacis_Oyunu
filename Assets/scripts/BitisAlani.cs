using UnityEngine;

public class BitisAlani : MonoBehaviour
{
    public GameObject bitisPaneli;

    void OnTriggerEnter(Collider diger)
    {
        if (diger.CompareTag("Player"))
        {
            bitisPaneli.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}