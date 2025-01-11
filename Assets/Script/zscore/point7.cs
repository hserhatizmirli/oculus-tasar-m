using UnityEngine;
namespace MyProjectNamespace
{
    public class point7 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(7);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 7;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 7);
        }
    }
}

