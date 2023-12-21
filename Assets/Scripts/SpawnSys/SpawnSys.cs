using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSys : MonoBehaviour
{
    public List<Transform> transforms = new List<Transform>();
    public List<GameObject> enemyPrefab = new List<GameObject>();
    private void Start()
    {
        foreach (var pos in transforms)
        {
            Instantiate(enemyPrefab[Random.Range(0, enemyPrefab.Count)], pos.position, Quaternion.identity);
        }
    }
}
