using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
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
    void Start()
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
        InvokeRepeating("SpawnInfiniteMapTiles",0f,2.5f);
    }
    
    void SpawnInfiniteMapTiles(){
        GameObject spawnedPlatform = Instantiate(_mapPrefab, transform.forward*offsetDist, Quaternion.identity);
        SpawnObstacles(spawnedPlatform);
        offsetDist = offsetDist + _platformLength;
    }

    void SpawnObstacles(GameObject spawnedPlatform){
        // int obstaclesCount = UnityEngine.Random.Range(0,5);
        // for (int i = 0; i < obstaclesCount; i++)
        // {
            float xPos = UnityEngine.Random.Range(spawnedPlatform.transform.position.x - _platformWidth/2, spawnedPlatform.transform.position.x + _platformWidth/2);
            float zPos = UnityEngine.Random.Range(spawnedPlatform.transform.position.z - _platformLength/2, spawnedPlatform.transform.position.z + _platformLength/2);
            Instantiate(_obstaclePrefab, new Vector3(xPos,_obstacleHeight/2,zPos), Quaternion.identity);
        //}
    }

}
