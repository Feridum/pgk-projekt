using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    [SerializeField]
    private GameObject trap;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float jumpForce = 1.0f;

    [SerializeField]
    private float jumpLevelDiff = 2.0f;

    [SerializeField]
    private float trapPropbability = 0.1f;

    [SerializeField]
    private float enemyProbability = 0.2f;

    [SerializeField]
    private float emptySpaceProbability = 0.2f;

    Camera mainCamera;
    Vector2 mainCameraPosition;
    float halfHeight;
    float halfWidth;
    Vector2 startPosition;
    Vector3 tileSize;

    Vector3 enemySize;

    void Start()
    {
        mainCamera = Camera.main;
        mainCameraPosition = mainCamera.transform.position;

        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;

        tileSize = tile.GetComponent<Renderer>().bounds.size;
        enemySize = enemy.GetComponent<Renderer>().bounds.size;
        startPosition = new Vector2(mainCameraPosition.x - halfWidth + tileSize.x / 2, mainCameraPosition.y - halfHeight + tileSize.y / 2);

        generateBoard();
    }

    void generateBoard()
    {
        float height = startPosition.y;
        int align = 1;

        while (height < mainCameraPosition.y + halfHeight)
        {
            createLevel(height, align);

            align = align * -1;
            height = height + jumpForce * jumpLevelDiff;
        }
    }

    void createLevel(float y, int align)
    {
        float percentWidth = Random.Range(0.3f, 0.4f);
        float maxWidth = mainCameraPosition.x + halfWidth - startPosition.x;
        float start = startPosition.x;
        float end = startPosition.x + maxWidth;
        float emptySpace = maxWidth * percentWidth;

        if (y != startPosition.y)
        {

            if (align == 1)
            {
                end = end - emptySpace;
            }
            else
            {
                float moveChance = Random.Range(0.0f, 0.1f);
                start = startPosition.x + emptySpace;
                if (moveChance > 0.3)
                {
                    start = start + 2 * tileSize.x;
                    end = end + 2 * tileSize.x;
                }
            }
        }


        for (float i = start; i < end; i = i + 3 * tileSize.x)
        {

            if (y != startPosition.y)
            {
                float floorChance = Random.Range(0.0f, 1.0f);

                if (floorChance > emptySpaceProbability)
                {
                    placeBlock(i, y);
                }
                else
                {
                    i = i + 2 * tileSize.x;
                }
            }

            else
            {
                placeBlock(i, y);
            }



        }
    }

    void placeBlock(float x, float y)
    {
        float blockChance = Random.Range(0.0f, 1.0f);

        if (blockChance > trapPropbability)
        {
            for (float i = 0; i < 3; i = i + 1)
            {
                Vector2 position = new Vector2(x + i * tileSize.x, y);

                Instantiate(tile, position, Quaternion.identity);
            }

            placeEnemy(x, y);
        }
        else
        {

            Instantiate(trap, new Vector2(x, y), Quaternion.identity);
            for (float i = 1; i < 3; i = i + 1)
            {
                Instantiate(tile, new Vector2(x + i * tileSize.x, y), Quaternion.identity);
            }
        }
    }

    void placeEnemy(float x, float y)
    {
        float enemyChance = Random.Range(0.0f, 1.0f);

        if (enemyChance < enemyProbability && y > startPosition.y)
        {
            Instantiate(enemy, new Vector2(x, y + enemySize.y), Quaternion.identity);
        }
    }
}
