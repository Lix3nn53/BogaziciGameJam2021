using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    public virtual void OnInterract()
    {
        Destroy(gameObject);
    }

}
