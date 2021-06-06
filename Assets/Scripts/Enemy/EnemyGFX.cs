using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    public int lastRotater = 1;

     void Start()
    {
        Rigidbody2D rg = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            //if (lastRotater != 0)
            //{
            //    transform.Rotate(0, 180, 0);

            //    lastRotater = 0;
            //}
            transform.localScale = new Vector3(-lastRotater, lastRotater, 1f);
        }
        else if(aiPath.desiredVelocity.x<=0.01f)
        {
            //if (lastRotater != 1)
            //{
            //    transform.Rotate(0, 180, 0);

            //    lastRotater = 1;
            //}
            transform.localScale = new Vector3(lastRotater, lastRotater, 1f);
        }

        //if (transform.position.y >= 0.83f)
        //{
        //    transform.position = new Vector3(transform.position.x, Mathf.Clamp(5f, 0.80f, 0.83f), transform.position.z);
        //}
        //else if (transform.position.y <= 0.8f)
        //{
        //    transform.position = new Vector3(transform.position.x, Mathf.Clamp(5f, 0.80f, 0.83f), transform.position.z);
        //}
    }
}
