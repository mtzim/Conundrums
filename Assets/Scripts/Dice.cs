using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {
    public Texture[] number = new Texture[20];
    public AudioClip[] hitSound = new AudioClip[4];
    public AudioSource diceAudio;
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

        diceAudio = GetComponent<AudioSource>();
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

            if(i == 19) diceAudio.PlayOneShot(hitSound[3]);
            else if(i >= 10) diceAudio.PlayOneShot(hitSound[2]);
            else if(i >= 1) diceAudio.PlayOneShot(hitSound[1]);
            else diceAudio.PlayOneShot(hitSound[3]);

            col.gameObject.GetComponent<Player>().steps_can_move = i+1;
        }
    }

    public void step_made() {
        if (i <= 0)
            Destroy(this.gameObject);
        else
            rend.material.mainTexture = number[--i];
    }
}
