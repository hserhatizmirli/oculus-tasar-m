using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TargetXRI : MonoBehaviour
{
    public Transform targetObject; // Hedef nesne
    public Vector3 targetPosition = new Vector3(0, 0, 0); // Yeni konum
    public Vector3 targetRotation = new Vector3(0, 0, 0); // Yeni rotasyon (Euler açýlarý)
    private bool isClicked = false; // Hedefin daha önce týklanýp týklanmadýðýný kontrol eder

    private XRGrabInteractable grabInteractable; // XR Grab Interactable bileþeni

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>(); // XR Grab Interactable bileþenini al

        if (grabInteractable != null)
        {
            // XR Interactable için Select Enter event'ini baðla
            grabInteractable.onSelectEntered.AddListener(OnSelectEntered);
        }
        else
        {
            Debug.LogError("XRGrabInteractable bileþeni eksik!");
        }
    }

    void OnSelectEntered(XRBaseInteractor interactor)
    {
        // Eðer hedef nesneye týklanmýþsa ve daha önce taþýnmamýþsa
        if (!isClicked)
        {
            MoveTarget(); // Hedefi taþý
            isClicked = true; // Hedef taþýndý, tekrar taþýnamaz
        }
    }

    void MoveTarget()
    {
        // Konum deðiþikliði
        if (targetObject != null)
        {
            targetObject.position = targetPosition; // Hedefin yeni konumunu belirler

            // Rotasyon deðiþikliði
            targetObject.rotation = Quaternion.Euler(targetRotation); // Hedefin yeni rotasýný belirler

            Debug.Log("Hedef taþýndý!"); // Debug log mesajý
        }
        else
        {
            Debug.LogError("Hedef nesne atanmadý!"); // Eðer hedef nesne atanmadýysa hata mesajý
        }
    }
}
