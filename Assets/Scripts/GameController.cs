using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerView _playerView;
    [SerializeField]
    GameObject playerPrefab;

    PlayerController _playerController;
    public PlayerController playerControl{
        get{return _playerController;}
    }

    static GameController _gameController;
    public static GameController Instance{
        get{ return _gameController;}
    }

    void Awake(){
        if (_gameController == null)
        {
            _gameController = this;
        }
        else{
            Destroy(this);
        }
        PlayerModel model = new PlayerModel();
        _playerController = new PlayerController(model, _playerView);
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerController.SpawnPlayerObject(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
