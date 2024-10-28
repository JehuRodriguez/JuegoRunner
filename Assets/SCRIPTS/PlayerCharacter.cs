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

    
    private Rigidbody rb;
    private bool canMove = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateLifeUI();  
        UpdateScoreUI(); 
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
        scoreText.text = "Puntuación: " + score; 
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        life -= damage; 
        UpdateLifeUI(); 

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
        SceneManager.LoadScene("GameOver");
        gameOverCanvas.transform.Find("RestartButton").gameObject.SetActive(true);
    }

    public void RestartGame()
    {
      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Limit"))
        {
            canMove = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Limit"))
        {
            canMove = true;
        }
    }
}

