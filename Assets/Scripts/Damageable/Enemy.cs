using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Damageable
{
    public int damage;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public Slider healthSlider;

    GameObject player;
    Vector2 distance;
    Vector2 distance2;
    public GameObject trigger2;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        healthSlider.value = base.Health;
    }

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");

        //Start the coroutine we define below named ExampleCoroutine.
        StartCoroutine(ExampleCoroutine());


    }

    void Update()
    {
        distance = transform.position - player.transform.position;
        distance2 = player.transform.position - transform.position;
        Vector3 vector3 = player.transform.position - firePoint.transform.position;
        vector3.z = 0;
        firePoint.transform.right = vector3.normalized;

        if (this.tag == "Boss")
        {
            if (base.Health <= 50)
            {
                trigger2.SetActive(true);
            }
        }
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        if (distance.x <= 15)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletObject.GetComponent<Bullet>().SetDamage(damage);
        }
        else if (distance.x >= -15 && distance.x<=15)
        {
            GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletObject.GetComponent<Bullet>().SetDamage(damage);
        }

        StartCoroutine(ExampleCoroutine());
    }
}
