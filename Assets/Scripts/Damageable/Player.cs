using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Damageable
{
    public static Player instance;

    public GameObject player;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fillImage;

    public LayerMask interactableLayer;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public override void Start()
    {
        base.Start();

        fillImage.color = gradient.Evaluate(1f);
    }
    
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        healthSlider.value = base.Health;

        fillImage.color = gradient.Evaluate(healthSlider.normalizedValue);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        InteractableScript interactable = hitInfo.GetComponent<InteractableScript>();
        if (interactable != null)
        {
            interactable.OnInterract();
        }

    }
}
