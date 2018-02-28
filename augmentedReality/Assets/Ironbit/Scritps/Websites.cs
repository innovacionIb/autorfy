using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Websites : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void goToPage(string name){
		switch(name){

		case "ap_web1":
			Application.OpenURL ("http://www.aguapiedramezcal.com/");
			break;

		case "ap_web2":
			Application.OpenURL ("https://www.facebook.com/AguaPiedra/");
			break;

		case "pin_web1":
			Application.OpenURL ("http://pinata2go.mx/");
			break;

		case "pin_web2":
			Application.OpenURL ("http://pinata2go.mx/contacto/");
			break;
		}
	}
}
