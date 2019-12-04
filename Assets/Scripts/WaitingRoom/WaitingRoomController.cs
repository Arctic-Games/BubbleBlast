using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomController : MonoBehaviourPunCallbacks
{

    private PhotonView myPhotonView;

    [SerializeField]
    private int multiplayerSceneIndex;
    [SerializeField]
    private int menuSceneIndex;
    private int playerCount;
    private int roomSize;

    [SerializeField]
    private Text messageToPlayer;
    [SerializeField]
    private Text totalPlayersOnline;
    [SerializeField]
    private Text timerToStartDisplay;

    private bool readyToStart;
    private bool startingGame;

    private float timerToStartGame;
    private float fullGameTimer;

    [SerializeField]
    private float maxWaitTime;
    [SerializeField]
    private float maxFullGameTime;

    private void Start() {
        myPhotonView = GetComponent<PhotonView>();
        fullGameTimer = maxFullGameTime;
        timerToStartGame = maxWaitTime;

        PlayerCountUpdate();
    }
    
    void PlayerCountUpdate() {
        // updates player count when players join the room
        // displays player count
        // triggers countdown timer
        playerCount = PhotonNetwork.PlayerList.Length;
        roomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        if (playerCount == roomSize) {
            messageToPlayer.text = "Two players found! Ready to start!";
            readyToStart = true;
        }
        else {
            messageToPlayer.text = "Waiting for player...";
            readyToStart = false;
        } 
    }

    // Called when a new player joins a waiting room
    public override void OnPlayerEnteredRoom(Player newPlayer) {
        PlayerCountUpdate();
        // Send master clients countdown timer to other player to sync time together 
        if(PhotonNetwork.IsMasterClient) {
            myPhotonView.RPC("RPC_SendTimer", RpcTarget.Others, timerToStartGame);
        }
    }

    [PunRPC]
    private void RPC_SendTimer(float masterClientsTime) {
        // RPC to sync the countdown timer to the other player
        timerToStartGame = masterClientsTime;
        if(masterClientsTime < fullGameTimer) {
            fullGameTimer = masterClientsTime;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) {
        PlayerCountUpdate();
    }

    private void Update() {
        WaitingForPlayer();
    }

    void WaitingForPlayer() {
        // Stop and reset the time when a player leaves before the countdown is complete
        if(playerCount < 2) {
            ResetTimer();
        }
        // If there are two players then the start timer will begin counting down
        if(readyToStart) {
            fullGameTimer -= Time.deltaTime;
            timerToStartGame = fullGameTimer;
        }

        string tempTimer = string.Format("{0:00}", timerToStartGame);
        timerToStartDisplay.text = tempTimer;
        int totalPlayers = PhotonNetwork.CountOfPlayersInRooms + PhotonNetwork.CountOfPlayersOnMaster;
        totalPlayersOnline.text = "Players Online: " + totalPlayers;    // TODO Doesn't update while in lobby. Fix later
        
        // Start the game when the countdown reaches 0
        if(timerToStartGame <= 0f) {
            if(startingGame) {
                return;
            }
            StartGame();
        }
    }

    void ResetTimer() {
        timerToStartGame = maxWaitTime;
        fullGameTimer = maxFullGameTime;
    }

    public void StartGame() {
        startingGame = true;
        if(PhotonNetwork.IsMasterClient) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
        else {
            return;
        }
    }

    public void Cancel() {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(menuSceneIndex);
    }

}
