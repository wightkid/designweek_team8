using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MeleeWeponMechanic : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    private bool isAttacking = false; //checking when the player confirms input

    private void Start()
    {
        this.enabled = true;
        if (weapon == null)
        {
            weapon = transform.GetChild(0).gameObject;
        }
        weapon.SetActive(false); //Set weapon collider to false at the start
    }
    private void Update()
    {
        AttackMechanic();
    }

    public void AttackMechanic()
    {
        if (Input.GetKeyDown(KeyCode.L) && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(1.2f, 0.8f, 0), new Vector3(1.2f, -0.5f, 0)));
        }
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(-1.2f, -0.8f, 0), new Vector3(-1.2f, 0.5f, 0)));
        }
        if (Input.GetKeyDown(KeyCode.I) && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(-1.2f, 1f, 0), new Vector3(1.2f, 1f, 0)));
        }
        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(1.2f, -1f, 0), new Vector3(-1.2f, -1f, 0)));
        }
    }

    private IEnumerator SwipeAttack(Vector3 start, Vector3 end)
    {
        isAttacking = true; //When this function is called, set is attacking to true
        weapon.SetActive(true);
        weapon.transform.localPosition = start; //When function is called, set the position of attack collider to start

        float attackDuration = 0.2f;
        float elapsedTime = 0;

        while (elapsedTime < attackDuration)
        {
            weapon.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / attackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        weapon.transform.localPosition = end; 
        yield return new WaitForSeconds(attackDuration);

        ResetWeaponPosition();
        isAttacking=false;

    }

    private void ResetWeaponPosition()
    {
        weapon.SetActive(false);
        weapon.transform.localPosition = Vector3.zero;
    }

    //[SerializeField] private GameObject weapon;
    //[SerializeField] private PlayableDirector attack;
    //private bool isAttacking = false;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    weapon = GetComponentInChildren<GameObject>();
    //    attack = weapon.GetComponent<PlayableDirector>();
    //    weapon.SetActive(false);
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        RightAttack();
    //        isAttacking = false;
    //    }
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        LeftAttack();
    //        isAttacking = false;
    //    }
    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        UpAttack();
    //        isAttacking = false;
    //    }
    //}

    //public void RightAttack()
    //{
    //    isAttacking = true;
    //    weapon.SetActive(true);
    //    weapon.transform.position = new Vector3(1, 0.8f, 0);
    //    attack.Play();
    //}

    //public void LeftAttack()
    //{
    //    isAttacking = true;
    //    weapon.SetActive(true);
    //    weapon.transform.position = new Vector3(-1, -0.8f, 0);
    //    attack.Play();
    //}
    //public void UpAttack()
    //{
    //    weapon.SetActive(true);
    //    attack.Play();
    //}
    //public void DownAttack()
    //{
    //    weapon.SetActive(true);
    //    attack.Play();
    //}
}
