using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuRoomDelayedController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int waitingRoomSceneIndex;
    
    public override void OnEnable() {
        // register to photon callback functions
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        // unregister to photon callback functions
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() {
        // load into a waiting room scene
        SceneManager.LoadScene(waitingRoomSceneIndex);
    }
}
