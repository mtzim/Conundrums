using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {
    public Texture[] number = new Texture[20];
    public float speed = 1f;
    public int current_tex;
    private int i = 0;
    private Renderer rend;
    private float time_passed = 0f;
    private bool change = true;
    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.material.mainTexture = number[0];
    }
	
	// Update is called once per frame
	void Update () {
        if (change) {
            time_passed += Time.deltaTime;
            if (time_passed > speed) {
                i++;
                if (i == number.Length) {
                    i = 0;
                }
                rend.material.mainTexture = number[i];
                time_passed = 0;
            }
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            change = false;
            col.gameObject.GetComponent<Player>().set_turn(++i);
            i--;
        }
    }

    public void step_made() {
        if (i <= 0)
            Destroy(this.gameObject);
        else
            rend.material.mainTexture = number[--i];

    }
}
