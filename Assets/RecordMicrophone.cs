using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public struct VoicePacket {
    public float[] voiceSamples;
    public int channels;
    public VoicePacket(float[] newVoice, int ch) { voiceSamples = newVoice; channels = ch; } 
}

public class RecordMicrophone : NetworkBehaviour {

    // Use this for initialization
    public AudioSource rec = null;
    int minf, maxf;
    public AudioSource play = null;
    AudioListener listener;
    GvrAudioListener gvrListener;
    [SyncVar (hook = "UpdateAudioSource")]
    private VoicePacket voiceSamples;
    public int samplesOffset;
    void Start () {
        samplesOffset = 64;
        rec = null;
        GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "Not Changed";
        if (!isLocalPlayer) return;
        GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "Changed";
        rec = this.GetComponents<AudioSource>()[0];
        if(Microphone.devices == null)
        {
            GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "Not There";
            return;
        }
        else
        {
            if (Microphone.devices.Length == 0)
            {
                GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "Not There 0";
            }
            else
            {
                GameObject.FindGameObjectWithTag("sendButton").GetComponent<Text>().text = "" + Microphone.devices[0];
            }
        }
        
        Microphone.GetDeviceCaps(Microphone.devices[0], out minf, out maxf);
        rec.clip = Microphone.Start(Microphone.devices[0], true, 999, maxf);
        while (!(Microphone.GetPosition(Microphone.devices[0]) > 0))
        { }
        rec.Play();
        listener = GetComponent<AudioListener>();
        listener.enabled = true;
        gvrListener = GetComponent<GvrAudioListener>();
        gvrListener.enabled = true;
    }
    [Command]
    public void CmdSendVoice(float[] newVoice, int channels)
    {
        if (!isServer)
        {
            return;
        }
        //possibility of stealing entire history
        
        voiceSamples = new VoicePacket(newVoice, channels);
    }
    void UpdateAudioSource(VoicePacket newVoice)
    {
        //return;
        if (play == null)
        {
            play = this.GetComponents<AudioSource>()[1];
            play.clip = AudioClip.Create("Playing", 999, newVoice.channels, maxf, false);
        }

        play.Stop();
        play.clip.SetData(newVoice.voiceSamples, 0);
        foreach(float f in newVoice.voiceSamples)
        {
            Debug.Log(f);
        }
        play.Play();
    }
}
