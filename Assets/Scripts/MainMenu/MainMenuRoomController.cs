using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int versusSceneIndex; // Number for the build index to the versus scene.
    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this); 
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom() // Callback function for when we successfully create or join a room.
    {
        Debug.Log("Joined Room");
        StartGame();
    }
    private void StartGame() // Function for loading into the multiplayer scene.
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(versusSceneIndex); // Because of AutoSyncScene all players who join the room will also be loaded into the versus scene.
        }
    }
}
