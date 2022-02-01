using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class MazeSpawner : MonoBehaviour
{
    public static MazeSpawner instance;
    public GameObject Cell_container, CellPrefab, AStar;
    public int mazeWidth, mazeHeight;
    public MazeGeneratorCell[,] maze;
    public int mazeTotalSize;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        MazeGenerator generator = new MazeGenerator();
        generator.width = mazeWidth;
        generator.height = mazeHeight;
        maze = generator.GenerateMaze();

        mazeTotalSize = maze.GetLength(0) * maze.GetLength(1);
        Vector3 offset = new Vector3(-4.1f, 0, -4.1f);
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x, 1, y) + offset, Cell_container.transform.rotation, Cell_container.transform).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallRight.SetActive(maze[x, y].WallRight);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
                c.WallTop.SetActive(maze[x, y].WallTop);
                if (maze[x, y].Finish)
                {
                    c.FinishZone.SetActive(true);
                    PlayerController.instance.Finish = c.FinishZone;
                }
            }
        }
        //AStar.GetComponent<Pathfinding.Pathfinder>().Scan();
        AstarPath.active.Scan();
    }
    

    public void SpawnAMaze()
    {
        //������� ������ ��������
        foreach (Transform cellT in Cell_container.GetComponentsInChildren<Transform>())
        {
            if(cellT != Cell_container.transform)
            {
                Destroy(cellT.gameObject);
            }
        }
        //��������� �����
        MazeGenerator generator = new MazeGenerator();
        generator.width = mazeWidth;
        generator.height = mazeHeight;
        maze = generator.GenerateMaze();

        mazeTotalSize = maze.GetLength(0) * maze.GetLength(1);
        Vector3 offset = new Vector3(-4.1f, 0, -4.1f);
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x, 1, y) + offset, Cell_container.transform.rotation, Cell_container.transform).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallRight.SetActive(maze[x, y].WallRight);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
                c.WallTop.SetActive(maze[x, y].WallTop);
            }
        }
        
    }
}
