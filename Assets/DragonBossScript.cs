using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragonBossScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject border1, border2, border3, border4;

    [SerializeField]
    public float speed;

    float border1Z, border2X, border3Z, border4X;
    int currentState;
    Vector3 moveDirection;
    Vector3 currPos;
    Animator animator;
    Rigidbody rb;
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
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();
        transform.LookAt(player.transform);
        currPos = transform.position;
        if (currPos.x <= border2X || currPos.x >= border4X || currPos.z <= border1Z || currPos.z >= border3Z)
        {
            Debug.Log("Not moving");
            rb.velocity = Vector3.zero;
            currentState = 0;
        }
        else
        {
            Debug.Log("Moving");
            Vector3 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * speed;
            currentState = 1;
        }
        SetAnimationState();
    }
    void CheckAttackRange()
    {
        if ((player.transform.position - transform.position).sqrMagnitude<3*3)
        {
            AttackChoice();
        }
    }
    void AttackChoice()
    {

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
                break;
            case 4:
                dragonAnimationState = AnimationStateEnum.AttackHand;
                break;
            case 5:
                dragonAnimationState = AnimationStateEnum.AttackMouth;
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
}
