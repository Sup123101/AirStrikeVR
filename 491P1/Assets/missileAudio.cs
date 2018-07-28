using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileAudio : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    private void OnDisable()
    {
        AkSoundEngine.PostEvent("stopMissile", gameObject);
    }
	void OnEnable()
	{
		AkSoundEngine.PostEvent ("Missile", gameObject);

	}
	void OnDestroy(){
		AkSoundEngine.PostEvent ("stopMissile", gameObject);
      
	}
}
