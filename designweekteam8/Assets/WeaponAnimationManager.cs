using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationManager : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        WeaponAttack();
    }

    public void WeaponAttack()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Attack Trigger");
        }
    }
}
