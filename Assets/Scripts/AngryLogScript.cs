using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryLogScript : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public float speed;

    bool inRange;
    Vector3 pos;
    Vector3 origPos;
    float minDist;

    Vector3 moveDirection;
    Rigidbody rb;
    bool resetPos;


    Animator animator;
    private enum AnimationStateEnum
    {
        Idle = 0,
        Run = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
        resetPos = false;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkRange();
        SetAnimationState();
        if (inRange)
        {
            transform.LookAt(player.transform);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z) * speed;
            CheckPlatformRange();
            if (resetPos)
            {
                ResetPos();
            }

        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
    void checkRange()
    {
        if (player.transform.position.z >= 85 && player.transform.position.z <= 131)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }
    void CheckPlatformRange()
    {
        if (transform.position.x <= -21.5f || transform.position.x >= 25 || transform.position.z <= 81 || transform.position.z >= 131)
        {
            resetPos = true;
        }
        else
        {
            resetPos = false;
        }
    }
    void SetAnimationState()
    {
        AnimationStateEnum treeAnimationState;

        if (inRange)
        {
            treeAnimationState = AnimationStateEnum.Run;
        }
        else
        {
            treeAnimationState = AnimationStateEnum.Idle;
        }

        animator.SetInteger("treeState", (int)treeAnimationState);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            ResetPos();
        }
    }
    void ResetPos()
    {
        transform.position = origPos;
        rb.velocity = Vector3.zero;
        resetPos = false;
    }
}
