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

        public bool isMoving { get; set; }
        CharacterController controller;

        Vector3 direction;
        float maxRightVal;
        float maxLeftVal;
        // Start is called before the first frame update
        void Start()
        {
            controller = GetComponent<CharacterController>();
            direction = transform.forward;
            maxRightVal = 0.5f;
            maxLeftVal = -0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMoving)
            {
                if (!controller.isGrounded)
                {
                    direction.y += _gravityValue * Time.deltaTime;
                }

                controller.Move(direction * speed * Time.deltaTime);
            }
        }

        public void ChangeDirection(int dir)
        {
            if (direction.x < maxLeftVal)
            {
                direction.x = maxLeftVal;
            }
            else if (direction.x > maxRightVal)
            {
                direction.x = maxRightVal;
            }
            direction = direction + transform.right * dir * xAxisMoveVal;
        }

        public void Jump()
        {
            if (controller.isGrounded)
            {
                direction.y = jumpForce;
            }
        }

    }
}