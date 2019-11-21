using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject bubblePrefab;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        photonView = GetComponent<PhotonView>();
        if(photonView.IsMine) 
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
            GetComponent<Rigidbody2D>().AddForce(transform.up * 30, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * -1 * 30, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * -1 * 30, ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * 30, ForceMode2D.Impulse);
            }
        }
    }
}
