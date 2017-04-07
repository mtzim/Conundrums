using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {
    public Game_Manager manager;
    public GameObject dice;             //instantiate when able to jump
    public float thrust = 95f;
    public int player_num;

    private const float step = .25f;
    private Rigidbody rb;
    private bool turn = false;          //for being able to move available steps
    private bool can_jump = true;       //for hitting the dice
    private int steps_can_move = 0;
    private bool dice_made = false;
    private Dice current_dice;
    private bool input_clear = false;


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
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) {
            input_clear = true;
        }
        if (input_clear) {
            float vert = Input.GetAxis("Vertical");
            float hori = Input.GetAxis("Horizontal");
            if (vert > 0) {
                input_clear = false;
                Debug.Log("up");
                transform.Translate(Vector3.forward);
                current_dice.transform.Translate(Vector3.forward);
                steps_can_move--;
                current_dice.step_made();
            }
            else if(vert < 0) {
                input_clear = false;
                Debug.Log("down");
                transform.Rotate(180f * Vector3.up);
                current_dice.transform.Rotate(180f * Vector3.up);
            }
            else if (hori < 0) {
                input_clear = false;
                Debug.Log("left");
                transform.Rotate(90f * -Vector3.up);
                current_dice.transform.Rotate(90f * -Vector3.up);
            }
            else if (hori > 0) {
                input_clear = false;
                Debug.Log("right");
                transform.Rotate(90f * Vector3.up);
                current_dice.transform.Rotate(90f * Vector3.up);
            }
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
