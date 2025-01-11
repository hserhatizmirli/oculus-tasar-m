using UnityEngine;
namespace MyProjectNamespace
{
    public class point8 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(8);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 8;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 8);
        }
    }
}
