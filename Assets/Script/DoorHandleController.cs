using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandleController : MonoBehaviour
{
    [Header("Door Handle Settings")]
    public Transform doorHandle; // Kapý kolu (pivot noktasýna baðlý)
    public Transform door;       // Kapý objesi
    public float handleRotationLimit = 30f; // Kapý kolu aþaðý bastýrma açýsý
    public float handleResetSpeed = 5f;     // Kapý kolunun eski pozisyonuna dönüþ hýzý

    [Header("Door Settings")]
    public float doorOpenAngle = 90f;      // Kapýnýn açýlma açýsý
    public float doorOpenSpeed = 2f;       // Kapýnýn açýlma hýzý
    public bool isHandlePressed = false;   // Kapý kolunun basýlý olup olmadýðýný kontrol eder

    private Quaternion initialHandleRotation; // Kapý kolunun baþlangýç rotasyonu
    private Quaternion targetDoorRotation;    // Kapýnýn hedef rotasyonu
    private bool doorOpening = false;         // Kapýnýn açýlma durumunu kontrol eder

    private void Start()
    {
        // Baþlangýç rotasyonlarýný kaydet
        initialHandleRotation = doorHandle.localRotation;
        targetDoorRotation = door.localRotation;
    }

    private void Update()
    {
        if (isHandlePressed)
        {
            RotateHandle();
        }
        else
        {
            ResetHandle();
        }

        if (doorOpening)
        {
            OpenDoor();
        }
    }

    private void RotateHandle()
    {
        // Kapý kolunu aþaðý doðru bastýr
        float rotation = Mathf.Clamp(handleRotationLimit, 0, handleRotationLimit);
        doorHandle.localRotation = Quaternion.Euler(-rotation, 0, 0);

        // Kapý kolu tam bastýrýldýðýnda kapýyý aç
        if (Mathf.Abs(doorHandle.localRotation.eulerAngles.x) >= handleRotationLimit)
        {
            doorOpening = true;
            targetDoorRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        }
    }

    private void ResetHandle()
    {
        // Kapý kolu eski pozisyonuna döner
        doorHandle.localRotation = Quaternion.Lerp(doorHandle.localRotation, initialHandleRotation, Time.deltaTime * handleResetSpeed);
    }

    private void OpenDoor()
    {
        // Kapýyý yavaþça aç
        door.localRotation = Quaternion.Lerp(door.localRotation, targetDoorRotation, Time.deltaTime * doorOpenSpeed);

        // Kapý tam açýldýðýnda dur
        if (Quaternion.Angle(door.localRotation, targetDoorRotation) < 1f)
        {
            doorOpening = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // VR kullanýcýsý kapý koluna dokunduðunda
        if (other.CompareTag("PlayerHand")) // PlayerHand VR elinin tag'i olmalý
        {
            isHandlePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // VR kullanýcýsý kapý kolundan ayrýldýðýnda
        if (other.CompareTag("PlayerHand"))
        {
            isHandlePressed = false;
        }
    }
}
