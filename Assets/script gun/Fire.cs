using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace MyGame.Weapons
{
    public class FireController : MonoBehaviour
    {
        public bool SingleFire = true;
        public float bulletSpeed = 20f;
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public float fireRate = 600;
        private bool isShooting;
        private float elapsedShootingTime;
        private float shootDelay;
        public UnityEvent OnFire;

        private void Awake()
        {
            shootDelay = 60f / fireRate; // Dakikada atýþ sayýsýna göre gecikme.
        }

        [ContextMenu("Fire")]
        public void DoFire()
        {
            if (!SingleFire)
            {
                return;
            }

            GameObject createdBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            if (createdBullet.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.velocity = bulletSpeed * bulletSpawn.forward;
            }
            Destroy(createdBullet, 5f);
            OnFire?.Invoke();
        }

        private void FixedUpdate()
        {
            if (SingleFire || !isShooting)
            {
                return;
            }

            elapsedShootingTime += Time.fixedDeltaTime;
            while (elapsedShootingTime >= shootDelay)
            {
                elapsedShootingTime -= shootDelay;
                DoFire();
            }
        }

        [ContextMenu("Start Shooting")]
        public void StartShooting()
        {
            if (SingleFire)
            {
                DoFire();
            }
            else
            {
                isShooting = true;
            }
        }

        public void StopShooting()
        {
            isShooting = false;
        }
    }
}
