using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MapManager : MonoBehaviourPunCallbacks,IInRoomCallbacks
{
    [SerializeField]
    GameObject _mapPrefab;
    [SerializeField]
    GameObject _obstaclePrefab;
    float offsetDist;
    float _platformWidth;
    float _platformLength;
    float _obstacleHeight;
    // Start is called before the first frame update
    void Awake()
    {
        //offsetDist = _mapPrefab.transform.localScale.z;
        _platformWidth = _mapPrefab.transform.localScale.x;
        _platformLength = _mapPrefab.transform.localScale.z;
        _obstacleHeight = _obstaclePrefab.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMap(){
        PhotonNetwork.InstantiateRoomObject(_mapPrefab.name, transform.forward*offsetDist, Quaternion.identity);
        offsetDist = offsetDist + _platformLength;
        InvokeRepeating("SpawnInfiniteMapTiles",0.5f,2.5f);
    }

    void SpawnInfiniteMapTiles()
    {
        GameObject spawnedPlatform = PhotonNetwork.InstantiateRoomObject(_mapPrefab.name, transform.forward * offsetDist, Quaternion.identity);
        SpawnObstacles(spawnedPlatform);
        offsetDist = offsetDist + _platformLength;
        SendData();
    }

    void SpawnObstacles(GameObject spawnedPlatform)
    {
        float xPos = UnityEngine.Random.Range(spawnedPlatform.transform.position.x - _platformWidth / 2 + 0.7f, spawnedPlatform.transform.position.x + _platformWidth / 2 - 0.7f);
        float zPos = UnityEngine.Random.Range(spawnedPlatform.transform.position.z - _platformLength / 2 + 0.7f, spawnedPlatform.transform.position.z + _platformLength / 2 - 0.7f);
        PhotonNetwork.InstantiateRoomObject(_obstaclePrefab.name, new Vector3(xPos, _obstacleHeight / 2, zPos), Quaternion.identity);
    }


    void SendData()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("UpdateOffsetValue", RpcTarget.All, offsetDist);
    }

    [PunRPC]
    public void UpdateOffsetValue(float pos)
    {
        offsetDist = pos;
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            InvokeRepeating("SpawnInfiniteMapTiles",0.5f,2.5f);
        }
    }

}
