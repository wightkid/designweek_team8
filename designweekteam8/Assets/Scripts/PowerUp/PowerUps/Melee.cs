using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Melee PowerUp", menuName = "Power Up/Melee")]
public class Melee : PowerUp
{
    public override void Use(GameObject player)
    {
        Debug.Log("Melee is in use!");

        //Disable the gun script
        // Disable melee attack, enable gun attack
        player.GetComponent<MeleeWeponMechanic>().enabled = true;
        player.GetComponent<BulletMechanic>().enabled = false;
    }
}
