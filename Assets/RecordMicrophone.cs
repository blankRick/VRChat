using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RecordMicrophone : NetworkBehaviour {

    // Use this for initialization
    AudioSource aud;
    AudioListener listener;
    GvrAudioListener gvrListener;
    void Start () {
        if (!isLocalPlayer) return;
        aud = this.GetComponent<AudioSource>();
        if(Microphone.devices == null)
        {
            GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "Not There";
            return;
        }
        else
        {
            Debug.Log(Microphone.devices.Length);
            GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "" + Microphone.devices[0];
        }
        aud.clip = Microphone.Start(Microphone.devices[0], true, 999, 44100);
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0))
        { }
        aud.Play();
        listener = GetComponent<AudioListener>();
        listener.enabled = true;
        gvrListener = GetComponent<GvrAudioListener>();
        gvrListener.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
