using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorKeyBoard : MonoBehaviour {
    private Vector3 globalPos;
    private Vector3 globalForward;
    private bool assigned;
    private void Start()
    {
        assigned = false;
    }
    
    private void FixedUpdate()
    {
        if(assigned)
        {
            this.GetComponent<RectTransform>().position = globalPos;
            //this.GetComponent<RectTransform>().forward = globalForward;
        }
    }
    public void anchor()
    {
        globalPos = this.GetComponent<RectTransform>().position;
        globalForward= this.GetComponent<RectTransform>().forward;
        assigned = true;
    }
    public void unfix()
    {
        assigned = false;
    }
}
