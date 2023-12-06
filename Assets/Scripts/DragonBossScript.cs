using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragonBossScript : MonoBehaviour
{
    [SerializeField]
    GameState gameState;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject border1, border2, border3, border4;

    [SerializeField]
    public float speed;

    [SerializeField]
    public float health;

    [SerializeField]
    public float maxHealth;

    [SerializeField]
    FloatingHealthBar healthBar;

    float border1Z, border2X, border3Z, border4X;
    int currentState;
    Vector3 moveDirection;
    Vector3 currPlayerPos;
    Animator animator;
    Rigidbody rb;
    Vector3 origPos;
    float minDist = 7.5f;
    bool isGrounded;
    bool hitPlayer;

    float deathTimer = 2.133f;
    float timer;

    private enum AnimationStateEnum
    {
        Idle = 0,
        Run = 1,
        Walk = 2,
        AttackFlame = 3,
        AttackHand = 4,
        AttackMouth = 5,
        GetHit = 6,
        Die = 7
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        border1Z = border1.transform.position.z;
        border2X = border2.transform.position.x;
        border3Z = border3.transform.position.z;
        border4X = border4.transform.position.x;
        origPos = transform.position;
        hitPlayer = false;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimationState();
        transform.LookAt(player.transform);
        currPlayerPos = player.transform.position;
        if (currPlayerPos.x <= border2X || currPlayerPos.x >= border4X || currPlayerPos.z <= border1Z || currPlayerPos.z >= border3Z)
        {
            
            currentState = 0;
        }
        else
        {
            if (Vector3.Distance(transform.position, player.transform.position) >= minDist)
            {
                if (isGrounded)
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                    currentState = 1;
                    SetAnimationState();
                }
            }
            else
            {
                AttackChoice();
            }
        }
        if (transform.position.y < -3f)
        {
            transform.position = origPos;
        }
        Debug.Log(health);
        if (health <= 0)
        {
            currentState = 7;
            timer += Time.deltaTime;
            if (timer >= deathTimer)
            {
                Destroy(gameObject);
            }
        }
    }
    void AttackChoice()
    {
        int rand = Random.Range(1, 4);
        switch (rand)
        {
            case 1:
                currentState = 3;
                break;
            case 2: 
                currentState = 4;
                break;
            case 3:
                currentState = 5;
                break;
            default:
                currentState = 1;
                break;
        }
    }
    void SetAnimationState()
    {
        AnimationStateEnum dragonAnimationState;
        switch(currentState)
        {
            case 0:
                dragonAnimationState = AnimationStateEnum.Idle; 
                break;
            case 1:
                dragonAnimationState = AnimationStateEnum.Run;
                break;
            case 2:
                dragonAnimationState = AnimationStateEnum.Walk;
                break;
            case 3:
                dragonAnimationState = AnimationStateEnum.AttackFlame;
                currentState = 1;
                break;
            case 4:
                dragonAnimationState = AnimationStateEnum.AttackHand;
                currentState = 1;
                break;
            case 5:
                dragonAnimationState = AnimationStateEnum.AttackMouth;
                currentState = 1;
                break;
            case 6:
                dragonAnimationState = AnimationStateEnum.GetHit;
                break;
            case 7:
                dragonAnimationState = AnimationStateEnum.Die;
                break;
            default:
                dragonAnimationState = AnimationStateEnum.Idle;
                break;
        }

        animator.SetInteger("dragonState", (int)dragonAnimationState);
    }
    void MoveBack()
    {
        transform.position = origPos;
        currentState = 1;
        hitPlayer = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag.Equals("Player") && !hitPlayer)
        {
            hitPlayer = true;
            gameState.health -= 10;
            gameState.takeDamage = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!gameState.damageDone)
        {
            if (other.gameObject.tag.Equals("Sword"))
            {
                Debug.Log("Dragon Hit");
                currentState = 6;
                health -= 10;
                healthBar.UpdateHealthBar(health, maxHealth);
                gameState.damageDone = true;
            }
        }
    }
}
