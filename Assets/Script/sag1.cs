using UnityEngine;

public class Right_ButtonHandler : MonoBehaviour
{
    public GameObject objectToMove; // Hareket ettirilecek nesne (Poligono3)
    public float initialXPosition = -32.217f; // Sağdaki butonlar için başlangıç X koordinatı

    void Start()
    {
        // Nesnenin başlangıç pozisyonunu ayarlama
        if (objectToMove != null)
        {
            objectToMove.transform.position = new Vector3(initialXPosition, objectToMove.transform.position.y, objectToMove.transform.position.z);
        }
    }

    public void OnRightButtonClick(int xOffset)
    {
        MoveObject(xOffset);  // Nesneyi hareket ettir
    }

    void MoveObject(int xOffset)
    {
        if (objectToMove != null)
        {
            // Nesnenin mevcut pozisyonunu alıyoruz
            Vector3 newPosition = objectToMove.transform.position;
            newPosition.x = initialXPosition + xOffset; // X koordinatına offset ekle
            objectToMove.transform.position = newPosition; // Yeni pozisyona taşı
            Debug.Log("Nesne yeni pozisyona taşındı: " + newPosition);
        }
        else
        {
            Debug.LogWarning("Taşınacak nesne atanmadı!");
        }
    }
}
