using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public float range;

    public void SetRange(float bulletRange)
    {
        range = bulletRange;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // �arp��ma an�nda mermiyi yok et
        Destroy(gameObject);
    }
}
