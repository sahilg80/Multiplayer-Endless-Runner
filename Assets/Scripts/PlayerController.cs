using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    PlayerView _playerView;
    PlayerModel _playerModel;

    //GameObject _playerObject;

    public PlayerController(PlayerModel model, PlayerView view){
        _playerModel = model;
        _playerView = view;
        _playerModel.SetController(this);
        _playerView.SetController(this);
        
    }

    public void SpawnPlayerObject(GameObject playerPrefab){
        _playerView.SetMapTiles();
        _playerModel.playerObject = GameObject.Instantiate(playerPrefab);
        _playerView.SetCameraFollow(_playerModel.playerObject);
    }

    public void ControlPlayerMovement(){
        _playerView.ControlPlayerMovement(_playerModel.playerObject);   
    }

    public void ChangePlayerDirection(int dir){
        _playerView.ChangePlayerDirection( _playerModel.playerObject, dir);
    }

    public void JumpPlayer(){
        _playerView.JumpPlayer( _playerModel.playerObject);
    }

}

