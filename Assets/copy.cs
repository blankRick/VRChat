using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class copy : MonoBehaviour {

    public InputField reqdtext;
    public Text inputtext;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        reqdtext.text = inputtext.text;
    }

}
