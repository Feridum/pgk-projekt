using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject ladder;


    Vector2 startPosition;
    Vector2 ladderSize;

    bool isLadder = false;
    void Start()
    {
        startPosition = transform.position;
        ladderSize = ladder.GetComponent<Renderer>().bounds.size;
    }

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemies.Length == 0 && !isLadder)
        {
            placeLadder();
        }
    }

    void placeLadder()
    {
        int i = 0;
        Collider2D board;
        do
        {

            Instantiate(ladder, new Vector2(startPosition.x, startPosition.y - ladderSize.y * i), Quaternion.identity);
            Vector2 newPosition = new Vector2(startPosition.x, startPosition.y - ladderSize.y*(i+1));
            board = Physics2D.OverlapCircle(newPosition, 0.1f, 1 << LayerMask.NameToLayer("Board"));
            i= i+1;
        } while (!board && i <20);
        isLadder = true;
    }

   

}
