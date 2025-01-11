using UnityEngine;
namespace MyProjectNamespace
{
    public class point9 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(9);
        }
        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 9;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen Deðer: " + 9);
        }
    }
}
