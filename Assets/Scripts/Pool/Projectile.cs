using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 firepoint;
    public List<Bullet> _projectiles = new List<Bullet>();
    [SerializeField]public int id;
    [SerializeField] public string name;
    public int receivedID=0;
    private bool allowList = false;

    private void Awake()
    {
        
    }

    public void ForAwake()
    {
        _projectiles = GetComponentsInChildren<Bullet>(true).ToList();
        _projectiles = _projectiles.Where(x => x != this).ToList();
        _projectiles = _projectiles.OrderByDescending(x => x.id).ToList();

        foreach (var VARIABLE in _projectiles)
        {
            VARIABLE._projectile = this;
        }
    }

    private void OnEnable()
    {
        if (allowList)
        {
            _projectiles[receivedID].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (!allowList)
            allowList = true;
        receivedID = 0;
    }
}
