using UnityEngine;
using System.Collections;
using HWRWeaponSystem;
using UnityEngine.SceneManagement;
namespace AirStrikeKit
{
	public class GameUI : MonoBehaviour
	{

		public GUISkin skin;
		public Texture2D Logo;
		public int Mode;
		private WeaponController weapon;
		public GameObject musicManager;
		private MusicManager musicScript;
		public bool musicpaused = false;
      

        public string[] menuOptions = new string[2];
        public int selectedIndex = 0;
        private bool canInteract = true;
        private bool sceneChange = false;
		void Awake(){
			AirStrikeGame.gameUI = this;
           
		}

		void Start ()
		{
			weapon = AirStrikeGame.playerController.GetComponent<WeaponController> ();
			musicManager = GameObject.Find ("WwiseGlobal");
			musicScript = musicManager.GetComponent<MusicManager> ();
            selectedIndex = 0;
           
		}
       
		void onDestroy()
		{
			musicpaused = false;
			musicScript.resumeMusic ();
		}
        int menuSelection(string[] menuItems, int selectedItem, string direction)
        {
            if (direction == "up")
            {
                if (selectedItem == 0)
                {
                    selectedItem = 1;
                }
                else if (selectedItem == 1)
                {
                    selectedItem = 0;
                }
            }

            if (direction == "down")
            {
                if (selectedItem == 1)
                {
                    selectedItem = 0;
                }
                else if (selectedItem == 0)
                {
                    selectedItem = 1;
                }
            }

            return selectedItem;
        }
		 void Update()
		{
            print("selected index is " + selectedIndex);
            print("state of change is " + sceneChange);
            if (Input.GetKeyDown("joystick button 12"))
                    {
                        Mode = 2;
                    }  
            if (sceneChange == true)
            {
                if (Input.GetAxis("Mouse Y") == -0.7f && canInteract == true)
            {
                canInteract = false;
                selectedIndex = menuSelection(menuOptions, selectedIndex, "down");

                StartCoroutine(MenuChange(.33f));
            }
                //for MAC, change Mouse Y to Axis 3, windows Axis 6
            if (Input.GetAxis("Mouse Y") == 0.7f && canInteract == true)
            { 
                canInteract = false;
                selectedIndex = menuSelection(menuOptions, selectedIndex, "up");
                StartCoroutine(MenuChange(.33f));
            }

            if (Input.GetKeyDown("joystick button 0"))
            {
                handleSelection();
               
            } 
            }

		}
        void handleSelection()
        {
            GUI.FocusControl(menuOptions[selectedIndex]);



            if(menuOptions[selectedIndex] == "Restart")
            {
                musicScript.playModeMusic();

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (menuOptions[selectedIndex] == "Main menu")
            {
                Time.timeScale = 1;
                Mode = 0;

                musicScript.resumeMusic();
                musicpaused = false;
                musicScript.switchMenu();
                musicScript.currentMode = 0;
                //Application.LoadLevel ("Mainmenu");
                SceneManager.LoadScene("Mainmenu");
            }
            if (menuOptions[selectedIndex] == "Resume")
            {
                Mode = 0;
                Time.timeScale = 1;
                musicScript.resumeMusic();
                musicpaused = false;
            }

           
                    
             
        }

		public void OnGUI ()
		{
		
			if (skin)
				GUI.skin = skin;
		
		
			switch (Mode) {
			case 0:
                   
				if (Input.GetKeyDown (KeyCode.Escape)) {
					Mode = 2;	
				}
                   // sceneChange = false;
			
				if (AirStrikeGame.playerController) {
				
					AirStrikeGame.playerController.Active = true;
			
					GUI.skin.label.alignment = TextAnchor.UpperLeft;
					GUI.skin.label.fontSize = 30;
					GUI.Label (new Rect (20, 20, 200, 50), "Kills " + AirStrikeGame.gameManager.Killed.ToString ());
					GUI.Label (new Rect (20, 60, 200, 50), "Score " + AirStrikeGame.gameManager.Score.ToString ());
				
					GUI.skin.label.alignment = TextAnchor.UpperRight;
					GUI.Label (new Rect (Screen.width - 220, 20, 200, 50), "ARMOR " + AirStrikeGame.playerController.GetComponent<DamageManager> ().HP);
					GUI.skin.label.fontSize = 16;
				
		
					if (weapon.WeaponLists [weapon.CurrentWeapon].Icon)
						GUI.DrawTexture (new Rect (Screen.width - 100, Screen.height - 100, 80, 80), weapon.WeaponLists [weapon.CurrentWeapon].Icon);
				
					GUI.skin.label.alignment = TextAnchor.UpperRight;
					if (weapon.WeaponLists [weapon.CurrentWeapon].Ammo <= 0 && weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess > 0) {
						if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo)
							GUI.Label (new Rect (Screen.width - 230, Screen.height - 120, 200, 30), "Reloading " + Mathf.Floor ((1 - weapon.WeaponLists [weapon.CurrentWeapon].ReloadingProcess) * 100) + "%");
					} else {
						if (!weapon.WeaponLists [weapon.CurrentWeapon].InfinityAmmo)
							GUI.Label (new Rect (Screen.width - 230, Screen.height - 120, 200, 30), weapon.WeaponLists [weapon.CurrentWeapon].Ammo.ToString ());
					}
	
				
					GUI.skin.label.alignment = TextAnchor.UpperLeft;
					GUI.Label (new Rect (20, Screen.height - 50, 250, 30), "R Mouse : Switch Guns C : Change Camera");
			
				} else {
					AirStrikeGame.playerController = (PlayerController)GameObject.FindObjectOfType (typeof(PlayerController));
					if (AirStrikeGame.playerController)
						weapon = AirStrikeGame.playerController.GetComponent<WeaponController> ();
				}
				break;
			case 1:
				if (AirStrikeGame.playerController)
					AirStrikeGame.playerController.Active = false;
				
				MouseLock.MouseLocked = false;
                    sceneChange = true;
				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Game Over");
				if (musicpaused == false) {
					musicScript.pauseMusic ();
					musicpaused = true;
				}
                    menuOptions[0] = "Restart";
                    menuOptions[1] = "Main menu";

				GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
                    GUI.SetNextControlName("Restart");
				if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Restart")) {
					
					musicScript.playModeMusic ();

                     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                       
			
				}
                    GUI.SetNextControlName("Main menu");
				if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
					
					musicScript.resumeMusic ();

					musicScript.switchMenu ();
					musicScript.currentMode = 0;
                    SceneManager.LoadScene("Mainmenu");

				}
                    GUI.FocusControl(menuOptions[selectedIndex]);
				break;
		
			case 2:
				if (AirStrikeGame.playerController)
					AirStrikeGame.playerController.Active = false;
                    sceneChange = true;
				MouseLock.MouseLocked = false;
                    menuOptions[0] = "Resume";
                    menuOptions[1] = "Main menu";
                   
				Time.timeScale = 0;

				GUI.skin.label.alignment = TextAnchor.MiddleCenter;
				GUI.Label (new Rect (0, Screen.height / 2 + 10, Screen.width, 30), "Pause");
				if (musicpaused == false) {
					musicScript.pauseMusic ();
					musicpaused = true;
				}

		
				GUI.DrawTexture (new Rect (Screen.width / 2 - Logo.width / 2, Screen.height / 2 - 150, Logo.width, Logo.height), Logo);
                    GUI.SetNextControlName("Resume");
				if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 50, 300, 40), "Resume")) {
					Mode = 0;
					Time.timeScale = 1;
					musicScript.resumeMusic ();
					musicpaused = false;
				}
                    GUI.SetNextControlName("Main menu");
				if (GUI.Button (new Rect (Screen.width / 2 - 150, Screen.height / 2 + 100, 300, 40), "Main menu")) {
					Time.timeScale = 1;
					Mode = 0;

					musicScript.resumeMusic ();
					musicpaused = false;
					musicScript.switchMenu ();
					musicScript.currentMode = 0;
					//Application.LoadLevel ("Mainmenu");
                    SceneManager.LoadScene("Mainmenu");
				}
                    GUI.FocusControl(menuOptions[selectedIndex]);
				break;
			
			}
		
		}
        IEnumerator MenuChange(float time)
        {
            

            yield return new WaitForSecondsRealtime(time);  // I suggest decreasing the time here. One second for each button is quite a long time, which I'm sure you already know.
            canInteract = true;
        } 
	}
}