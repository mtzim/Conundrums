using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
    public Game_Manager manager;
    public GameObject dice;             //instantiate when able to jump
    public float thrust = 95f;
    public int steps_can_move;

    private Board_Generator board;
    private bool turn;          //for being able to move available steps
    private int player_num;
    private const float step = .25f;
    private Rigidbody rb;
    private bool can_jump = true;       //for hitting the dice
    private bool dice_made = false;
    private Dice current_dice;
    private bool input_clear = false;
    private bool can_move = false;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        steps_can_move = 0;
    }
	
	void Update () {
        gameObject.GetComponentInChildren<Camera>().enabled = turn;
        if (can_move && steps_can_move > 0) {
            movement();
        }
        //means turn to dice jump
        if (turn && can_jump) {
            if (!dice_made) {                
                GameObject inst = Instantiate(dice) as GameObject;
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + .75f, transform.position.z);
                inst.transform.position = pos;
                inst.transform.rotation = this.transform.rotation;
                dice_made = true;
            }
            //need to add floor check so can't repeatedly jump
            if (Input.GetButtonDown("Jump" + player_num)) {
                rb.AddForce(transform.up * thrust);
                can_jump = false;
                can_move = true;
                
            }
        }
    }

    private void movement() {
        if (Input.GetAxis("Horizontal" + player_num) == 0 && Input.GetAxis("Vertical" + player_num) == 0) {
            input_clear = true;
        }
        if (input_clear) {
            float vert = Input.GetAxis("Vertical" + player_num);
            float hori = Input.GetAxis("Horizontal" + player_num);
            if (vert > 0 && forward_is_clear()) {
                input_clear = false;
                transform.Translate(Vector3.forward);
                current_dice.transform.Translate(Vector3.forward);
                steps_can_move--;
                current_dice.step_made();
                board.addToBoard();
            }
            else if(vert < 0) {
                input_clear = false;
                transform.Rotate(180f * Vector3.up);
                current_dice.transform.Rotate(180f * Vector3.up);
            }
            else if (hori < 0) {
                input_clear = false;
                transform.Rotate(90f * -Vector3.up);
                current_dice.transform.Rotate(90f * -Vector3.up);
            }
            else if (hori > 0) {
                input_clear = false;
                transform.Rotate(90f * Vector3.up);
                current_dice.transform.Rotate(90f * Vector3.up);
            }
        }

        if (steps_can_move <= 0) {
            turn = false;
            dice_made = can_move = false;
            can_jump = true;            
        }
    }

    void OnTriggerEnter(Collider collide) {
        if(collide.tag == "Dice") {
            current_dice = collide.GetComponent<Dice>();
        }
        else if(collide.tag == "Ladder") {
        }
    }

    private bool forward_is_clear() {
        bool is_clear = true;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if(hit.distance < 1.5f) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Blocking")) {
                    is_clear = false;
                    if(hit.collider.gameObject.tag == "Ladder") {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                        current_dice.transform.position = new Vector3(current_dice.transform.position.x, current_dice.transform.position.y + 5, current_dice.transform.position.z);
                        steps_can_move--;
                    }
                }
            }
        }
        return is_clear;
    }

    public void set_player_num(int num) {
        player_num = num;
    }

    public bool get_turn() {
        return turn;
    }

    public void set_turn(bool val) {
        turn = val;
    }

    public void set_board_manager(Board_Generator manage) {
        board = manage;
    }
}
