/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.Video;


namespace Vuforia
{
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
        private TrackableBehaviour mTrackableBehaviour;
    
        #endregion // PRIVATE_MEMBER_VARIABLES


		public GameObject uiManager, videomanager,video, btns_AP_Vertical, btns_AP_Horizontal, btns_P_Vertical, btns_P_Hroizontal;
        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }

        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS

        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS

        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
			video.SetActive (true);
			uiManager.GetComponent<UIManager> ().activated = true;
			uiManager.GetComponent<UIManager> ().video1 = true;
			GameObject.Find ("ARCamera").GetComponent<AudioSource>().enabled = true;
			//Agua Piedra 1
			if(mTrackableBehaviour.TrackableName == "c135bef9-1fcb-4ed5-b79c-47636302e4a1"){
				videomanager.GetComponent<videoManager>().getVideoName("http://ibinnovation9734.cloudapp.net:8082/media/files/Action/dbcbb7f1-e1b4-4328-b69b-153bc5fb9a9c.mp4");
				btns_AP_Horizontal.SetActive (true);
				btns_AP_Vertical.SetActive (true);
				btns_P_Hroizontal.SetActive (false);
				btns_P_Vertical.SetActive (false);
			}
			//Agua Piedra 2
			if(mTrackableBehaviour.TrackableName == "66d832fb-4129-4ba0-97fb-392d661a62f1"){
				videomanager.GetComponent<videoManager>().getVideoName("http://ibinnovation9734.cloudapp.net:8082/media/files/Action/dbcbb7f1-e1b4-4328-b69b-153bc5fb9a9c.mp4");
				btns_AP_Horizontal.SetActive (true);
				btns_AP_Vertical.SetActive (true);
				btns_P_Hroizontal.SetActive (false);
				btns_P_Vertical.SetActive (false);
			}
			//Piñata 1
			if(mTrackableBehaviour.TrackableName == "bad1206a-5576-4456-a764-18ca915a2f03"){
				videomanager.GetComponent<videoManager>().getVideoName("http://ibinnovation9734.cloudapp.net:8082/media/files/Action/7d235a7b-f1ba-4015-b77f-c71411183f70.mp4");
				btns_AP_Horizontal.SetActive (false);
				btns_AP_Vertical.SetActive (false);
				btns_P_Hroizontal.SetActive (true);
				btns_P_Vertical.SetActive (true);
			}
			//Piñata 2
			if(mTrackableBehaviour.TrackableName == "7390fcb5-fce2-4974-9251-4b94a1c029e0"){
				videomanager.GetComponent<videoManager>().getVideoName("http://ibinnovation9734.cloudapp.net:8082/media/files/Action/7d235a7b-f1ba-4015-b77f-c71411183f70.mp4");
				btns_AP_Horizontal.SetActive (false);
				btns_AP_Vertical.SetActive (false);
				btns_P_Hroizontal.SetActive (true);
				btns_P_Vertical.SetActive (true);
			}
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }
			video.SetActive (false);
			btns_AP_Horizontal.SetActive (false);
			btns_AP_Vertical.SetActive (false);
			btns_P_Hroizontal.SetActive (false);
			btns_P_Vertical.SetActive (false);
			uiManager.GetComponent<UIManager> ().activated = false;
			uiManager.GetComponent<UIManager> ().video1 = false;
			GameObject.Find ("ARCamera").GetComponent<AudioSource>().enabled = false;
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS
    }
}