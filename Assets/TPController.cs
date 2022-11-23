using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TPCharacter))]
public class TPController : MonoBehaviour
{
    TPCharacter character;
    Transform camTrans;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<TPCharacter>();
        camTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(camTrans.forward, Vector3.up);
        Vector3 camRight = Vector3.ProjectOnPlane(camTrans.right, Vector3.up);

        Vector3 move = camForward * Input.GetAxis("Vertical") + camRight * Input.GetAxis("Horizontal");
        move.Normalize();
        if (Input.GetKey(KeyCode.LeftShift)) {
            move *= 0.5f;
        }

        character.Move(move, Input.GetButtonDown("Jump"));
    }
}
