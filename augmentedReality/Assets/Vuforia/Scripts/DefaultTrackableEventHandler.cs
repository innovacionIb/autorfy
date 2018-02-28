/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES

    public TrackableBehaviour mTrackableBehaviour;
	public GameObject APbtn_web1, APbtn_web2, Pbtn_web1, Pbtn_web2;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region UNTIY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PRIVATE_METHODS

    public virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
		//Agua Piedra
		if(mTrackableBehaviour.TrackableName == "c135bef9-1fcb-4ed5-b79c-47636302e4a1"){
			APbtn_web1.SetActive (true);
			APbtn_web2.SetActive (true);
			Pbtn_web1.SetActive (false);
			Pbtn_web2.SetActive (false);
		}
		if(mTrackableBehaviour.TrackableName == "66d832fb-4129-4ba0-97fb-392d661a62f1"){
			APbtn_web1.SetActive (true);
			APbtn_web2.SetActive (true);
			Pbtn_web1.SetActive (false);
			Pbtn_web2.SetActive (false);
		}
		//Piñata
		if(mTrackableBehaviour.TrackableName == "7390fcb5-fce2-4974-9251-4b94a1c029e0"){
			APbtn_web1.SetActive (false);
			APbtn_web2.SetActive (false);
			Pbtn_web1.SetActive (true);
			Pbtn_web2.SetActive (true);
		}
		if(mTrackableBehaviour.TrackableName == "bad1206a-5576-4456-a764-18ca915a2f03"){
			APbtn_web1.SetActive (false);
			APbtn_web2.SetActive (false);
			Pbtn_web1.SetActive (true);
			Pbtn_web2.SetActive (true);
		}
        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    public virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
		APbtn_web1.SetActive (false);
		APbtn_web2.SetActive (false);
		Pbtn_web1.SetActive (false);
		Pbtn_web2.SetActive (false);
        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    #endregion // PRIVATE_METHODS
}
