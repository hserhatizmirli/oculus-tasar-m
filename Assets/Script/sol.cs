using UnityEngine;

public class LeftButtonHandler : MonoBehaviour
{
    public GameObject objectToMove; // Hareket ettirilecek nesne (Poligono1)
    public float initialXPosition = -27.217f; // Soldaki butonlar için baþlangýç X koordinatý

    void Start()
    {
        // Nesnenin baþlangýç pozisyonunu ayarlama
        if (objectToMove != null)
        {
            objectToMove.transform.position = new Vector3(initialXPosition, objectToMove.transform.position.y, objectToMove.transform.position.z);
        }
    }

    public void OnLeftButtonClick(int xOffset)
    {
        MoveObject(xOffset);  // Nesneyi hareket ettir
    }

    void MoveObject(int xOffset)
    {
        if (objectToMove != null)
        {
            // Nesnenin mevcut pozisyonunu alýyoruz
            Vector3 newPosition = objectToMove.transform.position;
            newPosition.x = initialXPosition + xOffset; // X koordinatýna offset ekle
            objectToMove.transform.position = newPosition; // Yeni pozisyona taþý
            Debug.Log("Nesne yeni pozisyona taþýndý: " + newPosition);
        }
        else
        {
            Debug.LogWarning("Taþýnacak nesne atanmadý!");
        }
    }
}
