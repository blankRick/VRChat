using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eventCamera : MonoBehaviour {

    // Use this for initialization
    private double displacement;
	void Start () {
	}
	
	// Update is called once per frame
	public void setEventCamera(Camera cam, Vector3 t)
    {
        GetComponent<Canvas>().worldCamera = cam;
        displacement = Vector3.Distance(t, this.transform.position);
    }

    public void changePos(Vector3 pos, Vector3 forward, Quaternion rotation)
    {
        this.transform.position = pos + new Vector3(forward.x, 0, forward.z)*(float)(displacement);
        this.transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
    }
}
