using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gerak : MonoBehaviour
{
    public int kecepatan;
    public int kekuatanlompat;
    public bool balik;
    public int pindah;
    Rigidbody2D lompat;
    public bool tanah;
    public LayerMask targetlayer;
    public Transform deteksitanah;
    public float jangkauan;
    public int heart;
    public int coin;
    public GameObject lose;
    Vector2 play; //variabel vector untuk posisi start
    public bool play_again;
    Text info_heart; // Variabel Heart
    Text info_Coin; // Variabel untuk Koin
    
    Animator anim; 
    void Start()
    {
        play=transform.position; //start sebagai object transform posisi
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        info_heart = GameObject.Find("UI_Heart").GetComponent<Text>(); // Pendefinisian UI Heart sebagai componen Text
        // info_Coin = GameObject.Find("UI_Coin").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()

    {

        if(play_again == true)
        {
            transform.position = play;
            play_again=false;
        }

        if(tanah == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Nyawa : " + heart.ToString(); //Heart yaitu Variabel di Atribut Player
        // info_Coin.text = "Promogem : " + coin.ToString();

        if (Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            pindah = -1;
            if (tanah == true)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }
        }
        else if (Input.GetKey (KeyCode.A))
        {
            transform.Translate(Vector2.left * kecepatan * Time.deltaTime);
            pindah = 1;
            if (tanah == true)
            {
                anim.SetBool("Run", true);
            }
            else
            {
                anim.SetBool("Run", false);
            }    
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (tanah==true && Input.GetKeyDown(KeyCode.Space))
        {
            float x = lompat.velocity.x;
            lompat.velocity = new Vector2(x, kekuatanlompat);
            //lompat.AddForce(new Vector2(0, kekuatanlompat));
        }
        if (pindah > 0 && !balik)
        {
            flip();
        }
        else if (pindah < 0 && balik)
        {
            flip();
        }
        if (heart < 1)
        {
            gameObject.SetActive(false);
            lose.SetActive(true);
            Debug.Log("Player Mokad");
        }

    }

    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }
}
