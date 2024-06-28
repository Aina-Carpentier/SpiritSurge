using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;

    private static Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private void Awake()
    {
        Instance = this; 
    }
    void Start()
    {
        Debug.Log("Connection au serveur");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("Connecté au serveur");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Connecté au lobby");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }


    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
            return;

       
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");

    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
            Destroy(child.gameObject);


        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Une erreur s'est produite lors de la création : " + message;
        MenuManager.Instance.OpenMenu("error");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
        cachedRoomList.Clear();
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
        foreach (KeyValuePair<string, RoomInfo> entry in cachedRoomList)
        {
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(cachedRoomList[entry.Key]);
        }
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
