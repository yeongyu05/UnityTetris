using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject blockPrefab;
    public float timer = 0;
    public float interval = 1;

    private new Transform transform;
    private Vector2Int blockCount;

    private Bounds bounds;
    private Bounds blockBounds;
    private GameObject[,] grid;

    private int x = 4;
    private int y = 21;

    Block currentBlock;


    void Start()
    {
        TileRender();

        currentBlock = CreateBlock(x, y);
    }

    void TileRender()
    {
        blockCount = new Vector2Int(10, 24);
        grid = new GameObject[blockCount.x, blockCount.y];
        bounds = GetComponent<SpriteRenderer>().bounds;
        blockBounds = new Bounds(
            center: Vector2.zero,
            size: new Vector3(bounds.size.x / blockCount.x, bounds.size.y / blockCount.y)
        );

        CreateBoard();
        CreateBlock(x, y);
    }

    void CreateBoard()
    {
        Vector2 startPosition = bounds.min + new Vector3(blockBounds.extents.x, blockBounds.extents.y, 0f);
        Vector2 position = startPosition;

        Gizmos.color = Color.red;
        for (int y = 0; y < blockCount.y; y++)
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                // 블럭 하나하나 처리
                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                block.GetComponent<SpriteRenderer>().bounds = new Bounds(
                    center: position,
                    size: blockBounds.size
                );
                block.SetActive(false);

                grid[x, y] = block;

                position.x += blockBounds.size.x;
            }

            position.x = startPosition.x;
            position.y += blockBounds.size.y;
        }
    }

    Block CreateBlock(int column, int row)
    {
        Block newBlock = new Block();
        newBlock.Column = column;
        newBlock.Row = row;
        newBlock.Shape = newBlock.I;

        ToggleGridActive(newBlock, true);

        return newBlock;
    }

    void ToggleGridActive(Block block, bool active)
    {
        int localY = 0;

        for (int worldY = block.Row - 2; worldY < block.Row + 3; worldY++)
        {
            int localX = 0;
            for (int worldX = block.Column - 2; worldX < block.Column + 3; worldX++)
            {
                if(block.Shape[localY, localX])
                {
                    grid[worldX, worldY].SetActive(active);
                }

                localX++;
            }

            localY++;
        }
    }

    void RotateGrid(Block block)
    {
        bool[,] temp = new bool[5, 5];

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                temp[i, j] = block.Shape[4 - j, i];
            }
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                block.Shape[i, j] = temp[i, j];
            }
        }
    }

    void BlockKeyDownHandller()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            {
            //    // (포지션 + 왼쪽 모양 수)가 0보다 작은지
            //    int localY = 0;
            //
            //for (int worldY = currentBlock.Row - 2; worldY < currentBlock.Row + 3; worldY++)
            //{
            //int localX = 0;
            //for (int worldX = currentBlock.Column - 2; worldX < currentBlock.Column + 3; worldX++)
            //            {
            // currentBlock.Shape[localY, localX]
            //int a = currentBlock.Column + 
            //
            //localX++;
            //}
            //
            //localY++;
            //}
            }

            ToggleGridActive(currentBlock, false);
            currentBlock.Column--;
            ToggleGridActive(currentBlock, true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // (포지션 + 오른쪽 모양 수)가 10보다 큰지
            ToggleGridActive(currentBlock, false);
            currentBlock.Column++;
            ToggleGridActive(currentBlock, true);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            ToggleGridActive(currentBlock, false);
            RotateGrid(currentBlock);
            ToggleGridActive(currentBlock, true);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            ToggleGridActive(currentBlock, false);
            currentBlock.Row--;
            ToggleGridActive(currentBlock, true);
        }
    }

    void AutoDrop()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            ToggleGridActive(currentBlock, false);
            currentBlock.Row--;
            ToggleGridActive(currentBlock, true);

            timer = 0;
        }
    }

    private void Update()
    {
        BlockKeyDownHandller();
        AutoDrop();
    }
}
