using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class OyuncuKontrol : MonoBehaviour
{
    public float hiz = 5f;
    public Transform kamera;

    public CharacterController karakterKontrolcu;
    public float dikeyHiz;

    void Start()
    {
        karakterKontrolcu = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 ileri = Vector3.ProjectOnPlane(kamera.forward, Vector3.up).normalized;
        Vector3 sag = Vector3.ProjectOnPlane(kamera.right, Vector3.up).normalized;

        Vector3 hareket = (ileri * z) + (sag * x);

        if (hareket.magnitude > 1f)
        {
            hareket.Normalize();
        }

        if (karakterKontrolcu.isGrounded == true)
        {
            dikeyHiz = -1f;
        }
        else
        {
            dikeyHiz = dikeyHiz + (Physics.gravity.y * Time.deltaTime);
        }

        Vector3 sonHareket = (hareket * hiz) + (Vector3.up * dikeyHiz);
        karakterKontrolcu.Move(sonHareket * Time.deltaTime);

        if (hareket.magnitude > 0.01f)
        {
            Quaternion donusHedefi = Quaternion.LookRotation(hareket);
            transform.rotation = Quaternion.Slerp(transform.rotation, donusHedefi, 12f * Time.deltaTime);
        }
    }
}