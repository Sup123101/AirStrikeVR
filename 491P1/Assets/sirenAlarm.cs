using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sirenAlarm : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void Awake()
	{
        AkSoundEngine.PostEvent("playSirenAlarm", this.gameObject);
	}
	private void OnDestroy()
	{
        AkSoundEngine.PostEvent("stopSirenAlarm", this.gameObject);
	}
	private void OnDisable()
	{
        AkSoundEngine.PostEvent("stopSirenAlarm", this.gameObject);
	}
}
