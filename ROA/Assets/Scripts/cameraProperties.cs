using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraProperties : MonoBehaviour {
	public GameObject canvasHorizontal, canvasVertical;
	// Use this for initialization
	void Start () {
		canvasHorizontal.SetActive (false);
		Screen.SetResolution (1080,1920,true);
	}
	
	// Update is called once per frame
	void Update () {
		if(Screen.orientation == ScreenOrientation.LandscapeLeft){
			canvasHorizontal.SetActive (true);
			canvasVertical.SetActive (false);
		}
		if(Screen.orientation == ScreenOrientation.LandscapeRight){
			canvasHorizontal.SetActive (true);
			canvasVertical.SetActive (false);
		}
		if(Screen.orientation == ScreenOrientation.Portrait){
			canvasHorizontal.SetActive (false);
			canvasVertical.SetActive (true);
		}
		if(Screen.orientation == ScreenOrientation.PortraitUpsideDown){
			canvasHorizontal.SetActive (false);
			canvasVertical.SetActive (true);
		}
	}
}
