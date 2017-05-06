using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuFS : MonoBehaviour
{

    private bool toggle;
    public GameObject pauseMenu;

    // Use this for initialization
    void Start()
    {
        toggle = true;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            resetPanels();
        }
    }

    /*private void TogMouseLook(bool enable)
    {
        player.GetComponent<MovementSP>().enabled = enable;
        player.GetComponentInChildren<CameraControllerSP>().enabled = enable;
    }*/

    public void ClickedButton()
    {
        Cursor.visible = toggle;
        toggle = !toggle;
        //TogMouseLook(toggle);
        pauseMenu.SetActive(!toggle);        
    }

    private void resetPanels()
    {
        if (pauseMenu.transform.GetChild(0).gameObject.activeSelf == true)
        {
            pauseMenu.SetActive(toggle);
            Cursor.visible = toggle;
            toggle = !toggle;
            //TogMouseLook(toggle);
        }
        else
        {
            pauseMenu.transform.GetChild(1).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(2).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(3).gameObject.SetActive(false);
            pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
