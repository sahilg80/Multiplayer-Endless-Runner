using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController
{
    PlayerView _playerView;
    PlayerModel _playerModel;

    //GameObject _playerObject;

    public PlayerController(PlayerModel model, PlayerView view)
    {
        _playerModel = model;
        _playerView = view;
        _playerModel.SetController(this);
        _playerView.SetController(this);

    }

    public void SpawnPlayerObject(GameObject playerPrefab, GameObject mapTile)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _playerView.SetMapTiles();
        }
        float xPos = UnityEngine.Random.Range(mapTile.transform.position.x - mapTile.transform.localScale.x / 2 + 1f, mapTile.transform.position.x + mapTile.transform.localScale.x / 2 - 1f);

        Vector3 spawnPos= new Vector3(xPos, playerPrefab.transform.position.y, 0);
        
        while (Physics.CheckSphere(spawnPos, 0.25f, LayerMask.GetMask("Player")))
        {
            Debug.Log("hit wall");
            xPos = UnityEngine.Random.Range(mapTile.transform.position.x - mapTile.transform.localScale.x / 2 + 1f, mapTile.transform.position.x + mapTile.transform.localScale.x / 2 - 1f);
            spawnPos= new Vector3(xPos, playerPrefab.transform.position.y, 0);
        }
        
        _playerModel.playerObject = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(xPos, playerPrefab.transform.position.y, 0), Quaternion.identity);
        _playerView.SetCameraFollow(_playerModel.playerObject);
    }

    

    public void ControlPlayerMovement()
    {
        _playerView.ControlPlayerMovement(_playerModel.playerObject);
    }

    public void ChangePlayerDirection(int dir)
    {
        _playerView.ChangePlayerDirection(_playerModel.playerObject, dir);
    }

    public void JumpPlayer()
    {
        _playerView.JumpPlayer(_playerModel.playerObject);
    }

}

