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
    //        StartCoroutine(SwipeAttack(new Vector3(1f, 1f, 0), new Vector3(1f, -0.8f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.J) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(-1f, -1f, 0), new Vector3(-1f, 0.8f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.I) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(-1.5f, 1f, 0), new Vector3(1.5f, 1f, 0)));
    //    }
    //    if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
    //    {
    //        StartCoroutine(SwipeAttack(new Vector3(1.5f, -1f, 0), new Vector3(-1.5f, -1f, 0)));
    //    }
    //}

    //private IEnumerator SwipeAttack(Vector3 start, Vector3 end)
    //{
    //    isAttacking = true; //When this function is called, set is attacking to true
    //    attackCollider.SetActive(true);
    //    attackCollider.transform.localPosition = start; //Set initial position

    //    float attackDuration = 0.2f;
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
    //    isAttacking=false;

    //}

    //private void ResetWeaponPosition()
    //{
    //    attackCollider.SetActive(false);
    //    attackCollider.transform.localPosition = Vector3.zero;
    //}

    [SerializeField] private GameObject attackCollider;
    private bool isAttacking = false; //checking when the player confirms input
    private InputActionAsset inputActions;
    private InputActionMap inputMap;
    private InputAction attackUp;
    private InputAction attackDown;
    private InputAction attackLeft;
    private InputAction attackRight;

    private void Awake()
    {
        inputActions = this.GetComponent<PlayerInput>().actions;
        inputMap = inputActions.FindActionMap("Player");

        attackUp = inputMap.FindAction("AttackUp");
        attackDown = inputMap.FindAction("AttackDown");
        attackLeft = inputMap.FindAction("AttackLeft");
        attackRight = inputMap.FindAction("AttackRight");
    }

    private void OnEnable()
    {
        attackUp.performed += OnAttackUp;
        attackDown.performed += OnAttackDown;
        attackLeft.performed += OnAttackLeft;
        attackRight.performed += OnAttackRight;

        inputMap.Enable();
    }

    private void OnDisable()
    {
        attackUp.performed -= OnAttackUp;
        attackDown.performed -= OnAttackDown;
        attackLeft.performed -= OnAttackLeft;
        attackRight.performed -= OnAttackRight;

        inputMap.Disable();
    }

    private void Start()
    {
        //Enable this script when first starting 
        this.enabled = true;
        if (attackCollider == null)
        {
            attackCollider = transform.GetChild(1).gameObject;
        }
        attackCollider.SetActive(false); //Set weapon collider to false at the start
    }

    private void OnAttackUp(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(-1.5f, 1f, 0), new Vector3(1.5f, 1f, 0)));
        }
    }

    private void OnAttackDown(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(1.5f, -1f, 0), new Vector3(-1.5f, -1f, 0)));
        }
    }

    private void OnAttackLeft(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(-1f, -1f, 0), new Vector3(-1f, 0.8f, 0)));
        }
    }

    private void OnAttackRight(InputAction.CallbackContext context)
    {
        if (context.performed && !isAttacking)
        {
            StartCoroutine(SwipeAttack(new Vector3(1f, 1f, 0), new Vector3(1f, -0.8f, 0)));
        }
    }

    private IEnumerator SwipeAttack(Vector3 start, Vector3 end)
    {
        isAttacking = true; //When this function is called, set is attacking to true
        attackCollider.SetActive(true);
        attackCollider.transform.localPosition = start; //Set initial position

        float attackDuration = 0.2f;
        float elapsedTime = 0;

        //Over the duration of the attack, move the collider from start to end position
        while (elapsedTime < attackDuration)
        {
            attackCollider.transform.localPosition = Vector3.Lerp(start, end, elapsedTime / attackDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        attackCollider.transform.localPosition = end;
        yield return new WaitForSeconds(attackDuration);

        ResetWeaponPosition();
        isAttacking = false;
    }

    private void ResetWeaponPosition()
    {
        attackCollider.SetActive(false);
        attackCollider.transform.localPosition = Vector3.zero;
    }

}
