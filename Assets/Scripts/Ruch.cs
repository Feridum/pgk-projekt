using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ruch : MonoBehaviour
{
    [SerializeField]
    private float MOVEMENT_SPEED = 0.7f;
    private float GRAVITATION = 0.3f;
    [SerializeField]
    private int MAX_JUMP_DURATION = 10;

    [SerializeField]
    private float BASIC_JUMP_FORCE = 1500.0f;
    [SerializeField]
    private float JUMP_FORCE_ADDITION = 400.0f;
    private float FLOOR_LEVEL = -19.0f;
    private float LEFT_WALL_POSSITION = -43.5f;
    private float RIGHT_WALL_POSSITION = 43.5f;

    private bool isOnGround = false;
    private bool jumping = false;
    private bool in_air = true;
    private int jump_duration = 0;
    private float jump_force = 0.0f;
    private bool jumpPressed = false;

    private Collider2D characterCollider;
    private Rigidbody2D charackterRigidbody;

    private bool onLadder = false;

    private GameObject finish; 

    // Start is called before the first frame update
    void Start()
    {
        characterCollider = GetComponent<Collider2D>();
        charackterRigidbody = GetComponent<Rigidbody2D>();
        finish = GameObject.FindWithTag("Finish");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!onLadder)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(Vector3.left * MOVEMENT_SPEED);
                if (transform.position.x <= LEFT_WALL_POSSITION)
                {
                    Vector3 lWallPosition = new Vector3(LEFT_WALL_POSSITION, transform.position.y, transform.position.z);
                    transform.position = lWallPosition;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(Vector3.right * MOVEMENT_SPEED);
                if (transform.position.x >= RIGHT_WALL_POSSITION)
                {
                    Vector3 rWallPosition = new Vector3(RIGHT_WALL_POSSITION, transform.position.y, transform.position.z);
                    transform.position = rWallPosition;
                }
            }


            if (Input.GetKey(KeyCode.UpArrow) && ((isOnGround && jumpPressed == false) || jumping))
            {
                if (isOnGround && jumpPressed == false)
                {
                    jumpPressed = true;
                    jumping = true;
                    in_air = true;
                    jump_duration = 0;
                    jump_force = BASIC_JUMP_FORCE;
                }
                else
                {
                    if (jump_duration <= MAX_JUMP_DURATION)
                    {
                        jump_force = JUMP_FORCE_ADDITION;
                        jump_duration++;
                    }
                    else
                        jumping = false;
                }
                charackterRigidbody.AddForce(transform.up * jump_force);
                //transform.Translate(Vector3.up * MOVEMENT_SPEED * jump_force);
            }
        }
        else
        {
            Vector3 moveDir = (finish.transform.position - transform.position).normalized;
            transform.position += moveDir * MOVEMENT_SPEED * 10 * Time.deltaTime;
        }
        /*
        else if (in_air)
        {
            jumping = false;
            jump_force = jump_force - GRAVITATION;
            if(jump_force <= 0.0f)
            {
                transform.Translate(Vector3.down * MOVEMENT_SPEED * -jump_force);
                
                if (transform.position.y <= FLOOR_LEVEL)
                {
                    jump_force = 0.0f;
                    in_air = false;
                    Vector3 floorPosition = new Vector3(transform.position.x, FLOOR_LEVEL, transform.position.z);
                    transform.position = floorPosition;
                    jumping = false;
                }
                
            }
            else
                transform.Translate(Vector3.up * MOVEMENT_SPEED * jump_force);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

            isOnGround = true;
            in_air = false;

            if (jumpPressed && !Input.GetKey(KeyCode.UpArrow))
                jumpPressed = false;
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        isOnGround = true;
        in_air = false;

        if (jumpPressed && !Input.GetKey(KeyCode.UpArrow))
            jumpPressed = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
       isOnGround = false;
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            onLadder = true;
        }
    }


}
