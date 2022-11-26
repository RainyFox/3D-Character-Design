using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPCharacter))]
public class TPController : MonoBehaviour
{
    [SerializeField] Transform weaponSlotOnBack, weaponSlotOnHand;
    [SerializeField] Transform weapon;
    Animator animator;
    TPCharacter character;
    Transform camTrans;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<TPCharacter>();
        camTrans = Camera.main.transform;
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(camTrans.forward, Vector3.up);
        Vector3 camRight = Vector3.ProjectOnPlane(camTrans.right, Vector3.up);

        Vector3 move = camForward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal");
        move.Normalize();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move *= 0.5f;
        }

        if (Input.GetMouseButtonDown(2))
        {
            if (animator.GetBool("IsFighting")) animator.SetTrigger("PutWeapon");
            else animator.SetTrigger("GetWeapon");

        }

        character.Move(move, Input.GetButtonDown("Jump"));
    }

    void GetWeapon()
    {
        animator.SetBool("IsFighting", true);
        weapon.SetParent(weaponSlotOnHand);
        weapon.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    void PutWeapon()
    {
        animator.SetBool("IsFighting", false);
        weapon.SetParent(weaponSlotOnBack);
        weapon.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
}
