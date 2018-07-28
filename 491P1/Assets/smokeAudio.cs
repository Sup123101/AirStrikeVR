using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smokeAudio : MonoBehaviour {

	void Awake()
	{
		AkSoundEngine.PostEvent ("startFlames", gameObject);
	}
	void OnDestroy()
	{
		AkSoundEngine.PostEvent ("stopFlames", gameObject);
	}
}
