using UnityEngine; // Bu sat�r� ekle

namespace MyProjectNamespace
{
    public class point10 : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            Destroy(other.gameObject);
            DeductPoints(10);
        }

        void DeductPoints(int DamageAmount)
        {
            GlobalScore.CurrentScore += 10;
            Debug.Log("Score Updated: " + GlobalScore.CurrentScore + " Eklenen De�er: " + 10);
        }
    }
}
