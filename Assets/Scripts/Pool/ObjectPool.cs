using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private List<GameObject> pooledObject = new List<GameObject>();

    [SerializeField] private int amount = 500;
    [SerializeField] private GameObject bulletPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gobj = Instantiate(bulletPrefabs);
            gobj.GetComponent<Projectile>().ForAwake();
            gobj.SetActive(false);
            pooledObject.Add(gobj);
            gobj.transform.parent = this.transform;
        }
    }

    public GameObject getPooledGameobject(Vector3 firepoint, Quaternion rotation, int shotID)
    {
        for (int i = 0; i < pooledObject.Count; i++)
        {
            if (!pooledObject[i].activeInHierarchy)
            {
                pooledObject[i].GetComponent<Projectile>().firepoint = firepoint;
                pooledObject[i].transform.rotation = rotation;
                pooledObject[i].GetComponent<Projectile>().receivedID = shotID;
                return pooledObject[i];
            }
        }
        return null;
    }
}
