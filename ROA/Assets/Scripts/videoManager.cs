using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using System.IO;
using System;
using System.Linq;

namespace Vuforia{	
	public class videoManager : MonoBehaviour {
		public VideoPlayer v;
		public GameObject Loader;
		public TextMesh txtloader;
		// Use this for initialization
		void Start () {
			//v.enabled = false;
		}
		
		// Update is called once per frame
		void Update () {
		
		}
		public void getVideoName(string trackname){
			DownloadData (trackname);
			print (trackname);
		}
		public void DownloadData(string path){
			string localPath = Application.persistentDataPath + "/movie.mp4";
			if (!File.Exists (localPath)) {
				File.Delete (localPath);
			}
			Debug.Log ("Video: "+localPath);
			WWW www = new WWW (path);
			StartCoroutine (WaitForRequest(www,localPath));
		}

		public IEnumerator WaitForRequest(WWW www, string localpath){
			while(!www.isDone){
				Loader.SetActive (true);
				txtloader.text = Mathf.Round (www.progress * 100) + "%";
				Debug.Log (www.progress);
				yield return null;
			}
			if (!string.IsNullOrEmpty (www.error)) {
				txtloader.text = "Error: "+www.error;
				Debug.Log (">>>>>>>> >>>>>>>> >>>>>>>> Error: " + www.error);
			} else {
				txtloader.text = "0";
				Loader.SetActive (false);
				Debug.Log ("Video Downloaded!");
				yield return www;
				File.WriteAllBytes (localpath,www.bytes);
				v.url = localpath;
			}
		}
		/*

		public void getVideoName(string url){
			Debug.Log ("Video link:"+url);
			StartCoroutine (downloadAndPlayVideo(url,"video.mp4",true));
		}

		private IEnumerator downloadAndPlayVideo(string videoUrl, string saveFileName, bool overwriteVideo){
			//Where to Save the Video
			string saveDir = Path.Combine(Application.persistentDataPath, saveFileName);
			//Play back Directory
			string playbackDir = saveDir;
			#if UNITY_IPHONE
			playbackDir = "file://" + saveDir;
			#endif
			#if(UNITY_EDITOR)
			playbackDir = "file://" + saveDir;
			#endif

			bool downloadSuccess = false;
			byte[] vidData = null;

			//Check if the video file exist before downloading it again. 
     		//Requires(using System.Linq)
 
			string[] persistantData = Directory.GetFiles(Application.persistentDataPath);
			if (persistantData.Contains(playbackDir) && !overwriteVideo)
			{
				Debug.Log("Video already exist. Playing it now");
				//Play Video
				playVideo(playbackDir);
				//EXIT
				yield break;
			}
			else if (persistantData.Contains(playbackDir) && overwriteVideo)
			{
				Debug.Log("Video already exist [but] we are [Re-downloading] it");
				yield return downloadData(videoUrl, (status, dowloadData) =>
					{
						downloadSuccess = status;
						vidData = dowloadData;
					});
			}
			else
			{
				Debug.Log("Video Does not exist. Downloading video");
				yield return downloadData(videoUrl, (status, dowloadData) =>
					{
						downloadSuccess = status;
						vidData = dowloadData;
					});
			}

			//Save then Play if there was no download error
			if (downloadSuccess)
			{
				//Save Video
				saveVideoFile(saveDir, vidData);

				//Play Video
				playVideo(playbackDir);
			}
		}

		//Downloads the Video
		IEnumerator downloadData(string videoUrl, Action<bool, byte[]> result)
		{
			//Download Video
			UnityWebRequest webRequest = UnityWebRequest.Get(videoUrl);
			webRequest.Send();

			//Wait until download is done
			while (!webRequest.isDone)
			{
				Debug.Log("Downloading: " + webRequest.downloadProgress +"%");
				yield return null;
			}

			//Exit if we encountered error
			if (webRequest.isError)
			{
				Debug.Log("Error while downloading Video: " + webRequest.error);
				yield break; //EXIT
			}

			Debug.Log("Video Downloaded");
			//Retrieve downloaded Data
			result(!webRequest.isError, webRequest.downloadHandler.data);
		}

		//Saves the video
		bool saveVideoFile(string saveDir, byte[] vidData)
		{
			try
			{
				FileStream stream = new FileStream(saveDir, FileMode.Create);
				stream.Write(vidData, 0, vidData.Length);
				stream.Close();
				Debug.Log("Video Downloaded to: " + saveDir);

				v.url = Application.persistentDataPath +"/"+saveDir;
				return true;
			}
			catch (Exception e)
			{
				Debug.Log("Error while saving Video File: " + e.Message);
			}
			return false;
		}

		//Plays the video
		void playVideo(string path)
		{
			v.enabled = true;
			Debug.Log ("playing video...");
			Handheld.PlayFullScreenMovie(path, Color.black,
				FullScreenMovieControlMode.Full, FullScreenMovieScalingMode.AspectFill);
		}*/
	}
}
