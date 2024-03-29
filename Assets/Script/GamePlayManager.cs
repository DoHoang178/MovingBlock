using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtScore;
    [SerializeField] private Player player;
    [SerializeField] private Image gameOver;
    [SerializeField] private Image gameWon;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        player.OnEventScore += IncreaseScore;
        player.OnEventGameOver += GameOver;
        gameOver.GetComponentInChildren<Button>().onClick.AddListener(Restart);
        gameWon.GetComponentInChildren<Button>().onClick.AddListener(Restart);
        SetUpGame();
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= 10)
        {
            GameWon();
        }
    }
    private void IncreaseScore()
    {
        score += 1;
        txtScore.text = "Score: " + score;
    }
    private void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        player.isEnable = false;
    }
    private void GameWon()
    {
        gameWon.gameObject.SetActive(true);
        player.isEnable = false;
    }
    private void SetUpGame()
    {
        Debug.Log(player.gameObject.transform.position);
        Debug.Log(new Vector3(0, 1.5f, 0) - player.gameObject.transform.position);
        player.controller.Move((new Vector3(0, 1.5f, 0) - player.gameObject.transform.position));

        for (int i = 0; i < 3; i++)
        {
            GameObject ball = BallPoolObject.Instance.GetBall();
            player.isEnable = true;
        }
    }
    private void Restart()
    {
        score = 0;
        txtScore.text = "Score: " + score;
        foreach (GameObject ball in BallPoolObject.Instance.ballPool)
        {
            ball.gameObject.SetActive(false);
        }
        gameOver.gameObject.SetActive(false);
        gameWon.gameObject.SetActive(false);
        SetUpGame();
    }
}
