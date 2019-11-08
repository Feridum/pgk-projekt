using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        Collider2D board = Physics2D.OverlapCircle(transform.position, 0.1f, 1 << LayerMask.NameToLayer("Player"));
        if (board)
        {
            SceneManager.LoadScene("Scene");
        }
    }
}
