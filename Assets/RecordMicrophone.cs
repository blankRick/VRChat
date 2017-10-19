using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RecordMicrophone : NetworkBehaviour {

    // Use this for initialization
    AudioSource aud;
	void Start () {
        if (!isLocalPlayer) return;
        aud = this.GetComponent<AudioSource>();
        if(Microphone.devices == null)
        {
            GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<Text>().text = "Not there";
            return;
        }
        else
        {
            GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<Text>().text = "there";
        }
        aud.clip = Microphone.Start(Microphone.devices[0], true, 999, 44100);
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0))
        { }
        aud.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
