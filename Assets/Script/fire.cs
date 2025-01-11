using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    public bool SingleFire = true;
    public float bulletSpeed = 20f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float firerate = 600;
    private bool isShooting;
    //float remainingBalls;
    //Action stoppedShooting;
    private float elapsedShootingTime;
    private float shootDelay;
    //public AudioSource fireAudio;

    //public static event Action pistolFire;
    public UnityEvent OnFire;

    private void Awake()
    {
        shootDelay = 1000 * 60 / firerate;
    }

    [ContextMenu("Fire")]
    public void doFire()
    {
        if (!SingleFire)
        {
            return;
        }
        GameObject createBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        createBullet.GetComponent<Rigidbody>().velocity = bulletSpeed * bulletSpawn.forward;
        //fireAudio.Play();
        Destroy(createBullet, 5f);
        //pistolFire?.Invoke();
        OnFire?.Invoke();
    }





    private void FixedUpdate()
    {
        if (SingleFire || !isShooting)
        {
            return;
        }

        //if (remainingBalls == 0)
        //{
        //    isShooting = false;
        //    stoppedShooting.Raise();
        //    return;
        //}
        while (elapsedShootingTime > shootDelay / 1000)
        {
            elapsedShootingTime -= shootDelay / 1000;
            Shoot(elapsedShootingTime);
        }
        elapsedShootingTime += Time.fixedDeltaTime;
    }

    private void Shoot(float timeAlreadyElapsed)
    {
        var newBall = Instantiate(bulletPrefab);
        //newBall.transform.localScale = ballScale * Vector3.one;
        if (newBall.TryGetComponent<Rigidbody>(out var rigidbody))
        {
            //var ballSpeed = bulletSpeed / rigidbody.mass;
            newBall.transform.position = bulletSpawn.position;// + timeAlreadyElapsed * bulletSpeed * bulletSpawn.forward;
            rigidbody.velocity = bulletSpawn.forward * bulletSpeed;
            // rigidbody.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.VelocityChange);
        }
        OnFire?.Invoke();
        Destroy(newBall, 3);
        //remainingBalls--;
    }

    [ContextMenu("StartFiring")]
    public void StartShooting()
    {
        if (SingleFire)
        {
            doFire();
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