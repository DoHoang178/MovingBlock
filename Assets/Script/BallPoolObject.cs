using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoolObject : MonoBehaviour
{
    private static BallPoolObject _instance;

    public static BallPoolObject Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = "GameManager";
                _instance = obj.AddComponent<BallPoolObject>();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }

    
    public GameObject ballPrefab;
    private int poolSize = 3;
    public List<GameObject> ballPool;
    [SerializeField] private Material[] materials; 
    void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }


        ballPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ball.GetComponent<MeshRenderer>().material = materials[i];
            ballPool.Add(ball);
        }
    }
    void Start()
    {
        
    }

    public GameObject GetBall()
    {
        foreach (GameObject ball in ballPool)
        {
            if (!ball.activeInHierarchy)
            {
                Vector3 randomPos = new Vector3(
                        Random.Range(-35f, 35f),
                        1.5f,
                        Random.Range(-35f, 35f)
                    );
                ball.gameObject.transform.position = randomPos;
                ball.SetActive(true);
                return ball;
            }
        }
        return null;
    }

    public void ReturnBall(GameObject ball)
    {
        ball.SetActive(false);
    }
}
