using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starAllyDoppler : MonoBehaviour {
    /*
   * DOPPLER EFFECT (Modified from Kenneth CM Young) - Richard L 
   * 
   * Add this to a game object and then pass through dopplerPitch to an RTPC in Wwise
   * Make sure to center your RTPC around 1.0 - dopplerPitch is a multiplier!
   * Make sure to find Listener 
   */


    public float SpeedOfSound = 343.3f;
	public float DopplerFactor = 1.0f;
	private GameObject musicManager;
	private MusicManager musicScript;
	private GameObject Listener;

	Vector3 emitterLastPosition = Vector3.zero;
	Vector3 listenerLastPosition = Vector3.zero;
	void Awake(){
		AkSoundEngine.PostEvent ("startStarAllyEngine", this.gameObject);

		musicManager = GameObject.Find ("WwiseGlobal");
		musicScript = musicManager.GetComponent<MusicManager> ();
		if (musicScript.currentMode == 0) {
			Listener = GameObject.Find("Camera");
		}
		if (musicScript.currentMode == 1) {
			Listener = GameObject.Find("WW2");
		}
		if (musicScript.currentMode == 2) {
			Listener = GameObject.Find("Fighter");
		}
		if (musicScript.currentMode == 3) {
			Listener = GameObject.Find("V_Fighter");
		}

	}
	void OnDestroy(){
		AkSoundEngine.PostEvent ("stopStarAllyEngine", gameObject);
	}
	private void OnDisable()
	{
        AkSoundEngine.PostEvent("stopStarAllyEngine", gameObject);
	}
	// Update is called once per frame
	void FixedUpdate () {

        var playerF = Listener;

        if (playerF)
        {
            var player = Listener;
            // get velocity of source/emitter manually
            Vector3 emitterSpeed = (emitterLastPosition - transform.position) / Time.fixedDeltaTime;
            emitterLastPosition = transform.position;

            // get velocity of listener/player manually
            Vector3 listenerSpeed = (listenerLastPosition - player.transform.position) / Time.fixedDeltaTime;
            listenerLastPosition = player.transform.position;

            // do doppler calc -  (OpenAL's implementation of doppler)
            var distance = (player.transform.position - transform.position); // source to listener vector
            var listenerRelativeSpeed = Vector3.Dot(distance, listenerSpeed) / distance.magnitude;
            var emitterRelativeSpeed = Vector3.Dot(distance, emitterSpeed) / distance.magnitude;
            listenerRelativeSpeed = Mathf.Min(listenerRelativeSpeed, (SpeedOfSound / DopplerFactor));
            emitterRelativeSpeed = Mathf.Min(emitterRelativeSpeed, (SpeedOfSound / DopplerFactor));
            var dopplerPitch = (SpeedOfSound + (listenerRelativeSpeed * DopplerFactor)) / (SpeedOfSound + (emitterRelativeSpeed * DopplerFactor));

            // pass the dopplerPitch through to an RTPC in Wwise (or do whatever you want with the value!)
            AkSoundEngine.SetRTPCValue("starFighterDoppler", dopplerPitch, gameObject); // "DopplerParam" is the name of the RTPC in the Wwise project :)

        }
        else if (playerF == null)
        {
            var player = Listener;
            player = GameObject.Find("FlightCamera");
            // get velocity of source/emitter manually
            Vector3 emitterSpeed = (emitterLastPosition - transform.position) / Time.fixedDeltaTime;
            emitterLastPosition = transform.position;

            // get velocity of listener/player manually
            Vector3 listenerSpeed = (listenerLastPosition - player.transform.position) / Time.fixedDeltaTime;
            listenerLastPosition = player.transform.position;

            // do doppler calc -  (OpenAL's implementation of doppler)
            var distance = (player.transform.position - transform.position); // source to listener vector
            var listenerRelativeSpeed = Vector3.Dot(distance, listenerSpeed) / distance.magnitude;
            var emitterRelativeSpeed = Vector3.Dot(distance, emitterSpeed) / distance.magnitude;
            listenerRelativeSpeed = Mathf.Min(listenerRelativeSpeed, (SpeedOfSound / DopplerFactor));
            emitterRelativeSpeed = Mathf.Min(emitterRelativeSpeed, (SpeedOfSound / DopplerFactor));
            var dopplerPitch = (SpeedOfSound + (listenerRelativeSpeed * DopplerFactor)) / (SpeedOfSound + (emitterRelativeSpeed * DopplerFactor));

            // pass the dopplerPitch through to an RTPC in Wwise (or do whatever you want with the value!)
            AkSoundEngine.SetRTPCValue("starFighterDoppler", dopplerPitch, gameObject); // "DopplerParam" is the name of the RTPC in the Wwise project :)
        }

    }
}
