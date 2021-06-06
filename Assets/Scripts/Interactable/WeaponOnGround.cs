using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOnGround : InteractableScript
{
    public GameObject weapon;

    public override void OnInterract()
    {
        base.OnInterract();

        WeaponInventory.instance.AddWeapon(weapon);
    }
}
