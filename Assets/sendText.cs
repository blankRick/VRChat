using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class sendText : MonoBehaviour {
    public GameObject localg;
    public void send()
    {
        if (!localg.GetComponent<NetworkIdentity>().isLocalPlayer)
            Debug.Log("notLocalPlayer");
        Debug.Log("called");
        localg.GetComponent<move>().CmdSend(GetComponent<InputField>().text);
    }
}
