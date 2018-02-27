using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace IBViur
{
	public class Loader : MonoBehaviour {
		public List<Texture2D> loaderAnimations;

		private int animationsIndex;
		// Use this for initialization
		void Start () {
			animationsIndex = 0;
		}
		
		// Update is called once per frame
		void Update () {
			//this.gameObject.renderer.material.mainTexture  = loaderAnimations[animationsIndex];
			this.gameObject.GetComponent<Renderer>().material.mainTexture = loaderAnimations[animationsIndex];
			animationsIndex++;
			if(animationsIndex >= loaderAnimations.Count)
				animationsIndex = 0;
			
		}
	}
}
