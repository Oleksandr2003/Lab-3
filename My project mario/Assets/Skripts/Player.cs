using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public SpriteRenderer spriteRenderer;
    public float movementSpeed = 5f;
    public float jumpHeight = 15f;
    public float theTimerToCloseDoor = 0;
    CapsuleCollider2D capsuleCollider;
    Rigidbody2D rbody;
    bool timeStart;

    private float waitTime = 1.0f;
    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
         float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movementVector = new Vector2 (horizontalInput * movementSpeed * 100* Time.deltaTime, rbody.velocity.y);
        Debug.Log(Time.deltaTime);
        rbody.velocity = movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            timeStart = true;
            boxCollider2D.enabled = false;
            //тут
            spriteRenderer.enabled=false;
        }
        if (timeStart)
        {
            timer += Time.deltaTime;
        }
        if (timer > waitTime && !capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Door")))
        {
            boxCollider2D.enabled = true;
            //тут
            spriteRenderer.enabled = true;
            timer = timer - waitTime;
            timeStart = false;
        }
        //Vector2 playerVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //transform.Translate ( playerVector * movementSpeed * Time.deltaTime);
        //Debug.Log(Time.deltaTime);
        if (!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            //   Vector2 jumpVector = new Vector2(0f,jumpHeight);
            //    rbody.velocity += jumpVector;
            rbody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
