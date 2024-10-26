using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : BaseCharacter
{
    public float jumpForce = 5f;
    private bool isGrounded;
    public int score = 0; 
    public Text scoreText;

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
}

