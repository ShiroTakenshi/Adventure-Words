using System.Collections;
using UnityEngine;

using UnityEngine.SceneManagement;
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
    // public int coin;
    public GameObject lose;
    Vector2 play; //variabel vector untuk posisi start
    public bool play_again;
    [SerializeField] private Text info_heart; // Variabel Heart
    Text info_Coin; // Variabel untuk Koin
    private string[] scene;
    [SerializeField] private GameObject objectivePlayers;
    [SerializeField] private Text objectiveText;
    private int go, totalPoints, objectivePoints;
    [SerializeField] private string Level1;
    [SerializeField] private string Level2;
    [SerializeField] private string Level3;
    [SerializeField] private string Level4;
    [SerializeField] private string Level5;

    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource dieAudio;
    [SerializeField] AudioSource checkpointAudio;


    Animator anim;
    private void Awake()
    {

    }


    void Start()
    {
        play = transform.position; //start sebagai object transform posisi
        lompat = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // info_Coin = GameObject.Find("UI_Coin").GetComponent<Text>();
        totalPoints = objectivePlayers.transform.childCount;
    }

    public bool ButtonLeft;
    public bool ButtonRight;
    public bool ButtonJump;

    public void buttonDownLeft()
    {
        ButtonLeft = true;
    }
    public void buttonUpLeft()
    {
        ButtonLeft = false;
    }
    public void buttonDownRight()
    {
        ButtonRight = true;
    }
    public void buttonUpRight()
    {
        ButtonRight = false;
    }
    public void buttonJump()
    {
        do
        {
            ButtonJump = true;
        } while (tanah == false);
    }

    // Update is called once per frame
    void Update()
    {
        MethodObjectives();
        go = objectivePlayers.transform.childCount;
        if (go == 0)
        {
            objectiveText.text = "Good Joob!. Now Find A Door To Get Next Level";
        }

        if (play_again == true)
        {
            dieAudio.Play();
            transform.position = play;
            play_again = false;
        }

        if (tanah == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        info_heart.text = "Life : " + heart.ToString(); //Heart yaitu Variabel di Atribut Player
        // info_Coin.text = "Promogem : " + coin.ToString();

        if (Input.GetKey(KeyCode.D) || (ButtonRight == true))
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
        else if (Input.GetKey(KeyCode.A) || (ButtonLeft == true))
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

        if (tanah == true && Input.GetKeyDown(KeyCode.Space) || (ButtonJump == true))
        {
            jumpAudio.Play();
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
            Debug.Log("Player Wafat");
        }
    }
    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Monster")
        {
            Debug.Log("Monster Trigger");
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(loadMiniGames(Level1));
                Destroy(other.gameObject);
                objectivePoints++;
            }
        }
        if (other.gameObject.tag == "Monster2")
        {
            Debug.Log("Monster Trigger1");
            StopCoroutine(loadMiniGames(Level1));
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(loadMiniGames(Level2));
                Destroy(other.gameObject);
                objectivePoints++;
            }
        }
        if (other.gameObject.tag == "Monster3")
        {
            Debug.Log("Monster Trigger2");
            StopCoroutine(loadMiniGames(Level2));
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(loadMiniGames(Level3));
                Destroy(other.gameObject);
                objectivePoints++;
            }
        }
        if (other.gameObject.tag == "Monster4")
        {
            Debug.Log("Monster Trigger3");
            StopCoroutine(loadMiniGames(Level3));
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(loadMiniGames(Level4));
                Destroy(other.gameObject);
                objectivePoints++;
            }
        }
        if (other.gameObject.tag == "Monster5")
        {
            Debug.Log("Monster Trigger4");
            StopCoroutine(loadMiniGames(Level4));
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(loadMiniGames(Level5));
                Destroy(other.gameObject);
                objectivePoints++;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            checkpointAudio.Play();
            play = other.transform.position;
            Debug.Log("Checkpoint");
            StopAllCoroutines();
        }
    }

    IEnumerator loadMiniGames(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Additive);
        yield return new WaitUntil(() => SceneManager.GetSceneByName(Name).isLoaded);
    }

    private void MethodObjectives()
    {
        objectiveText.text = "Find All The Objectives " + objectivePoints + "/" + totalPoints;
    }
}
