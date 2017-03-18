using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {
    private const float step = .25f;
    private Rigidbody rb;

    //these managed by Game_Manager and Player
    private bool turn = false;          //for being able to move available steps
    private bool can_jump = true;       //for hitting the dice
    private int steps_can_move = 0;
    public Game_Manager manager;
    public GameObject dice;             //instantiate when able to jump

    public float thrust = 95f;
    private bool dice_made = false;
    private Dice current_dice;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	void Update () {
        if (turn && steps_can_move > 0) {
            movement();
        }
        //means turn to dice jump
        if (can_jump) {
            if (!dice_made) {
                GameObject inst = Instantiate(dice) as GameObject;
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + .75f, transform.position.z);
                inst.transform.position = pos;
                dice_made = true;
            }
            //need to add floor check so can't repeatedly jump
            if (Input.GetButtonDown("Jump")) {
                rb.AddForce(transform.up * thrust);
                can_jump = false;
                turn = true;
            }
        }
    }

    private void movement() {
        if (Input.GetButtonDown("Up")) {
            Debug.Log("up");
            transform.Translate(Vector3.forward);
            current_dice.transform.Translate(Vector3.forward);
            steps_can_move--;
            current_dice.step_made();
        }
        else if (Input.GetButtonDown("Down")) {
            Debug.Log("down");
            transform.Rotate(180f * Vector3.up);
            current_dice.transform.Rotate(180f * Vector3.up);
        }
        else if (Input.GetButtonDown("Left")) {
            Debug.Log("left");
            transform.Rotate(90f * -Vector3.up);
            current_dice.transform.Rotate(90f * -Vector3.up);
        }
        else if (Input.GetButtonDown("Right")) {
            Debug.Log("right");
            transform.Rotate(90f * Vector3.up);
            current_dice.transform.Rotate(90f * Vector3.up);
        }
        if (steps_can_move <= 0)
            turn = false;
    }

    public void set_turn(int steps) {
        turn = true;
        steps_can_move = steps;
    }

    void OnTriggerEnter(Collider collide) {
        if(collide.tag == "Dice") {
            current_dice = collide.GetComponent<Dice>();
        }
    }
}
