using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using endlessrunner;
using Photon.Pun;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    CameraFollower mainCamera;
    [SerializeField]
    MapManager _map;
    PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetController(PlayerController controller){
        _playerController = controller;
    }

    public void SetMapTiles(){
        _map.SpawnMap();
    }

    public void SetCameraFollow(GameObject playerObject){
        mainCamera.SetOffsetValue(playerObject);
    }

    public void ControlPlayerMovement(GameObject playerObject){
        playerObject.GetComponent<Player>().isMoving = !playerObject.GetComponent<Player>().isMoving;
    }

    public void ChangePlayerDirection(GameObject playerObject,int dir){
        playerObject.GetComponent<Player>().ChangeDirection(dir);
    }

    public void JumpPlayer(GameObject playerObject){
        playerObject.GetComponent<Player>().Jump();
    }
}
