using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandleController : MonoBehaviour
{
    [Header("Door Handle Settings")]
    public Transform doorHandle; // Kap� kolu (pivot noktas�na ba�l�)
    public Transform door;       // Kap� objesi
    public float handleRotationLimit = 30f; // Kap� kolu a�a�� bast�rma a��s�
    public float handleResetSpeed = 5f;     // Kap� kolunun eski pozisyonuna d�n�� h�z�

    [Header("Door Settings")]
    public float doorOpenAngle = 90f;      // Kap�n�n a��lma a��s�
    public float doorOpenSpeed = 2f;       // Kap�n�n a��lma h�z�
    public bool isHandlePressed = false;   // Kap� kolunun bas�l� olup olmad���n� kontrol eder

    private Quaternion initialHandleRotation; // Kap� kolunun ba�lang�� rotasyonu
    private Quaternion targetDoorRotation;    // Kap�n�n hedef rotasyonu
    private bool doorOpening = false;         // Kap�n�n a��lma durumunu kontrol eder

    private void Start()
    {
        // Ba�lang�� rotasyonlar�n� kaydet
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
        // Kap� kolunu a�a�� do�ru bast�r
        float rotation = Mathf.Clamp(handleRotationLimit, 0, handleRotationLimit);
        doorHandle.localRotation = Quaternion.Euler(-rotation, 0, 0);

        // Kap� kolu tam bast�r�ld���nda kap�y� a�
        if (Mathf.Abs(doorHandle.localRotation.eulerAngles.x) >= handleRotationLimit)
        {
            doorOpening = true;
            targetDoorRotation = Quaternion.Euler(0, doorOpenAngle, 0);
        }
    }

    private void ResetHandle()
    {
        // Kap� kolu eski pozisyonuna d�ner
        doorHandle.localRotation = Quaternion.Lerp(doorHandle.localRotation, initialHandleRotation, Time.deltaTime * handleResetSpeed);
    }

    private void OpenDoor()
    {
        // Kap�y� yava��a a�
        door.localRotation = Quaternion.Lerp(door.localRotation, targetDoorRotation, Time.deltaTime * doorOpenSpeed);

        // Kap� tam a��ld���nda dur
        if (Quaternion.Angle(door.localRotation, targetDoorRotation) < 1f)
        {
            doorOpening = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // VR kullan�c�s� kap� koluna dokundu�unda
        if (other.CompareTag("PlayerHand")) // PlayerHand VR elinin tag'i olmal�
        {
            isHandlePressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // VR kullan�c�s� kap� kolundan ayr�ld���nda
        if (other.CompareTag("PlayerHand"))
        {
            isHandlePressed = false;
        }
    }
}
