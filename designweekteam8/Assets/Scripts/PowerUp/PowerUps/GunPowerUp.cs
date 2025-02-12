using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun PowerUp", menuName = "Power Up/Weapons/Gun")]
public class GunPowerUp : PowerUp
{
    //When function is called, enables the gun firing script and disables the melee
    public override void Use(GameObject player)
    {
        Debug.Log("Gun has been picked up!");

        // Disable melee attack, enable gun attack
        player.GetComponent<MeleeWeponMechanic>().enabled = false;
        player.GetComponent<BulletMechanic>().enabled = true;
    }
}
