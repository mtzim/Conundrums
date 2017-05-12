using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
    public Game_Manager manager;
    public GameObject dice;             //instantiate when able to jump
    public float thrust = 95f;
    public int steps_can_move;
    public SpriteRenderer[] armor;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Animator myAnim;

    private int floor = 0;
    private Board_Generator board;
    [SerializeField]private bool turn;          //for being able to move available steps
    public int player_num;
    private const float step = .25f;
    private Rigidbody rb;
    [SerializeField]
    private bool can_jump = true;       //for hitting the dice
    private Dice current_dice;
    private bool input_clear = false;
    [SerializeField]
    private bool can_move = false;
    //private Animator myAnim;
    private bool grounded = false;
    private float groundCheckRadius = 0.2f;
    public AudioClip[] footSteps = new AudioClip[4];
    private AudioSource sounds;
    private GameObject gameCanvas;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start () {
        rb = GetComponent<Rigidbody>();
        myAnim = GetComponentInChildren<Animator>();
        steps_can_move = 0;
        sounds = GetComponent<AudioSource>();
        
    }
	
	void Update () {
        gameObject.GetComponentInChildren<Camera>().enabled = turn;
        if (can_move && steps_can_move > 0) {
            movement();
        }
        //means turn to dice jump
        if (turn && can_jump) {
            if (current_dice == null) {                
                GameObject inst = Instantiate(dice) as GameObject;
                current_dice = inst.gameObject.GetComponent<Dice>();
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + .75f, transform.position.z);
                inst.transform.position = pos;
                inst.transform.rotation = this.transform.rotation;
            }
            //need to add floor check so can't repeatedly jump
            if (grounded && Input.GetButtonDown("Jump" + player_num)) {
                grounded = false;
                myAnim.SetBool("isGrounded", grounded);
                rb.AddForce(transform.up * thrust);
                can_jump = false;
                can_move = true;
                
            }
        }
    }

    void FixedUpdate()
    {
        grounded = true;
        myAnim.SetBool("isGrounded", grounded);

        myAnim.SetFloat("verticalSpeed", rb.velocity.y);
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
                board.add_to_board(floor);
                sounds.PlayOneShot(footSteps[Random.Range(0, 3)]);
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
            can_move = false;
            can_jump = true;            
        }
    }

    void OnTriggerEnter(Collider collide) {
        if(collide.tag == "Dice") {
            current_dice = collide.GetComponent<Dice>();
        }
        else if(collide.tag == "Ladder") {
            floor += 5;
            board.new_floor(floor);
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            if(current_dice != null)
                current_dice.transform.position = new Vector3(current_dice.transform.position.x, current_dice.transform.position.y + 5, current_dice.transform.position.z);
            steps_can_move--;
            current_dice.step_made();
            if (steps_can_move <= 0) {
                current_dice = null;
                turn = false;
                can_move = false;
                can_jump = true;
            }
        }
        else if (collide.tag == "goal")
        {
            switch (player_num)
            {
                case 0:
                    GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p1Score += 250;
                    break;
                case 1:
                    GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p2Score += 250;
                    break;
                case 2:
                    GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p3Score += 250;
                    break;
                case 3:
                    GameObject.Find("Game_Manager").GetComponent<Game_Manager>().p4Score += 250;
                    break;
            }
            gameCanvas = GameObject.Find("Canvas");
            gameCanvas.GetComponent<GameFinished>().finished();
        }
    }

    private bool forward_is_clear() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if(hit.distance < 1.5f) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Blocking"))
                    return false;
            }
        }
        return true;
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

    public void setPlayerColor()
    {
        Color armorColor;
        switch (player_num)
        {
            case 0:
                armorColor = new Color(1f, 0f, 0f, 1f);
                break;
            case 1:
                armorColor = new Color(0f, 0f, 1f, 1f);
                break;
            case 2:
                armorColor = new Color(0f, 1f, 0f, 1f);
                break;
            case 3:
                armorColor = new Color(1f, 0.92f, 0.016f, 1f);
                break;
            default:
                armorColor = new Color(1f, 1f, 1f, 1f);
                break;
        }
        int x = 0;
        foreach(SpriteRenderer i in armor)
        {
            armor[x++].color = armorColor;
        }
    }
}
