using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject mainMenuStartButton; // To create and join a game
    [SerializeField]
    private GameObject mainMenuCancelButton; // To stop searching for a game to join
    [SerializeField]
    private int playerLimit;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        mainMenuStartButton.SetActive(true);
    }

    public void MainMenuStart() 
    {
        mainMenuStartButton.SetActive(false);
        mainMenuCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Main Menu Start");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join an existing room, creating room...");
        CreateRoom();
    }

    void CreateRoom()
    {
        int randomRoomNumber = Random.Range(0, 10000);
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)playerLimit };
        PhotonNetwork.CreateRoom("Room" + randomRoomNumber, roomOps);
        Debug.Log(randomRoomNumber);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room, retrying...");
        CreateRoom();
    }

    public void MainMenuCancel()
    {
        mainMenuCancelButton.SetActive(false);
        mainMenuStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
