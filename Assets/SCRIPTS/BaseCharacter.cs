using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacter : MonoBehaviour
{
    public float speed = 5f;
    public int life = 3;
    public Text lifeText; 
    public GameObject gameOverPanel; 

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            life--;
            UpdateLifeUI();
            CheckGameOver();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            life--;
            UpdateLifeUI();
            CheckGameOver();
        }
    }

    private void UpdateLifeUI()
    {
        lifeText.text = "Vida: " + life; 
    }

    private void CheckGameOver()
    {
        if (life <= 0)
        {
            
            gameOverPanel.SetActive(true);
            Time.timeScale = 0; 
        }
    }

    protected  virtual void Update()
    {
        Move();
    }
}
