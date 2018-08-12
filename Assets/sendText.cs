using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class sendText : MonoBehaviour {
    public GameObject localg;
    private bool vTog;
    public void Start()
    {
        vTog = true;
    }
    public void voiceToggle()
    {
        vTog = !vTog;
    }
    public void send()
    {
        if (!localg.GetComponent<NetworkIdentity>().isLocalPlayer)
            Debug.Log("notLocalPlayer");
        Debug.Log("called");
        if (!vTog || !localg.GetComponent<RecordMicrophone>().enabled)
        {
            localg.GetComponent<move>().CmdSend(GetComponent<InputField>().text);
        }
        else
        {
            float[] samples = new float[999];
            localg.GetComponent<RecordMicrophone>().rec.clip.GetData(samples, localg);
            
            //localg.GetComponent<RecordMicrophone>().samplesOffset += 64;
            localg.GetComponent<RecordMicrophone>().CmdSendVoice(samples, localg.GetComponent<RecordMicrophone>().rec.clip.channels);
        }

    }
}
