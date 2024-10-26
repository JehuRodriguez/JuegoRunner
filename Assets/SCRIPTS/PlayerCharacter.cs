using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCharacter : BaseCharacter
{
    public float jumpForce = 5f;
    private bool isGrounded;
    public int score = 0; 
    public Text scoreText;

    public   new int life = 10;
    public Text playerLifeText;

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        UpdateScoreUI();
    }

    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; 
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

    public void TakeDamage(int damage)
    {
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
        SceneManager.LoadScene("GameOver");
    }
}

