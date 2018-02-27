using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;

public class UIManager : MonoBehaviour {
	public GameObject apV_1, apV_2, pV1,pV2;
	public bool activated = false;
	private string videoUrl = "http://clips.vorwaerts-gmbh.de/Vfe_html5.mp4";
	public bool video1 = false;
	public VideoPlayer videoPlayer;
	//private string localPath = Application.persistentDataPath + "/videos";
	// Use this for initialization
	void Start () {
		WWW www = new WWW (videoUrl);
		if (!Directory.Exists (Application.persistentDataPath + "movies")) {
			StartCoroutine (downloadVideo1(www, Application.persistentDataPath + "movies"));
			//falta completar la funcion para desargar y crear carpetas con videos
		}
	}

	private IEnumerator downloadVideo1(WWW www, string localpath){
		while(!www.isDone){
			yield return null;
		}
		if (!string.IsNullOrEmpty (www.error)) {
			Debug.Log ("Error" + www.error);
		} else {
			yield return www;
			File.WriteAllBytes (localpath,www.bytes);
		}
	}
	// Update is called once per frame
	void Update () {
		if(video1==true){
			//videoPlayer.url = localPath;
		}
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
