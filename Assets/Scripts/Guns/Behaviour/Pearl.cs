using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : GunBehaviour
{
    public override void BulletSpawn(Vector3 spawnPos, Quaternion rotation, BulletType _bullet, PlayerController playerController)
    {
        base.BulletSpawn(spawnPos, rotation, _bullet, playerController);
        StartCoroutine(ResetPlayerControllerGunRotationAbility(playerController, 0.2f));
    }
}
