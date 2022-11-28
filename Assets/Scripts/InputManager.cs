using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameController.Instance.playerControl.ControlPlayerMovement();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameController.Instance.playerControl.ChangePlayerDirection(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameController.Instance.playerControl.ChangePlayerDirection(-1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            GameController.Instance.playerControl.JumpPlayer();
        }
    }
}
