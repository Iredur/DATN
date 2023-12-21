using System.Collections;
using UnityEngine;

public class GunBehaviour : MonoBehaviour
{

    public BulletType normalBullet;
    public BulletType altBullet;
    private float time;
    bool canFire = true;

    public Guns weaponType;
    public void Shoot(Vector3 spawnPos, Quaternion rotation, float fireRateMultiplier, PlayerController playercontroller)
    {
        if (canFire)
        {
            BulletSpawn(spawnPos, rotation, normalBullet, playercontroller);
            canFire = false;
            playercontroller.freezeRotation = true;
        }
    }
    private void Update()
    {
        if (!canFire)
        {
            time += Time.deltaTime;
            float nextTimeToFire = 1 / weaponType.fireRate;
            if (time >= nextTimeToFire)
            {
                canFire = true;
                time = 0;
            }
        }
    }
    public void AltShoot(Vector3 spawnPos, Quaternion rotation, PlayerController playerController)
    {
        CallBullet(spawnPos, rotation, altBullet);
    }
    public virtual void BulletSpawn(Vector3 spawnPos, Quaternion rotation, BulletType _bullet, PlayerController playerController)
    {
        CallBullet(spawnPos, rotation, _bullet);
    }
    public void CallBullet(Vector3 spawnPos, Quaternion rotation, BulletType _bullet)
    {
        GameObject bullet = ObjectPool.instance.getPooledBullet(spawnPos, rotation, _bullet, true);
        if (bullet != null)
        {
            bullet.transform.position = spawnPos;
            bullet.SetActive(true);
        }
    }
    public IEnumerator ResetPlayerControllerGunRotationAbility(PlayerController playerController, float duration)
    {
        yield return new WaitForSeconds(duration);
        playerController.freezeRotation = false;
    }
}
