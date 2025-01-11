using UnityEngine;
namespace MyProjectNamespace
{
    public class point5 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(5);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 5;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 5);
        }
    }
}
