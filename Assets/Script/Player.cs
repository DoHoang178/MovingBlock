using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    private float speed = 20f;
    public CharacterController controller;
    public bool isEnable = false;
    private Color playerColor;
    public event Action OnEventScore;
    public event Action OnEventGameOver;

    private void Awake()
    {
        controller = this.GetComponent<CharacterController>();

    }
    void Start()
    {
        // Tạo màu ngẫu nhiên cho nhân vật
        int randomColor = UnityEngine.Random.Range(0, 3);
        switch (randomColor)
        {
            case 0:
                playerColor = Color.red;
                break;
            case 1:
                playerColor = Color.green;
                break;
            case 2:
                playerColor = Color.blue;
                break;
        }
        GetComponent<Renderer>().material.color = playerColor;
        
    }

    void Update()
    {
        // Điều khiển nhân vật
        if (isEnable)
        {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            //transform.position += movement * speed * Time.deltaTime;
        controller.Move(speed * movement * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Kiểm tra màu của quả bóng
            if (other.gameObject.GetComponent<Renderer>().material.color == playerColor)
            {

                // Tăng điểm
                OnEventScore?.Invoke();
                // Thay đổi màu ngẫu nhiên cho nhân vật
                int randomColor = UnityEngine.Random.Range(0, 3);
                switch (randomColor)
                {
                    case 0:
                        playerColor = Color.red;
                        break;
                    case 1:
                        playerColor = Color.green;
                        break;
                    case 2:
                        playerColor = Color.blue;
                        break;
                }
                GetComponent<Renderer>().material.color = playerColor;

                // Spawn quả bóng mới sau 1 giây
                StartCoroutine(SpawnNewBall(other.gameObject.transform.position));
            }
            else
            {
                // Kết thúc trò chơi
                OnEventGameOver?.Invoke();
            }

            // Xóa quả bóng hiện tại
            BallPoolObject.Instance.ReturnBall(other.gameObject);
        }
    }

    IEnumerator SpawnNewBall(Vector3 position)
    {
        yield return new WaitForSeconds(1);
        //Instantiate(ballPrefab, position, Quaternion.identity);
        BallPoolObject.Instance.GetBall();
    }
}
