using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WeaponAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private PlayableDirector attack;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<GameObject>();
        attack = weapon.GetComponentInChildren<PlayableDirector>();  
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            WeaponAttack();
        }
    }

    public void WeaponAttack()
    {
        weapon.SetActive(true);
        attack.Play();
    }
}
