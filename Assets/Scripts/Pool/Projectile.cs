using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 firepoint;
    public List<Bullet> _bullet = new List<Bullet>();
    [SerializeField]public int id;
    [SerializeField] public string name;
    public int receivedID=0;
    private bool allowList = false;

    private void Awake()
    {
        
    }

    public void ForAwake()
    {
        _bullet = GetComponentsInChildren<Bullet>(true).ToList();
        _bullet = _bullet.Where(x => x != this).ToList();
        _bullet = _bullet.OrderByDescending(x => x.id).ToList();

        foreach (var VARIABLE in _bullet)
        {
            VARIABLE._projectile = this;
        }
    }

    private void OnEnable()
    {
        foreach (var VARIABLE in _bullet)
        {
            VARIABLE.gameObject.SetActive(false);
        }
        if (allowList)
        {
            _bullet[receivedID].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (!allowList)
            allowList = true;
        receivedID = 0;
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
