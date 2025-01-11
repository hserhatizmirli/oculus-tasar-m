using UnityEngine;
namespace MyProjectNamespace
{
    public class point4 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(4);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 4;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 4);
        }
    }
}
