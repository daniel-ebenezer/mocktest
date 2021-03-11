using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    private CharacterController controller;
    public float gravity = 9.87f;
    public float speed;
    Vector3 velocity;

    // variables to check if player in ground
    public LayerMask groundLayer;
    public float groundRadius;
    public Transform groundPos;
    bool isGrounded;
    bool isJumping;
    public bool isDashing;
    public bool isRespawn;
    public float respawnTime;

    float jumpTime = 10f;
    float dashTime = 5f;

    public float jumpHeight;

    private Vector3 move;

    public bool isOut;
    public Transform respawnPoint;
    public GameObject endText;
    

    private float x, z;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Jump();
        Dash();
        //print(respawnPoint.gameObject.name);
        //if (isRespawn)
        //{
            //respawnTime -= Time.deltaTime;
            //if (respawnTime <= 0.5f)
            //{
            //transform.position = respawnPoint.position;
            //controller.Move(Vector3.zero);
            //transform.position = outColObject.GetComponent<SpawnPosHolder>().spawnPos.position;
            //    isRespawn = false;
            //respawnTime = 2f;
            //}
        //}
        if(isOut)
        {
            transform.position = respawnPoint.position;
            isOut = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            transform.position = respawnPoint.position;
        }

    }

    void MovePlayer()
    {
        isGrounded = Physics.CheckSphere(groundPos.position, groundRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //movement by getting input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * z + transform.right * x;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y -= gravity * Time.deltaTime;

        if (!isJumping)
        {
            if (!isGrounded)
            {
                //push player down if in the air
                controller.Move(velocity * Time.deltaTime);
            }
        }

    }

    void Jump()
    {
        
        if (isJumping)
        {
            if (jumpTime > 0)
            {
                isGrounded = false;
                velocity.y += gravity * Time.deltaTime;
                controller.Move(new Vector3(0, 1, 2) * speed * Time.deltaTime);
                jumpTime -= 0.05f;
            }
        }
        if (jumpTime <= 0)
        {
            isJumping = false;
            jumpTime = 10f;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E) && dashTime > 0f)
        {
            isDashing = true;
        }
        if (isDashing)
        {
            //controller.Move(move * 50 * Time.deltaTime);
            speed = 50f;
            dashTime -= 0.08f;
        }
        if (dashTime <= 1.4)
        {
            isDashing = false;
            speed = 5f;
            dashTime = 5f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Boost"))
        {
            isJumping = true;
        }
        if (other.gameObject.CompareTag("OutCol"))
        {
            //isRespawn = true;
            //outColObject = other.gameObject;

            //respawnPoint.position=other.gameObject.GetComponent<SpawnPosHolder>().spawnPos.position;
            isOut = true;
        }

        if(other.gameObject.CompareTag("End"))
        {
            endText.SetActive(true);
            speed = 0;

        }

    }
}
