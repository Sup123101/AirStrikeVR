using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flakExplosion : MonoBehaviour {

	// Use this for initialization
	
	private void OnEnable()
	{
        AkSoundEngine.PostEvent("flakExplosion", gameObject);

	}

	// Update is called once per frame
	void Update () {
		
	}
}
