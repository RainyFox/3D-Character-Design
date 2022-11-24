using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class TPCharacter : MonoBehaviour
{
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float maxSpeed = 6;
    [SerializeField] float jumpForce = 6;
    [SerializeField] float gravityScale = 1;

    Animator animator;
    Rigidbody rigidbody;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    bool onGround = false;
    float checkDelayTime = 0;
    public void Move(Vector3 moveDir, bool isJump)
    {
        onGround = Physics.CheckSphere(transform.position, 0.3f, whatIsGround, QueryTriggerInteraction.Ignore);
        if (onGround && Time.time > checkDelayTime)
        {
            Vector3 velocity = Vector3.zero;
            if (moveDir.magnitude > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);

                velocity = moveDir * maxSpeed * animator.GetFloat("MoveCurve");
                velocity.y = rigidbody.velocity.y;
            }

            animator.SetFloat("Speed", moveDir.magnitude, 0.25f, Time.deltaTime);
            rigidbody.velocity = velocity * Mathf.Clamp01(animator.GetFloat("Speed"));

            if (isJump)
            {
                checkDelayTime = Time.time + 0.5f;
                rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }

        animator.SetBool("OnGround", onGround);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(Physics.gravity * gravityScale);
    }
}
