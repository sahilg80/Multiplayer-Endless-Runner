using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    PlayerController _playerController;
    public GameObject playerObject;

    public PlayerModel(){

    }

    public void SetController(PlayerController controller){
        _playerController = controller;
    }
}
