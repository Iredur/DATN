using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private List<GameObject> pooledBullet = new List<GameObject>();
    [SerializeField] private List<GameObject> pooledFX = new List<GameObject>();
    [SerializeField] private int bulletAmount = 500;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] GameObject explosionFxPrefabs;
    [SerializeField] private int fxAmount = 100;
    [SerializeField] GameObject bulletPool;
    [SerializeField] GameObject vfxPool;
    Projectile projectile;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            GameObject gobj = Instantiate(bulletPrefabs);
            gobj.SetActive(false);
            pooledBullet.Add(gobj);
            gobj.transform.parent = bulletPool.transform;
        }
        for (int i = 0; i < fxAmount; i++)
        {
            GameObject gobj = Instantiate(explosionFxPrefabs);
            gobj.SetActive(false);
            pooledFX.Add(gobj);
            gobj.transform.parent = vfxPool.transform;
        }
    }

    public GameObject getPooledBullet(Vector3 firepoint, Quaternion rotation, BulletType bulletType, bool isFriendly)
    {
        for (int i = 0; i < pooledBullet.Count; i++)
        {
            if (!pooledBullet[i].activeInHierarchy)
            {
                projectile = pooledBullet[i].GetComponent<Projectile>();
                projectile.firepoint = firepoint;
                projectile.isFriendly = isFriendly;
                projectile.UpdateBulletData(bulletType);
                pooledBullet[i].transform.rotation = rotation;
                return pooledBullet[i];
            }
        }
        return null;
    }
    public GameObject getPooledFX(Quaternion rotation, int explosionSize, bool isFriendly, int damage)
    {
        for (int i = 0; i < pooledFX.Count; i++)
        {
            if (!pooledFX[i].activeInHierarchy)
            {
                pooledFX[i].transform.rotation = rotation;
                pooledFX[i].transform.localScale = new Vector3(explosionSize, explosionSize, 1);
                pooledFX[i].GetComponent<ExplosionCollision>().isFriendly = isFriendly;
                pooledFX[i].GetComponent<ExplosionCollision>().damage = damage;
                return pooledFX[i];
            }
        }
        return null;
    }

}
