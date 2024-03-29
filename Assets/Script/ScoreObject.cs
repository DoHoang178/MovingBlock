using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    private Color ballColor;
    private Rigidbody rb;

    void Start()
    {
        // Đặt màu sắc cho quả bóng
        //GetComponent<MeshRenderer>().material.color = ballColor;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }
}
