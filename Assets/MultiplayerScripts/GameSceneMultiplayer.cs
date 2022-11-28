using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameSceneMultiplayer : MonoBehaviour
{
    [SerializeField]
    GameObject _playerPrefab;
    [SerializeField]
    GameObject _platform;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            float xPos = UnityEngine.Random.Range(_platform.transform.position.x - _platform.transform.localScale.x/2, _platform.transform.position.x + _platform.transform.localScale.x/2);
            PhotonNetwork.Instantiate(_playerPrefab.name,new Vector3(xPos,0,0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
