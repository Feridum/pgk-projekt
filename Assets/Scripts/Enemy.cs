using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float maxMoveLeft = 0.1f;

    [SerializeField]
    private float maxMoveRight = 0.1f;

    [SerializeField]
    private float movingSpeed = 0.5f;

    private Vector2 initialPosition;
    private Vector2 currentPosition;

    private Rigidbody2D rigidbody2D;

    private float randomizedSpeed;
    int direction = 1;

    private Renderer m_Renderer;
    void Start()
    {
        initialPosition = transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>(); 
       randomizedSpeed = movingSpeed * Random.Range(0.03f, 0.05f);
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        switch (direction)
        {
            case -1:
                if (transform.position.x > initialPosition.x - maxMoveLeft)
                {
                    transform.position = new Vector2(position.x - randomizedSpeed, position.y);
                }
                else
                {
                    direction = 1;
                }
                break;
            case 1:
                if (transform.position.x < initialPosition.x + maxMoveRight)
                {
                    transform.position = new Vector2(position.x + randomizedSpeed, position.y);
                }
                else
                {
                    direction = -1;
                }
                break;
        }

       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("hide");
        Destroy(gameObject);
    }
}
