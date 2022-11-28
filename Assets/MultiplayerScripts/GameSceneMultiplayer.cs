using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using endlessrunner;

public class GameSceneMultiplayer : MonoBehaviour
{
    [SerializeField]
    GameObject _playerPrefab;
    [SerializeField]
    GameObject _platform;
    [SerializeField]
    CameraFollower mainCamera;
    GameObject myPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            float xPos = UnityEngine.Random.Range(_platform.transform.position.x - _platform.transform.localScale.x/2, _platform.transform.position.x + _platform.transform.localScale.x/2);
            myPlayer = PhotonNetwork.Instantiate(_playerPrefab.name,new Vector3(xPos,_playerPrefab.transform.position.y,0),Quaternion.identity);
            mainCamera.SetOffsetValue(myPlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myPlayer.GetComponent<Player>().isMoving = ! myPlayer.GetComponent<Player>().isMoving;
        }
    }
}
