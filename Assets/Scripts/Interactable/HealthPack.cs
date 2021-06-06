using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : InteractableScript
{
    public int healAmount;
    public override void OnInterract()
    {
        base.OnInterract();

        Player.instance.TakeDamage(-healAmount);
    }
}
