using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public bool playerBullet = false;
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;

    GameObject lastDialog2;
    void Start()
    {
        if (playerBullet)
        {
            speed = WeaponInventory.instance.GetCurrentBulletSpeed();
        }

        rb.velocity = transform.right * speed;

        StartCoroutine(DestroyBulletCoroutine());

    }


    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Bullet bullet = hitInfo.GetComponent<Bullet>();
        if (bullet != null) return;

        if (hitInfo.gameObject.layer == LayerMask.NameToLayer("Interactable")) return;

        if (this.tag == "EnemyBullet" && hitInfo.tag == "Player")
        {
            Damageable damageable = hitInfo.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Destroy(gameObject);
            }
        }

        // Instantiate(impactEffect, gameObject.transform.position, gameObject.transform.rotation);

        if (this.tag == "usBullet" && hitInfo.tag == "enemy")
        {
            Destroy(gameObject);
            Damageable damageable = hitInfo.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                
            }
        }
        if(this.tag == "usBullet" && hitInfo.tag == "BOSS")
        {
            Destroy(gameObject);
            Damageable damageable = hitInfo.GetComponent<Damageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);

            }
        }

    }

    IEnumerator DestroyBulletCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        //After we have waited 5 seconds print the time again.

        Destroy(gameObject);
    }
}
