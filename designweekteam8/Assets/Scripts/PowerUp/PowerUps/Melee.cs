using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : PowerUp
{
    public override void Use(GameObject player)
    {
        Debug.Log("Melee is in use!");

        BulletMechanic bullet = player.GetComponent<BulletMechanic>();
        MeleeWeponMechanic melee = player.GetComponent<MeleeWeponMechanic>();

        if (bullet) bullet.enabled = false;
        if (melee) melee.enabled = true;
    }
}
