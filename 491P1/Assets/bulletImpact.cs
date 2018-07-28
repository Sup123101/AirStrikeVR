using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletImpact : MonoBehaviour {
    private bool hitsand = false;
    private int hitType = 0;
	// Use this for initialization
	void Start () {
		
	}
    private void OnEnable()
    {
        if (hitType == 1)
        {
            AkSoundEngine.PostEvent("sandBullet", gameObject);
        }
       

	}
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
	{
       
			if (collision.gameObject.tag == "Scene") {
              hitType = 1; 
			}
       

	}
}
