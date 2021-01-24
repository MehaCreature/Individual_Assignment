using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KnightController : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed;
    public float topSpeed;
    
    private bool facingRight = true;

    private bool startTimer = false;

    public bool gameOver = false;
    public bool victory = false;

    public bool endstartTune = false;

    //public bool startvictoryTune = false;

    public bool startgameOver = false;

    public float timer = 10;

    public int score;
    public Text scoreCount;

    public Text timerDisplay;

    public GameObject screenOne;
    public GameObject screenTwo;
    public GameObject screenThree;

    public AudioClip collectible;
    public AudioSource audioSource;

    public ParticleSystem money;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator.SetInteger( "State", 1);

        score = 0;

        scoreCount.text = "x " + score.ToString();

        StartCoroutine(StartUp());

        timerDisplay.text = timer.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement =Input.GetAxis("Vertical");
        rigid.AddForce( new Vector2(horizontalMovement * speed, verticalMovement * speed));

        horizontalMovement = horizontalMovement * Time.deltaTime;

        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));


        if ( facingRight == false && horizontalMovement >0)
        {
            Flip();
        }
        if (facingRight == true && horizontalMovement <0)
        {
            Flip();
        }
       if(rigid.velocity.magnitude > topSpeed)
       {
           rigid.velocity = rigid.velocity.normalized * topSpeed;
       }

        if(Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("State", 2);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetInteger("State", 2);
        }

        else animator.SetInteger("State", 1);

        ScoreUI();
         
         if(timer <= 0 )
        {
            timer = 0;
            GameOver(); 
        }

        Timer();

        if(startTimer == true)
        {
            timer -= Time.deltaTime;
        }

        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
        

    }
        
    


    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            rigid.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }

        if(collision.collider.tag == "Goal" && score == 4)
        {
            victory = true;
            VictoryScreen();
            
        }

        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);

            score = score + 1;

            audioSource.PlayOneShot(collectible);

            ParticleSystem moneyObject = Instantiate(money, transform.position, Quaternion.identity);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    public void ScoreUI()
    {
        scoreCount.text = "x " + score.ToString();
    }

    public void Timer()
    {
        timerDisplay.text = timer.ToString("f0");

    }

    public void TimerMech()
    {
        startTimer = true;
        
    }

    public IEnumerator StartUp()
    {
        yield return new WaitForSeconds(2);

        EndInstructions();
    }

    public void EndInstructions()
    {
        screenOne.SetActive(false);

        endstartTune = true;

        TimerMech();
    }

    public void GameOver()
    {
        gameOver = true;
        if(victory == true)
        {

        }

        else if(gameOver == true)
        {
            startgameOver = true;
            screenTwo.SetActive(true);
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;

            if(Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("GemRushRestart");
            }
        }
        

    }
    public void VictoryScreen()
    {
        if(victory == true)
        {
            //audioSource.PlayOneShot(victoryMusic);
            screenThree.SetActive(true);
            rigid.constraints = RigidbodyConstraints2D.FreezeAll;

            if(Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("GemRushRestart");
            }
        }
    }
}
