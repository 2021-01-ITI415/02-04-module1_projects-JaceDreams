using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Vector3 moveDirection;

    private bool jump;
    private bool isGrounded;
    public float jumpPower = 1f;
    private Vector3 jumpDirection;

    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpDirection = Vector3.up;
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");

        moveDirection = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        rb.AddForce(moveDirection * speed);

        LayerMask layer = 1 << gameObject.layer;
        layer = ~layer;
        isGrounded = Physics.CheckSphere(transform.position, 1f, layer);
    }

    void Jump()
    {
        if (jump && isGrounded)
        {
            rb.AddForce(jumpDirection * jumpPower, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //jumpDirection = collision.contacts[0].normal;
    }

    void OnCollisionStay(Collision collision)
    {
        jumpDirection = Vector3.zero;
        foreach (ContactPoint c in collision.contacts)
        {
            jumpDirection += c.normal;
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText ()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
