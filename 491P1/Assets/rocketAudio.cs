using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketAudio : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}

	private void OnDisable()
	{
        AkSoundEngine.PostEvent("stopRocket", gameObject);
	}
	void OnEnable()
	{
		AkSoundEngine.PostEvent ("Rocket", gameObject);
       

	}
    
	void OnDestroy(){
		AkSoundEngine.PostEvent ("stopRocket", gameObject);
      
	}
}
