using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : MonoBehaviourPunCallbacks
{
    /***************
     * PUN 2 Plugin:  https://assetstore.unity.com/packages/tools/network/pun-2-free-119922
     * Documentation: https://doc.photonengine.com/en-us/pun/current/getting-started/pun-intro
     * Scripting API: https://doc-api.photonengine.com/en/pun/v2/index.html
     * 
     * If you are having trouble connecting two builds or the editor to a build then try 
     * changing the FixedRegion setting in the PhotonServerSettings during testing.
     * https://doc.photonengine.com/en-us/realtime/current/connection-and-authentication/regions
     ***************/

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connect to the Photon master server.
        // Alternatives: https://doc-api.photonengine.com/en/pun/v2/class_photon_1_1_pun_1_1_photon_network.html
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to the " + PhotonNetwork.CloudRegion + " server!");
    }
}
