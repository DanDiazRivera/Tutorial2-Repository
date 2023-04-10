using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public float speed;
    public Text score;
    public Text lives;
    private int scoreValue = 0;
    private int livesValue = 3;
    private bool tpFlag = true;
    private bool facingRight = true;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip backgroundMusic;
    public AudioSource musicSource;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        anim = GetComponent<Animator>();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        if (vertMovement != 0)
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
     if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
     if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
     if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
     if (facingRight == false && hozMovement > 0)
        {
            Flip();
        } else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
      if (hozMovement == 0 && vertMovement == 0){
            anim.SetInteger("State", 0);
      }
      if (Input.GetKeyDown(KeyCode.W)){

      }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if(scoreValue >= 8){
                winTextObject.SetActive(true);
                Destroy(gameObject);
                musicSource.clip = winSound;
                musicSource.Play();
            }
            if (scoreValue == 4 && tpFlag == true) {
		        transform.position = new Vector3(59f, -2f, 0.0f);
		        tpFlag = false;
                livesValue = 3;
                lives.text = "Lives: " + livesValue.ToString();
		    }
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if(livesValue <= 0){
                loseTextObject.SetActive(true);
                Destroy(gameObject);
                musicSource.clip = loseSound;
                musicSource.Play();
            }
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
