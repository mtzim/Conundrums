using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float rotationSpeed = 150f;
    public float moveSpeed = 3f;

    public const int maxHP = 100;
    public int currHP = maxHP;
    public RectTransform hpBar;
    public GameObject swordHitbox;
    public float attackRate = 1f;
    private float nextAttack;

    Animator myAnim;
    bool facingRight;

    // Use this for initialization
    void Start () {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            myAnim = GetComponent<Animator>();
        }
            
    }
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        //checkHealth();
    }

    private void movePlayer()
    {
        if (gameObject.CompareTag("Player0"))
        {
            float x = Input.GetAxis("Horizontal0") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical0") * Time.deltaTime * moveSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));
            if (x > 0 && !facingRight)       flip();
            else if(x < 0 && facingRight)   flip();
            //transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position); //fixes eyes from dissappearing
            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
            if (Input.GetButtonDown("Jump0"))
            {
                swordSwing();
            }
            else if (Time.time > nextAttack)
            {
                swordHitbox.SetActive(false);
            }
        }
        if (gameObject.CompareTag("Player1"))
        {
            float x = Input.GetAxis("Horizontal1") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical1") * Time.deltaTime * moveSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));
            if (x > 0 && !facingRight) flip();
            else if (x < 0 && facingRight) flip();
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
            if (Input.GetButtonDown("Jump1"))
            {
                swordSwing();
            }
            else if (Time.time > nextAttack)
            {
                swordHitbox.SetActive(false);
            }
        }
        if (gameObject.CompareTag("Player2"))
        {
            float x = Input.GetAxis("Horizontal2") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical2") * Time.deltaTime * moveSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));
            if (x > 0 && !facingRight) flip();
            else if (x < 0 && facingRight) flip();
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
            if (Input.GetButtonDown("Jump2"))
            {
                swordSwing();
            }
            else if (Time.time > nextAttack)
            {
                swordHitbox.SetActive(false);
            }
        }
        if (gameObject.CompareTag("Player3"))
        {
            float x = Input.GetAxis("Horizontal3") * Time.deltaTime * moveSpeed;
            float z = Input.GetAxis("Vertical3") * Time.deltaTime * moveSpeed;
            myAnim.SetFloat("speed", Mathf.Abs(x) + Mathf.Abs(z));
            if (x > 0 && !facingRight) flip();
            else if (x < 0 && facingRight) flip();
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            transform.Translate(0f, 0f, z);
            transform.Translate(x, 0f, 0f);
            if (Input.GetButtonDown("Jump3"))
            {
                swordSwing();
            }
            else if (Time.time > nextAttack)
            {
                swordHitbox.SetActive(false);
            }
        }
    }

    /*private void checkHealth()
    {
        if (gameObject.CompareTag("Player0") && (p1HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player1") && (p2HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player2") && (p3HP <= 0))
        {
            gameObject.SetActive(false);
        }
        if (gameObject.CompareTag("Player3") && (p4HP <= 0))
        {
            gameObject.SetActive(false);
        }
    }*/

    private void TakeDamage(int amt)
    {
        currHP -= amt;
        if (currHP <= 0)
        {
            currHP = 0;            
            gameObject.transform.parent.gameObject.SetActive(false);
        }

        hpBar.sizeDelta = new Vector2(currHP, hpBar.sizeDelta.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("SpikeBallFS"))
        {
            TakeDamage(10);
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void swordSwing()
    {
        if (Time.time > nextAttack)
        {
            Debug.Log("Swing!");
            nextAttack = Time.time + attackRate;
            myAnim.SetTrigger("swungSword");
            swordHitbox.SetActive(true);
        }
    }
}
