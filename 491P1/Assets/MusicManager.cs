using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public bool musicPlaying = false;
	public int currentMode = 0; // 0 = Menu, 1 = Classic, 2 = Modern, 3 = starFgither
    private bool chatterOngoing = false;
    private bool chatterEnabled = false;
    private bool initialsoundended = false;
    private bool stingerPlaying = false; 
	// Use this for initialization
	void Start () {
		currentMode = 0;
        chatterEnabled = false;
	}
	



	public void playMusic()
	{
		if (musicPlaying == false) {
			AkSoundEngine.PostEvent ("PlayMusic", this.gameObject);
			musicPlaying = true;
		}
	}
	//only used to restart music after player dies
	public void playModeMusic()
	{
		//AkSoundEngine.PostEvent ("StopEverything", this.gameObject);
		AkSoundEngine.PostEvent ("stopMusic", this.gameObject);
		AkSoundEngine.PostEvent ("PlayMusic", this.gameObject);
		if (currentMode == 0) {
			
			AkSoundEngine.SetSwitch ("Music", "Menu", this.gameObject);
            AkSoundEngine.PostEvent("stopVA", this.gameObject);
            AkSoundEngine.PostEvent("stopChatter", this.gameObject);
            chatterEnabled = false;
		}
		if (currentMode == 1) {
    			
			AkSoundEngine.SetSwitch ("Music", "Classic", this.gameObject);
            AkSoundEngine.PostEvent("stopVA", this.gameObject);
            AkSoundEngine.PostEvent("classicVA", this.gameObject);
            AkSoundEngine.PostEvent("stopChatter", this.gameObject);
            chatterEnabled = false;
            StartCoroutine("waitTimeInitial");
			/*
			print ("this gets called");
			print ("current mode is " + currentMode);
			resumeMusic ();
			AkSoundEngine.PostEvent ("PlayMusic", this.gameObject);
			AkSoundEngine.SetSwitch ("Music", "Classic", this.gameObject);
			*/
		}
		if (currentMode == 2) {
			
			AkSoundEngine.SetSwitch ("Music", "Modern", this.gameObject);
            AkSoundEngine.PostEvent("stopVA", this.gameObject);
            AkSoundEngine.PostEvent("modernVA", this.gameObject);
            AkSoundEngine.PostEvent("stopChatter", this.gameObject);
            chatterEnabled = false;
            StartCoroutine("waitTimeInitial");
		}
		if (currentMode == 3) {
			
			AkSoundEngine.SetSwitch ("Music", "Starfighter", this.gameObject);
            AkSoundEngine.PostEvent("stopVA", this.gameObject);
            AkSoundEngine.PostEvent("starVA", this.gameObject);
            AkSoundEngine.PostEvent("stopChatter", this.gameObject);
            chatterEnabled = false;
            StartCoroutine("waitTimeInitial");
		}

	}
	public void switchMenu()
	{
		//AkSoundEngine.PostEvent ("StopEverything", this.gameObject);
		AkSoundEngine.SetSwitch ("Music", "Menu", this.gameObject);
        AkSoundEngine.PostEvent("stopVA", this.gameObject);
        AkSoundEngine.PostEvent("stopChatter", this.gameObject);
        chatterEnabled = false;
	}
	public void switchClassic()
	{
		//AkSoundEngine.PostEvent ("StopEverything", this.gameObject);
		AkSoundEngine.SetSwitch ("Music", "Classic", this.gameObject);
        AkSoundEngine.PostEvent("stopVA", this.gameObject);
        AkSoundEngine.PostEvent("classicVA", this.gameObject);
        AkSoundEngine.PostEvent("stopChatter", this.gameObject);
        StartCoroutine("waitTimeInitial");

	}
	public void switchModern()
	{
		//AkSoundEngine.PostEvent ("StopEverything", this.gameObject);
		AkSoundEngine.SetSwitch ("Music", "Modern", this.gameObject);
        AkSoundEngine.PostEvent("stopVA", this.gameObject);
        AkSoundEngine.PostEvent("modernVA", this.gameObject);
        AkSoundEngine.PostEvent("stopChatter", this.gameObject);
        StartCoroutine("waitTimeInitial");
	}
	public void switchstarFighter()
	{
		//AkSoundEngine.PostEvent ("StopEverything", this.gameObject);
		AkSoundEngine.SetSwitch ("Music", "Starfighter", this.gameObject);
        AkSoundEngine.PostEvent("stopVA", this.gameObject);
        AkSoundEngine.PostEvent("starVA", this.gameObject);
        AkSoundEngine.PostEvent("stopChatter", this.gameObject);
        StartCoroutine("waitTimeInitial");

	}
	public void pauseMusic()
	{
		AkSoundEngine.PostEvent ("PauseMusic", this.gameObject);
        AkSoundEngine.PostEvent("pauseVA", this.gameObject);
        AkSoundEngine.PostEvent("stopChatter", this.gameObject);
        AkSoundEngine.PostEvent("pauseAll", this.gameObject);
        chatterEnabled = false;

	}
	public void resumeMusic()
	{
		AkSoundEngine.PostEvent ("ResumeMusic", this.gameObject);
        AkSoundEngine.PostEvent("resumeVA", this.gameObject);
        AkSoundEngine.PostEvent("resumeAll", this.gameObject);
        chatterEnabled = true;
	}
    public void startChatter()
    {
        if (initialsoundended == true)
        {
            if (chatterOngoing == false)
            {
                if (currentMode == 1)
                {
                    AkSoundEngine.PostEvent("classicChatter", this.gameObject);
                    chatterOngoing = true;
                    StartCoroutine(waitTime(Random.Range(12.0f, 18.0f)));
                }
                if (currentMode == 2)
                {
                    AkSoundEngine.PostEvent("modernChatter", this.gameObject);
                    chatterOngoing = true;
                    StartCoroutine(waitTime(Random.Range(12.0f, 18.0f)));
                }
                if (currentMode == 3)
                {
                    AkSoundEngine.PostEvent("starChatter", this.gameObject);
                    chatterOngoing = true;
                    StartCoroutine(waitTime(Random.Range(12.0f, 18.0f)));
                }
            }
        }
        else
        {
            
        }

    }

    public void playStinger()
    {
        if (stingerPlaying == false)
        {
            stingerPlaying = true;
            if (currentMode == 1)
            {
                AkSoundEngine.PostEvent("classicStinger", gameObject);
            }
            if (currentMode == 2)
            {
                AkSoundEngine.PostEvent("modernStinger", gameObject);
            }
            if (currentMode == 3)
            {
                AkSoundEngine.PostEvent("starStinger", gameObject);
            }


            StartCoroutine(waitStinger(Random.Range(10.0f, 15.0f)));
        }

    }

  

	public void Update()
	{
        if (chatterEnabled == true)
        {
            startChatter();
        }

	}

	IEnumerator waitTime(float times)
    {
        
        yield return new WaitForSeconds(times);
        chatterOngoing = false;


    }
    IEnumerator waitStinger(float times)
    {

        yield return new WaitForSeconds(times);
        stingerPlaying = false;


    }
    IEnumerator waitTimeInitial()
    {
        
        yield return new WaitForSeconds(15.5f);
        initialsoundended = true;
        chatterEnabled = true;


    }

}
