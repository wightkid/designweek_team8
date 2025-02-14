using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeWeponMechanic : MonoBehaviour
{
    //[SerializeField] private GameObject attackCollider;
    //private bool isAttacking = false; //checking when the player confirms input

    //private void Start()
    //{
    //    //Enable this script when first starting 
    //    this.enabled = true;
    //    if (attackCollider == null)
    //    {
    //        attackCollider = transform.GetChild(1).gameObject;
    //    }
    //    attackCollider.SetActive(false); //Set weapon collider to false at the start
    //}
    //private void Update()
    //{
    //    AttackMechanic();
    //}

    //public void AttackMechanic()
    //{
    //    if (Input.GetKeyDown(KeyCode.L) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(.5f, .5f, 0), new Vector3(.5f, -0.5f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(-.5f, -.5f, 0), new Vector3(-.5f, 0.5f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.I) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(-.5f, .5f, 0), new Vector3(.5f, .5f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(.5f, -.5f, 0), new Vector3(-.5f, -.5f, 0)));
    //    }
    //}

    //private IEnumerator SwipeAttack(Vector3 start, Vector3 end)
    //{
    //    isAttacking = true; //When this function is called, set is attacking to true
    //    attackCollider.SetActive(true);
    //    attackCollider.transform.localPosition = start; //Set initial position

    //    float attackDuration = 0.1f;
    //    float elapsedTime = 0;

    //    //Over the duration of the attack, move the collider from start to end position
    //    while (elapsedTime < attackDuration)
    //    {
    //        attackCollider.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / attackDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }

    //    attackCollider.transform.localPosition = end;
    //    yield return new WaitForSeconds(attackDuration);

    //    ResetWeaponPosition();
    //    isAttacking = false;

    //}

    //private void ResetWeaponPosition()
    //{
    //    attackCollider.SetActive(false);
    //    attackCollider.transform.localPosition = Vector3.zero;
    //}

    //New Input Code (Couldn't get to work on my end)--------------------------------------------------------------------------------------------------------

    public GameObject attackCollider;
    private bool isattacking = false; //checking when the player confirms input
    private InputActionAsset inputactions;
    private InputActionMap inputmap;
    private InputAction attackup;
    private InputAction attackdown;
    private InputAction attackleft;
    private InputAction attackright;

    PlayerStats playerstats;

    private void Awake()
    {
        playerstats = FindAnyObjectByType<PlayerStats>();

        inputactions = this.GetComponent<PlayerInput>().actions;
        inputmap = inputactions.FindActionMap("player");

        attackup = inputmap.FindAction("AttackUp");
        attackdown = inputmap.FindAction("AttackDown");
        attackleft = inputmap.FindAction ("AttackLeft");
        attackright = inputmap.FindAction("AttackRight");
    }

    private void OnEnable()
    {
        attackup.performed += onattackup;
        attackdown.performed += onattackdown;
        attackleft.performed += onattackleft;
        attackright.performed += onattackright;

        inputmap.Enable();
    }

    private void OnDisable()
    {
        attackup.performed -= onattackup;
        attackdown.performed -= onattackdown;
        attackleft.performed -= onattackleft;
        attackright.performed -= onattackright;

        inputmap.Disable();
    }

    private void Start()
    {
        //enable this script when first starting 
        this.enabled = true;
        if (attackCollider == null)
        {
            attackCollider = transform.GetChild(1).gameObject;
        }
        attackCollider.SetActive(false); //set weapon collider to false at the start
    }

    private void onattackup(InputAction.CallbackContext context)
    {
        if (context.performed && !isattacking || Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(swipeattack(new Vector3(-1.5f, 1f, 0), new Vector3(1.5f, 1f, 0)));
        }
    }

    private void onattackdown(InputAction.CallbackContext context)
    {
        if (context.performed && !isattacking || Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(swipeattack(new Vector3(1.5f, -1f, 0), new Vector3(-1.5f, -1f, 0)));
        }
    }

    private void onattackleft(InputAction.CallbackContext context)
    {
        if (context.performed && !isattacking || Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(swipeattack(new Vector3(-1f, -1f, 0), new Vector3(-1f, 0.8f, 0)));
        }
    }

    private void onattackright(InputAction.CallbackContext context)
    {
        if (context.performed && !isattacking || Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(swipeattack(new Vector3(1f, 1f, 0), new Vector3(1f, -0.8f, 0)));
        }
    }

    private IEnumerator swipeattack(Vector3 start, Vector3 end)
    {
        isattacking = true; //when this function is called, set is attacking to true
        attackCollider.SetActive(true);
        attackCollider.transform.localPosition = start; //set initial position

        float attackduration = 0.2f;
        float elapsedtime = 0;

        //over the duration of the attack, move the collider from start to end position
        while (elapsedtime < attackduration)
        {
            attackCollider.transform.localPosition = Vector3.Lerp(start, end, elapsedtime / attackduration);
            elapsedtime += Time.deltaTime;
            yield return null;
        }

        attackCollider.transform.localPosition = end;
        yield return new WaitForSeconds(attackduration);

        resetweaponposition();
        isattacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerstats.health--;
            collision.gameObject.transform.position = Vector2.right;
        }
    }

    private void resetweaponposition()
    {
        attackCollider.SetActive(false);
        attackCollider.transform.localPosition = Vector3.zero;
    }

}
