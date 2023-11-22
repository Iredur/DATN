using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class small_player_bullet : Bullet
{
    

    public override void BulletTravel()
    {
        if (timer < decayTime)
        {
            this.transform.position += travelSpeed * Time.deltaTime * transform.right;
        }
        else
        {
            //gameObject.SetActive(false);
            _projectile.gameObject.SetActive(false);
        }
    }

}
