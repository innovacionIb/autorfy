using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class cameraSettings : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.SetResolution (1080,1920,true);
		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
