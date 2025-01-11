using UnityEngine;
namespace MyProjectNamespace
{
    public class point6 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(6);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 6;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 6);
        }
    }
}
