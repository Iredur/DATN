using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingItem : MonoBehaviour, ICollectible
{
    public static event Action OnHealthCollected;
    public void Collect()
    {
        Destroy(gameObject);
        OnHealthCollected?.Invoke();
    }

}
