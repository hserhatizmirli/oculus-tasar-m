using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // Necessary namespace

public class FireBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        if (grabbable != null)
        {
            grabbable.activated.AddListener(FireBulletAction); // Correct method binding
        }
        else
        {
            Debug.LogError("XRGrabInteractable component not found!");
        }
    }

    // Update is called once per frame (not necessary in this case, so you can remove it if not used)
    void Update()
    {
        // Your update logic, if any
    }

    // The method that fires the bullet
    public void FireBulletAction(ActivateEventArgs args)
    {
        if (bullet != null && spawnPoint != null)
        {
            GameObject spawnedBullet = Instantiate(bullet);
            spawnedBullet.transform.position = spawnPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
            Destroy(spawnedBullet, 5); // Destroy the bullet after 5 seconds
        }
        else
        {
            Debug.LogError("Bullet or spawnPoint is not assigned!");
        }
    }
}
