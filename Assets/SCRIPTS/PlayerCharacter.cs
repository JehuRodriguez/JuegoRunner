using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerCharacter : BaseCharacter
{
    public float jumpForce = 5f;
    private bool isGrounded;
    public int score = 0; 
    public TextMeshProUGUI scoreText;

    public   new int life = 100;
    public TextMeshProUGUI playerLifeText;
    public GameObject gameOverCanvas;
    public GameObject victoryCanvas;
    public Button restartButton;


    private Rigidbody rb;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        life = 100;
        UpdateLifeUI();  
        UpdateScoreUI();

        gameOverCanvas.SetActive(false);
        victoryCanvas.SetActive(false);
        restartButton.gameObject.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
    }

    protected override void Update()
    {
        base.Update();

        if (canMove)
        {
          
            float move = Input.GetAxis("Horizontal"); 
            rb.velocity = new Vector3(move * 5f, rb.velocity.y, rb.velocity.z); 

           
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        UpdateScoreUI();
    }

    private void Jump()
    {
        if (isGrounded)
        {
            Debug.Log("JUGADOR ESTA SALTANDO");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; 
        }
       

        if (collision.gameObject.CompareTag("Coin"))
        {
            CollectCoin(collision.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        score++; 
        Destroy(coin); 
    }

    private void UpdateScoreUI()
    {
        scoreText.text = "Puntuaci�n: " + score; 
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        life -= damage; 
        UpdateLifeUI();

        Debug.Log("Se baj� vida a " + life);

        if (life <= 0)
        {
            Die();
        }
    }
    private void UpdateLifeUI()
    {
        playerLifeText.text = "Vida: " + life;
    }

    private void Die()
    {  
        Debug.Log("El jugador ha muerto.");
        gameObject.SetActive(false);
        gameOverCanvas.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Limit"))
        {
            ShowVictoryScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        Debug.Log("�Victoria alcanzada!");
        canMove = false;
        victoryCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}

