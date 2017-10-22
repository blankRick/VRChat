using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.VR;

public class move : NetworkBehaviour {

    private GameObject infield;
    [SyncVar (hook = "UpdateTextMesh")]
    public string chatHistory = string.Empty;
    private TextMesh tm;
    private eventCamera cvas;

    [Command]
    public void CmdSend(string newText)
    {
        if (!isServer)
        {
            return;
        }
        //possibility of stealing entire history
        chatHistory.Trim(new char[] { '\n' });
        Queue<string> prevChats = new Queue<string>(chatHistory.Split('\n'));
        DateTime curr = DateTime.Now;
        if (prevChats.Count > 5) prevChats.Dequeue();
        prevChats.Enqueue(curr.ToString("HH:mm") + newText);
        Debug.Log("local-"+ newText);
        Debug.Log("local-"+prevChats.Count);
        chatHistory = String.Join("\n", prevChats.ToArray());
        Debug.Log("local-"+transform.position + "->" + chatHistory);
    }
    void Awake()
    {
        if (!isLocalPlayer)
        {
            GetComponent<GvrAudioListener>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
            GetComponent<Camera>().enabled = false;
        }
        else
        {
            GetComponent<AudioListener>().enabled = true;
            GetComponent<GvrAudioListener>().enabled = true;
            GetComponent<Camera>().enabled = true;
        }
    }
    void Start()
    {
        if (!isLocalPlayer)
        {
            GetComponent<GvrAudioListener>().enabled = false;
            GetComponent<AudioListener>().enabled = false;
            GetComponent<Camera>().enabled = false;
            Transform leftEye = this.transform.Find("Player(Clone) Left");
            Transform rightEye = this.transform.Find("Player(Clone) Right");
            if (leftEye != null) leftEye.gameObject.SetActive(false);
            if (rightEye != null) rightEye.gameObject.SetActive(false);
        }
        else
        {
            GetComponent<AudioListener>().enabled = true;
            GetComponent<GvrAudioListener>().enabled = true;
            GetComponent<Camera>().enabled = true;
            this.transform.Find("GvrViewerMain").gameObject.SetActive(true);
            this.transform.Find("GvrReticlePointer").gameObject.SetActive(true);
            infield = GameObject.FindGameObjectWithTag("messageField");
            infield.GetComponent<sendText>().localg = this.gameObject;
            cvas = GameObject.FindGameObjectWithTag("canvas").GetComponent<eventCamera>();
            cvas.transform.position = this.transform.position + new Vector3(0, 0, 1.24f);
            cvas.setEventCamera(GetComponent<Camera>(), this.gameObject.transform.position);
            
        }
        tm = GetComponentInChildren<TextMesh>();
        UnityEngine.XR.XRSettings.enabled = true;
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        if(mainCam != null) mainCam.SetActive(false);

    }
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        if(isLocalPlayer || isServer)
        {
            Debug.Log("destoryed");
            GameObject.FindGameObjectWithTag("MainCamera").SetActive(true);
        }
    }
    void UpdateTextMesh(string history)
    {
        Debug.Log("called update");
        tm.text = history;
        if (isLocalPlayer)
        {
            Debug.Log("local" + history);
        }
        else
        {
            Debug.Log("un local" + history);
        }
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        
        moveForward();
    }

    public void moveForward()
    {
        cvas.changePos(this.transform.position, this.transform.forward, this.transform.rotation);
    }
}
