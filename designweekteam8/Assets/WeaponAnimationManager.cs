using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WeaponAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private PlayableDirector attack;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponentInChildren<GameObject>();
        attack = weapon.GetComponent<PlayableDirector>();  
        weapon.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightAttack();
            isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftAttack();
            isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            UpAttack();
            isAttacking = false;
        }
    }

    public void RightAttack()
    {
        isAttacking = true;
        weapon.SetActive(true);
        weapon.transform.position = new Vector3(1, 0.8f, 0);
        attack.Play();
    }

    public void LeftAttack()
    {
        isAttacking = true;
        weapon.SetActive(true);
        weapon.transform.position = new Vector3(-1, -0.8f, 0);
        attack.Play();
    }
    public void UpAttack()
    {
        weapon.SetActive(true);
        attack.Play();
    }
    public void DownAttack()
    {
        weapon.SetActive(true);
        attack.Play();
    }
}
