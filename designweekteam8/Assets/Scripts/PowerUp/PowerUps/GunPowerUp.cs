using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPowerUp : PowerUp
{
    public override void Use(GameObject player)
    {
        Debug.Log("Gun has been picked up!");

        BulletMechanic bullet = player.GetComponent<BulletMechanic>();
        MeleeWeponMechanic melee = player.GetComponent<MeleeWeponMechanic>();

        if (bullet) bullet.enabled = true;
        if(melee) bullet.enabled = false;
    }
}
