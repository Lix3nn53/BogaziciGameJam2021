using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public int ammo;
    public int ammoCapacity;
    public int damage;
    public float cooldown = 0.5f;
    public Animator playerAnimator;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public int bulletPerShot = 1;
    public float totalAngle = 60;
    public float bulletSpeed = 20f;

    public string fireSound;
    public string reloadSound;

    bool isReloading;

    private void Start()
    {
        ammo = ammoCapacity;
        isReloading = false;
    }

    /// <summary>
    /// @return - true if reload
    /// </summary>
    public bool Fire()
    {
        if (ammo <= 0)
        {
            Reload();
            return true;
        }

        if (isReloading)
        {
            return false;
        }

        int amountToFire = 1;

        if (bulletPerShot > 1)
        {
            amountToFire = bulletPerShot;

            if (ammo < bulletPerShot)
            {
                amountToFire = ammo;
            }

            // Given an Axis-Angle rotation
            Vector3 axis = firePoint.forward;
            Debug.Log("axis: " + axis);

            float maxAngle = totalAngle / 2f;
            float angleIncrement = totalAngle / (amountToFire - 1);
            for (int i = 0; i < amountToFire; i++)
            {
                float angle = -maxAngle + (angleIncrement * i);
                Debug.Log("angle: " + angle);

                if (angle == 0)
                {
                    GameObject bulletObjectt = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    bulletObjectt.GetComponent<Bullet>().SetDamage(damage);
                    continue;
                }

                if (axis.z < 0)
                {
                    angle += 180;
                }

                angle *= Mathf.Deg2Rad;

                // Calculated the Quaternion components
                Vector3 vectorComponent = axis * Mathf.Sin(angle / 2);
                float scalarComponent = Mathf.Cos(angle / 2);

                // Construct the result
                Quaternion rotation = new Quaternion(
                  vectorComponent.x,
                  vectorComponent.y,
                  vectorComponent.z,
                  scalarComponent);

                GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, rotation);
                bulletObject.GetComponent<Bullet>().SetDamage(damage);
            }
        } else
        {
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        ammo -= amountToFire;
        AudioManager.instance.Play(fireSound);

        return false;
    }

    public void Reload()
    {
        if (isReloading) return;

        if (ammo < ammoCapacity)
        {
            isReloading = true;
            playerAnimator.SetBool("Reload", true);
            AudioManager.instance.Play(reloadSound);

            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(1);

        isReloading = false;
        playerAnimator.SetBool("Reload", false);
        ammo = ammoCapacity;
    }
}
