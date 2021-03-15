using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    public Text score;
    public Text win;
    public GameObject[] lives;
    public GameObject Player;
    private int scoreValue = 0;
    private int life;
    private bool death;
    private string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        win.text = "";
        life = lives.Length;
        Scene currentScene = SceneManager.GetActiveScene ();
        sceneName = currentScene.name;
        musicSource.clip = musicClipOne;
        musicSource.Play();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
                if (death == true)
        {
            win.text = "Sorry, You've Lost! Game By Alexys Aponte";
            Player.SetActive(false);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue >=4)
            {
                if (sceneName == "Challenge2")
                {
                    SceneChange();
                }
                else 
                {
                    musicSource.clip = musicClipTwo;
                    musicSource.Play();
                    win.text = "You've Won! Game By Alexys Aponte";
                }
            }
        }
         if (collision.collider.tag == "Enemy")
        {
            TakeDamage(1);
            Debug.Log("Enemy Hit");
            Destroy(collision.collider.gameObject);
        }


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
        void SceneChange ()
    {
        SceneManager.LoadScene ("Challenge2 -2");
    }

    void TakeDamage(int damage)
    {
        if (life >= 1)
         {
             life -= damage;
             Destroy(lives[life].gameObject);
             if (life < 1)
             {
                 death = true;
             }
         }
    }

    }
