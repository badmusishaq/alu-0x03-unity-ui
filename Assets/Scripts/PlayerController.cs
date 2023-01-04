using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerBody;

    public float speed;

    int score = 0;
    public int health = 5;

    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text winLoseText;

    public Image winLoseBG;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            playerBody.AddForce(Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            playerBody.AddForce(-Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerBody.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerBody.AddForce(Vector3.right * speed);
        }

        if(health == 0)
        {
            SetYouLose();
            //Debug.Log("Game Over!");
            StartCoroutine(ReloadGame());
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            score++;
            //Debug.Log("Score: " + score);
            SetScoreText();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            health--;
            //Debug.Log("Health: " + health);
            SetHealthText();
        }

        if (other.gameObject.CompareTag("Goal"))
        {
            //Debug.Log("You Win");
            SetYouWin();
        }

    }

    void SetScoreText()
    {
        scoreText.SetText("Score : " + score);
    }

    void SetHealthText()
    {
        healthText.SetText("Health : " + health);
    }

    void SetYouWin()
    {
        winLoseText.SetText("You Win!");
        winLoseText.color = Color.black;
        winLoseBG.color = Color.green;
        winLoseBG.gameObject.SetActive(true);
    }

    void SetYouLose()
    {
        winLoseText.SetText("You Lose!");
        winLoseText.color = Color.white;
        winLoseBG.color = Color.red;
        winLoseBG.gameObject.SetActive(true);
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(3);

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        health = 5;
        score = 0;
    }
}
