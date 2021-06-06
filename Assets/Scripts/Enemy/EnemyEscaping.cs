using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEscaping : Damageable
{

    GameObject character;
    Vector2 distan;
    bool booli;
    float currentX;
    public int whichRotation=1;

    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        distan = transform.position - character.transform.position;
        if (distan.x >= 0)
        {
            currentX = transform.position.x + 0.01f *whichRotation;
        }
        else
        {
            currentX = transform.position.x -0.01f * whichRotation;
        }
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }
}
