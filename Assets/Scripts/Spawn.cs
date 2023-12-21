using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] public List<Transform> spawnLocation;
    [SerializeField] public List<GameObject> enemy;
    [SerializeField] public List<GameObject> survivingEnemy;
    GameObject g;
    //the spawn works......kinda
    private void Start()
    {
        spawnEnemy();
    }
    public void spawnEnemy()
    {
        for (int i = 0; i < spawnLocation.Count; i++)
        {
            int x = Random.Range(0, enemy.Count);
            g = Instantiate(enemy[x], spawnLocation[i].position, spawnLocation[i].rotation);
            survivingEnemy.Add(g);
        }
    }
    private void Update()
    {
        int d = 0;
        foreach (var VAR in survivingEnemy)
        {
            if (VAR == null) d++;
        }
        if (d == survivingEnemy.Count)
            spawnEnemy();
    }
}
