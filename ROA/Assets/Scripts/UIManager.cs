using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;

public class UIManager : MonoBehaviour {
	public GameObject apV_1, apV_2, pV1,pV2;
	public bool activated = false;
	public bool video1 = false;

	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {

	}

	public void goToPage(string name){
		switch(name){

		case "ap_web":
			Application.OpenURL ("http://www.aguapiedramezcal.com/");
			break;

		case "ap_face":
			Application.OpenURL ("https://www.facebook.com/AguaPiedra/");
			break;

		case "pin_web":
			Application.OpenURL ("http://pinata2go.mx/");
			break;

		case "pin_contact":
			Application.OpenURL ("http://pinata2go.mx/contacto/");
			break;
		}
	}
}
