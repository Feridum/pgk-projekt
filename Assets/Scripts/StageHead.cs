using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHead : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 0.5f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            Vector3 moveDir = (player.transform.position - transform.position).normalized;
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(player);
        }
    }
}
