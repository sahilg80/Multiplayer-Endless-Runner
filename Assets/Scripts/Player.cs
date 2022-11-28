using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace endlessrunner
{
public class Player : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float xAxisMoveVal;
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float _gravityValue;

    public bool isMoving{get; set;}
    CharacterController controller;
    
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (!controller.isGrounded)
            {
                direction.y += _gravityValue*Time.deltaTime;
            }
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    public void ChangeDirection(int dir){
        direction = direction + transform.right*dir*xAxisMoveVal;
    }

    public void Jump(){
        if (controller.isGrounded)
        {
            direction.y = jumpForce;
        }
    }

}
}