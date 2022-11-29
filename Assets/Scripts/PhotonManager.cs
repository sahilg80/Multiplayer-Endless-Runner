using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("Input Field Texts")]
    [SerializeField]
    InputField userNameText;
    [SerializeField]
    InputField _roomNameText;
    [SerializeField]
    InputField _maxPlayersText;
    [SerializeField]
    InputField _joinRoomNameText;

    [Header("Notification Texts")]
    [SerializeField]
    GameObject _incorrectInputTxt;
    [SerializeField]
    GameObject _notJoinRoomTxt;
    [SerializeField]
    GameObject _invalidLoginInputTxt;
    [SerializeField]
    GameObject _roomJoinFailedTxt;

    [Header("Panels")]
    [SerializeField]
    GameObject _loginPanel;

    [SerializeField]
    GameObject _lobbyPanel;
    [SerializeField]
    GameObject _conectingPanel;
    [SerializeField]
    GameObject _createRoomPanel;
    [SerializeField]
    GameObject _roomListPanel;
    [SerializeField]
    GameObject _insideRoomPanel;
    [SerializeField]
    GameObject _joinRoomPanel;

    [Header("Room Items")]
    [SerializeField]
    GameObject _roomItemPrefab;
    [SerializeField]
    GameObject _roomItemsParent;
    Dictionary<string, RoomInfo> roomListData;
    List<GameObject> _roomItemsList;

    [Header("Player Items")]
    [SerializeField]
    GameObject _networkPlayerItemPrefab;
    [SerializeField]
    GameObject _networkPlayerItemsParent;
    [SerializeField]
    GameObject _playButton;
    Dictionary<int, GameObject> networkPlayerListData;

    // Start is called before the first frame update
    void Start()
    {
        ActivePanel(_loginPanel.name);
        PhotonNetwork.AutomaticallySyncScene = true;
        roomListData = new Dictionary<string, RoomInfo>();
        networkPlayerListData = new Dictionary<int, GameObject>();
        _roomItemsList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickPlayButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void OnClickJoinRoomButton()
    {
        if (!string.IsNullOrWhiteSpace(_joinRoomNameText.text))
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }
            PhotonNetwork.JoinRoom(_joinRoomNameText.text);
        }
        else
        {
            _notJoinRoomTxt.GetComponent<TextMeshProUGUI>().text = "Not Valid Input";
            _notJoinRoomTxt.SetActive(true);
        }

    }

    public void OnClickBackFromRoomLIst()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivePanel(_lobbyPanel.name);
    }

    public void OnClickRoomsList()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivePanel(_roomListPanel.name);
    }

    public void OnClickCreateRoom()
    {
        if (int.TryParse(_maxPlayersText.text, out int value) && !string.IsNullOrWhiteSpace(_roomNameText.text))
        {
            _incorrectInputTxt.SetActive(false);
            ActivePanel(_conectingPanel.name);
            string name = _roomNameText.text;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = (byte)int.Parse(_maxPlayersText.text);
            PhotonNetwork.CreateRoom(name, roomOptions);
        }
        else
        {
            _incorrectInputTxt.SetActive(true);
        }
    }

    public void OnLoginClick()
    {
        string name = userNameText.text;
        if (!string.IsNullOrWhiteSpace(name))
        {
            _invalidLoginInputTxt.SetActive(false);
            ActivePanel(_conectingPanel.name);
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            _invalidLoginInputTxt.SetActive(true);
        }
    }

    public void OnClickBackFromPlayerList()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        ActivePanel(_lobbyPanel.name);
    }

    public void ActivePanel(string name)
    {
        _loginPanel.SetActive(_loginPanel.name.Equals(name));
        _lobbyPanel.SetActive(_lobbyPanel.name.Equals(name));
        _conectingPanel.SetActive(_conectingPanel.name.Equals(name));
        _createRoomPanel.SetActive(_createRoomPanel.name.Equals(name));
        _roomListPanel.SetActive(_roomListPanel.name.Equals(name));
        _insideRoomPanel.SetActive(_insideRoomPanel.name.Equals(name));
        _joinRoomPanel.SetActive(_joinRoomPanel.name.Equals(name));
    }

    public override void OnCreatedRoom()
    {
        print(PhotonNetwork.CurrentRoom.Name + " room is created");
    }

    public override void OnJoinedRoom()
    {
        print(PhotonNetwork.LocalPlayer.NickName + " is joined room");
        ActivePanel(_insideRoomPanel.name);
        if (PhotonNetwork.IsMasterClient)
        {
            _playButton.SetActive(true);
        }
        else
        {
            _playButton.SetActive(false);
        }
        foreach (Player item in PhotonNetwork.PlayerList)
        {
            GameObject obj = Instantiate(_networkPlayerItemPrefab);

            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = item.NickName;
            if (item.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                obj.transform.GetChild(1).gameObject.SetActive(true);
            }
            obj.transform.SetParent(_networkPlayerItemsParent.transform);
            obj.transform.localScale = Vector3.one;
            networkPlayerListData.Add(item.ActorNumber, obj);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        GameObject obj = Instantiate(_networkPlayerItemPrefab);

        obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = newPlayer.NickName;
        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            obj.transform.GetChild(1).gameObject.SetActive(true);
        }
        obj.transform.SetParent(_networkPlayerItemsParent.transform);
        obj.transform.localScale = Vector3.one;
        networkPlayerListData.Add(newPlayer.ActorNumber, obj);
    }

    public override void OnPlayerLeftRoom(Player leftPlayer)
    {
        Destroy(networkPlayerListData[leftPlayer.ActorNumber]);
        networkPlayerListData.Remove(leftPlayer.ActorNumber);
        if (PhotonNetwork.IsMasterClient)
        {
            _playButton.SetActive(true);
        }
        else
        {
            _playButton.SetActive(false);
        }
    }

    public override void OnLeftRoom()
    {
        ActivePanel(_lobbyPanel.name);
        foreach (GameObject item in networkPlayerListData.Values)
        {
            Destroy(item);
        }
        networkPlayerListData.Clear();
    }

    public override void OnConnected()
    {
        print("connected to internet");
    }

    public override void OnConnectedToMaster()
    {
        ActivePanel(_lobbyPanel.name);
    }

    public override void OnLeftLobby()
    {
        ClearRoomList();
        roomListData.Clear();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _roomJoinFailedTxt.GetComponent<TextMeshProUGUI>().text = message;
        _roomJoinFailedTxt.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomList();
        foreach (var room in roomList)
        {
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                if (roomListData.ContainsKey(room.Name))
                {
                    roomListData.Remove(room.Name);
                }
            }
            else
            {
                if (roomListData.ContainsKey(room.Name))
                {
                    roomListData[room.Name] = room;
                }
                else
                {
                    roomListData.Add(room.Name, room);
                }
            }
        }
        GenerateRoomItem();
    }

    void GenerateRoomItem()
    {
        foreach (RoomInfo room in roomListData.Values)
        {
            GameObject obj = Instantiate(_roomItemPrefab);
            obj.transform.SetParent(_roomItemsParent.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = room.Name;
            obj.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = room.PlayerCount + "/" + room.MaxPlayers;
            obj.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => JoinRoomFromList(room));
            _roomItemsList.Add(obj);
        }
    }

    void JoinRoomFromList(RoomInfo room)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(room.Name);
       
    }

    void ClearRoomList()
    {
        for (int i = 0; i < _roomItemsList.Count; i++)
        {
            Destroy(_roomItemsList[i]);
        }
        _roomItemsList.Clear();
    }

}
