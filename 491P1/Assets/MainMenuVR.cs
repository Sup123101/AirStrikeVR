using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuVR : MonoBehaviour {

    public Button classic;
    public Button modern;
    public Button starFighter;
    public GameObject musicManager;
    private MusicManager musicScript;
    public string[] menuOptions = new string[3];
    public int selectedIndex = 0;
    private bool canInteract = true;
    private ColorBlock normColor;
    private ColorBlock highColor;
    // Use this for initialization
    void Start () {
        musicManager = GameObject.Find("WwiseGlobal");
        musicScript = musicManager.GetComponent<MusicManager>();
        musicScript.switchMenu();
        musicScript.playMusic();
        menuOptions[0] = "Classic";
        menuOptions[1] = "Modern";
        menuOptions[2] = "StarFighter";
        


    }
    private void Awake()
    {
        selectedIndex = 0;
    }
    int menuSelection(string[] menuItems, int selectedItem, string direction)
    {
        if (direction == "up")
        {
            if (selectedItem == 0)
            {
                selectedItem = menuItems.Length - 1;
            }
            else
            {
                selectedItem -= 1;
            }
        }

        if (direction == "down")
        {
            if (selectedItem == menuItems.Length - 1)
            {
                selectedItem = 0;
            }
            else
            {
                selectedItem += 1;
            }
        }

        return selectedItem;
    }
    // Update is called once per frame
    void Update () {
        //print("selected index is " + selectedIndex);
        if(selectedIndex == 0)
        {
            if (canInteract == true)
            {
                classic.OnSelect(null);
                modern.OnDeselect(null);
                starFighter.OnDeselect(null);
            }

        }
        if (selectedIndex == 1)
        {
            if (canInteract == true)
            {
                classic.OnDeselect(null);
                modern.OnSelect(null);
                starFighter.OnDeselect(null);
            }
        }
        if (selectedIndex == 2)
        {
            if (canInteract == true)
            {
                classic.OnDeselect(null);
                modern.OnDeselect(null);
                starFighter.OnSelect(null);
            }
        }

        if (Input.GetAxis("Mouse Y") == -0.7f && canInteract == true)
        {
            canInteract = false;
            selectedIndex = menuSelection(menuOptions, selectedIndex, "down");

            StartCoroutine(MenuChange(.33f));
        }

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
    void handleSelection()
    {
        


        switch (selectedIndex)
        {
            case 0:
                loadClassic();
                break;
            case 1:
                loadModern();
                break;
            case 2:
                loadStarFighter();
                break;

            default:
                Debug.Log("None of the above selected..");
                break;
        }
    }
    void loadClassic()
    {
        musicScript.switchClassic();
        musicScript.currentMode = 1;



        SceneManager.LoadScene("Classic");
    }
    void loadModern()
    {
        musicScript.switchModern();
        musicScript.currentMode = 2;



        SceneManager.LoadScene("Modern");
    }
    void loadStarFighter()
    {
        musicScript.switchstarFighter();
        musicScript.currentMode = 3;



        SceneManager.LoadScene("StarFighter");
    }
    IEnumerator MenuChange(float time)
    {


        yield return new WaitForSeconds(time);  // I suggest decreasing the time here. One second for each button is quite a long time, which I'm sure you already know.
        canInteract = true;
    }
}
