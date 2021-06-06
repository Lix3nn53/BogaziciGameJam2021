using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastDialog : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            trigger.SetActive(true);
        }
    }
}
