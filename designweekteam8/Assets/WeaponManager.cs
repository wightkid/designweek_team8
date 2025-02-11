using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public enum weaponState { melee, gun};
    public weaponState currentState;
    MeleeWeponMechanic weapon;
    BulletMechanic bullet;


    // Start is called before the first frame update
    void Start()
    {
        weapon = FindAnyObjectByType<MeleeWeponMechanic>();
        bullet = FindAnyObjectByType<BulletMechanic>();

        currentState = weaponState.melee; //start with the melee weapon
    }

    // Update is called once per frame
    void Update()
    {
        //Setting up the switch case statements for each weapon
        switch (currentState)
        {
            case weaponState.melee:
                weapon.AttackMechanic();
                break;
            case weaponState.gun:
                bullet.BulletFiring();
                break;
        }
    }
}
