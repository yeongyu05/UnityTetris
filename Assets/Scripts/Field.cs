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
                // �� �ϳ��ϳ� ó��
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
        // Object�� �̸��� ã��. ���� ó���� ������ Object�� ��ȯ.
        // GameObject obj1 = transform.Find("Block").gameObject;
        // �ڽ��� ��ȣ�� ã��. 0��°�� ù ��° �ڽ�
        // GameObject obj2 = transform.GetChild(0).gameObject;
        timer += Time.deltaTime;
        if(timer >= Time.deltaTime)
        {
            Debug.Log("test");
            timer = 0;
        }
    }
}
