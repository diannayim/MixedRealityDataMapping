using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationModel : MonoBehaviour {
    public Camera cam;
    public float distance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = cam.transform.position + cam.transform.forward * distance;
        this.transform.rotation = new Quaternion(0.0f, cam.transform.rotation.y, 0.0f, cam.transform.rotation.w);
	}
}
