using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class movePlayer : MonoBehaviour {

    // Use this for initialization
    public GameObject localg;
    public void send()
    {
        if (!localg.GetComponent<NetworkIdentity>().isLocalPlayer)
            Debug.Log("notLocalPlayer");
        localg.GetComponent<move>().moveForward();
    }
}
