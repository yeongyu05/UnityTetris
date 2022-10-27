using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject blockPrefab;
    public float timer = 0f;
    public float interval = 1f;

    private new Transform transform;
    private Vector2Int blockCount;

    private Bounds bounds;
    private Bounds blockBounds;
    private bool[,] grid;


    void Start()
    {
        blockCount = new Vector2Int(10, 20);
        grid = new bool[blockCount.x, blockCount.y];
        bounds = GetComponent<SpriteRenderer>().bounds;
        blockBounds = new Bounds(
            center: Vector2.zero,
            size: new Vector3(bounds.size.x / blockCount.x, bounds.size.y / blockCount.y)
        );

        InitGrid();
    }

    void InitGrid()
    {
        Vector2 startPosition = bounds.min + new Vector3(blockBounds.extents.x, blockBounds.extents.y, 0f);
        Vector2 position = startPosition;

        Gizmos.color = Color.red;
        for (int y = 0; y < blockCount.y; y++)
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                // 블럭 하나하나 처리
                grid[x, y] = false;

                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                block.GetComponent<SpriteRenderer>().bounds = new Bounds(
                    center: position,
                    size: blockBounds.size
                );

                position.x += blockBounds.size.x;
            }

            position.x = startPosition.x;
            position.y += blockBounds.size.y;
        }
    }

    private void Update()
    {
        // Object의 이름을 찾음. 가장 처음에 나오는 Object를 반환.
        // GameObject obj1 = transform.Find("Block").gameObject;
        // 자식을 번호로 찾음. 0번째가 첫 번째 자식
        // GameObject obj2 = transform.GetChild(0).gameObject;
        timer += Time.deltaTime;
        if(timer >= Time.deltaTime)
        {
            Debug.Log("test");
            timer = 0;
        }
    }
}
