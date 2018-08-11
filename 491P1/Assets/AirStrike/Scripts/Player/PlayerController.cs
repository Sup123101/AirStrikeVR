/// <summary>
/// Player controller.
/// </summary>
using UnityEngine;
using System.Collections;

namespace AirStrikeKit
{
	[RequireComponent (typeof(FlightSystem))]

	public class PlayerController : MonoBehaviour
	{
	
		FlightSystem flight;
		// Core plane system
		FlightView View;
		public bool Active = true;
		public bool SimpleControl;
        public GameObject Restart;
		// make it easy to control Plane will turning easier.
		public bool Acceleration;
		// Mobile*** enabled gyroscope controller
		public float AccelerationSensitivity = 5;
		// Mobile*** gyroscope sensitivity
		private TouchScreenVal controllerTouch;
		// Mobile*** move
		private TouchScreenVal fireTouch;
		// Mobile*** fire
		private TouchScreenVal switchTouch;
		// Mobile*** swich
		private TouchScreenVal sliceTouch;
		// Mobile*** slice
		public GUISkin skin;
		public bool ShowHowto;
        public bool currentAccel = false;
        public bool previousAccel = false;
        public bool previousWeapon1 = false;
        public bool previousWeaponVR = false;
        public GameObject musicManager;
        private MusicManager musicScript;
        public GameObject playerEngineAudio;
        public bool isPlayer;
        private bool canChangeWeapon = true;

		void Awake(){
            UnityEngine.XR.InputTracking.disablePositionalTracking = true;
            AirStrikeGame.playerController = this;
            musicManager = GameObject.Find("WwiseGlobal");
            musicScript = musicManager.GetComponent<MusicManager>();
            playerEngineAudio = GameObject.Find("playerEngineAudio");
            if (musicScript.currentMode == 1)
            {
                AkSoundEngine.PostEvent("startClassicPEngine", playerEngineAudio);
            }
            if (musicScript.currentMode == 2)
            {
                AkSoundEngine.PostEvent("startModernPEngine", playerEngineAudio);
            }
            if (musicScript.currentMode == 3)
            {
                AkSoundEngine.PostEvent("startStarPEngine", playerEngineAudio);
            }
		}

		void Start ()
		{
			flight = this.GetComponent<FlightSystem> ();
			View = (FlightView)GameObject.FindObjectOfType (typeof(FlightView));
			// setting all Touch screen controller in the position
			controllerTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, Screen.height - 100));
			fireTouch = new TouchScreenVal (new Rect (Screen.width / 2, 0, Screen.width / 2, Screen.height));
			switchTouch = new TouchScreenVal (new Rect (0, Screen.height - 100, Screen.width / 2, 100));
			sliceTouch = new TouchScreenVal (new Rect (0, 0, Screen.width / 2, 50));
            View.SwitchCameras();

        }
		private void OnDisable()
		{
            AkSoundEngine.PostEvent("stopPEngine", playerEngineAudio);
		}
		private void OnDestroy()
		{
            AkSoundEngine.PostEvent("stopPEngine", playerEngineAudio);
            if (Restart)
            {
                Restart.SetActive(true);
            }
		}
		void Update ()
		{
			if (!flight || !Active)
				return;
			#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
			// On Desktop
			DesktopController ();
			#else
			// On Mobile device
			MobileController ();
			#endif
            if (musicScript.currentMode == 1) //classic engine
            {
                if (previousAccel == false)
                {
                    if (currentAccel == true)
                    {
                        AkSoundEngine.SetState("Classic", "accel");
                        previousAccel = true;
                    }

                }
                if (previousAccel == true)
                {
                    if (currentAccel == false)
                    {
                        AkSoundEngine.SetState("Classic", "deccel");
                        previousAccel = false;
                    }

                }
            }
            if (musicScript.currentMode == 2) //modern engine
            {
                if (previousAccel == false)
                {
                    if (currentAccel == true)
                    {
                        AkSoundEngine.SetState("Modern", "accel");
                        previousAccel = true;
                    }

                }
                if (previousAccel == true)
                {
                    if (currentAccel == false)
                    {
                        AkSoundEngine.SetState("Modern", "deccel");
                        previousAccel = false;
                    }

                }
            }
            if (musicScript.currentMode == 3) //star engine
            {
                if (previousAccel == false)
                {
                    if (currentAccel == true)
                    {
                        AkSoundEngine.SetState("starFighter", "accel");
                        previousAccel = true;
                    }

                }
                if (previousAccel == true)
                {
                    if (currentAccel == false)
                    {
                        AkSoundEngine.SetState("starFighter", "deccel");
                        previousAccel = false;
                    }

                }
            }
		}

		void DesktopController ()
		{
			// Desktop controller
			flight.SimpleControl = SimpleControl;
		
			// lock mouse position to the center.
			MouseLock.MouseLocked = true;
            //print("Mouse X is " + Input.GetAxis("Mouse XVR"));
            if (Input.GetAxis("Mouse YVR") != 0.0f && Input.GetAxis("Mouse XVR") != 0.0f && Input.GetAxis("Mouse XVR") != -1.0f && Input.GetAxis("Mouse XVR") != -1.0f)
            {
                flight.AxisControl(new Vector2(Input.GetAxis("Mouse XVR"), Input.GetAxis("Mouse YVR")));
                if (SimpleControl)
                {
                    flight.TurnControl(Input.GetAxis("Mouse XVR"));
                }
            }
            else
            {
                flight.AxisControl(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
                //flight.AxisControl(new Vector2(Input.GetAxis("X-Axis"), Input.GetAxis("Y-Axis")));

                if (SimpleControl)
                {
                    flight.TurnControl(Input.GetAxis("Mouse X"));
                }
            }

			flight.TurnControl (Input.GetAxis ("Horizontal"));
			flight.SpeedUp (Input.GetAxis ("Vertical"));
            if ((Input.GetAxis("Vertical")) > 0)
            {
                
                currentAccel = true;

            }
            if ((Input.GetAxis("Vertical")) <= 0)
            {
                
                currentAccel = false;
            }
           
		     
			if (Input.GetButton ("Fire1")) {
				flight.WeaponControl.LaunchWeapon ();
                if (previousWeapon1 == false)
                {
                    previousWeapon1 = true;
                    musicScript.playStinger();
                }
			}
            if (Input.GetAxis("TriggerR") == 1.0f)
            {

                flight.WeaponControl.LaunchWeapon();
                if (previousWeaponVR == false)
                {
                    previousWeaponVR = true;
                    musicScript.playStinger();
                }

            }
            if ((Input.GetButton("Fire1")) == false)
            {

                if (previousWeapon1 == true)
                {
                    previousWeapon1 = false;
                    //print("stopped firing normal");
                }

            }
            if ((Input.GetAxis("TriggerR")) == 0.0f)
            {

                if (previousWeaponVR == true)
                {
                    previousWeaponVR = false;
                    //print("stopped fireing");
                }

            }




            if (Input.GetButtonDown ("Fire2")) {
				flight.WeaponControl.SwitchWeapon ();

			}
            //print("getaxis" + Input.GetAxis("TriggerL"));
            if ((Input.GetAxis("TriggerL")) == 1.0f && canChangeWeapon == true)
            {
                canChangeWeapon = false;
                StartCoroutine(WeaponChangeTimer(.33f));
                flight.WeaponControl.SwitchWeapon();

            }
            /*
			if (Input.GetKeyDown (KeyCode.C)) {
				if (View)
					View.SwitchCameras ();	
			}	
            */
            //if (Input.GetKeyDown("joystick button 0"))
            //{
            // UnityEngine.XR.InputTracking.Recenter();
            //if(activeDevice.deviceName == "OpenVR")
            /*if (View)
                View.SwitchCameras ();  */
            //}
        }

		void MobileController ()
		{
			// Mobile controller
		
			flight.SimpleControl = SimpleControl;
		
			if (Acceleration) {
				// get axis control from device acceleration
				Vector3 acceleration = Input.acceleration;
				Vector2 accValActive = new Vector2 (acceleration.x, (acceleration.y + 0.3f) * 0.5f) * AccelerationSensitivity;
				flight.FixedX = false;
				flight.FixedY = false;
				flight.FixedZ = true;
			
				flight.AxisControl (accValActive);
				flight.TurnControl (accValActive.x);
			} else {
				flight.FixedX = true;
				flight.FixedY = false;
				flight.FixedZ = true;
				// get axis control from touch screen
				Vector2 dir = controllerTouch.OnDragDirection (true);
				dir = Vector2.ClampMagnitude (dir, 1.0f);
				flight.AxisControl (new Vector2 (dir.x, -dir.y) * AccelerationSensitivity * 0.7f);
				flight.TurnControl (dir.x * AccelerationSensitivity * 0.3f);
			}
			sliceTouch.OnDragDirection (true);
			// slice speed
			flight.SpeedUp (sliceTouch.slideVal.x);
		
			if (fireTouch.OnTouchPress ()) {
				flight.WeaponControl.LaunchWeapon ();
			}	
			if (switchTouch.OnTouchPress ()) {
		
			}	
		}


        // you can remove this part..
        /*
		void OnGUI ()
		{
			if (!ShowHowto)
				return;
		
			if (skin)
				GUI.skin = skin;
		
			if (GUI.Button (new Rect (20, 150, 200, 40), "Gyroscope " + Acceleration)) {
				Acceleration = !Acceleration;
			}
		
			if (GUI.Button (new Rect (20, 200, 200, 40), "Change View")) {
				if (View)
					View.SwitchCameras ();	
			}
		
			if (GUI.Button (new Rect (20, 250, 200, 40), "Change Weapons")) {
				if (flight)
					flight.WeaponControl.SwitchWeapon ();
			}
		
			if (GUI.Button (new Rect (20, 300, 200, 40), "Simple Control " + SimpleControl)) {
				if (flight)
					SimpleControl = !SimpleControl;
			}

			GUI.Label (new Rect (20, 350, 500, 40), "you can remove this in OnGUI in PlayerController.cs");
		} */
        IEnumerator WeaponChangeTimer(float time)
        {


            yield return new WaitForSecondsRealtime(time);  // I suggest decreasing the time here. One second for each button is quite a long time, which I'm sure you already know.
            canChangeWeapon = true;
        }
    }
    
}
