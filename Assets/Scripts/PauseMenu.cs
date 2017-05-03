using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
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
            pauseMenu.SetActive(toggle);
            Cursor.visible = toggle;
            toggle = !toggle;
            //TogMouseLook(toggle);
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
}
