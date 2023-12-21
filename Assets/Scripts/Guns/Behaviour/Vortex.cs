using System.Collections;
using UnityEngine;

public class Vortex : GunBehaviour
{
    Vector2 subDirection;
    float angle;
    Quaternion _rotationDown;
    Quaternion _rotationUp;
    bool canContinue = true;
    float timeSkip = 0.05f;
    private void Start()
    {
        angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        _rotationDown = Quaternion.AngleAxis(angle - 2, Vector3.forward);
        _rotationUp = Quaternion.AngleAxis(angle + 2, Vector3.forward);
    }

    public override void BulletSpawn(Vector3 spawnPos, Quaternion rotation, BulletType _bullet, PlayerController playerController)
    {
        angle = Mathf.Atan2(transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        _rotationDown = Quaternion.AngleAxis(angle - 2, Vector3.forward);
        _rotationUp = Quaternion.AngleAxis(angle + 2, Vector3.forward);
        StartCoroutine(BurstShot(spawnPos, rotation, _bullet, 0));
        StartCoroutine(BurstShot(spawnPos, _rotationDown, _bullet, timeSkip));
        StartCoroutine(BurstShot(spawnPos, _rotationUp, _bullet, timeSkip * 2));
        StartCoroutine(BurstShot(spawnPos, rotation, _bullet, timeSkip * 3));
        StartCoroutine(BurstShot(spawnPos, _rotationDown, _bullet, timeSkip * 4));
        StartCoroutine(BurstShot(spawnPos, _rotationUp, _bullet, timeSkip * 5));
        StartCoroutine(ResetPlayerControllerGunRotationAbility(playerController, timeSkip * 5f));
    }
    IEnumerator BurstShot(Vector3 spawnPos, Quaternion rotation, BulletType _bullet, float delay)
    {
        canContinue = false;

        yield return new WaitForSeconds(delay);
        CallBullet(spawnPos, rotation, _bullet);
        canContinue = true;
    }
}
