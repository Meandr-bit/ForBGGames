using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallRight = true;
    public bool WallBottom = true;
    public bool WallTop = true;

    public bool visited = false;
    public int distanceFromStart;
    public bool Finish = false;
}

public class MazeGenerator
{
    public int width = 20;
    public int height = 20;

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[width, height];//возвращаемый результат

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = new MazeGeneratorCell { X = x, Y = y };//размещаем клетку, пишем в нее ее же координаты
            }
        }
        //удаляем стенки по краям
        //самый верхний ряд
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, height - 1].WallLeft = false;
            maze[x, height - 1].WallRight = false;
            maze[x, height - 1].WallTop = false;
        }
        //самый правый ряд
        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[width - 1, y].WallTop = false;
            maze[width - 1, y].WallBottom = false;
            maze[width - 1, y].WallRight = false;
        }

        RemoveWallsWithBacktracker(maze);

        PlaceMazeExit(maze);

        return maze;
    }

    void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.visited = true;
        current.distanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < width - 2 && !maze[x + 1, y].visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < height - 2 && !maze[x, y + 1].visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.distanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y)
            {
                a.WallBottom = false;
                b.WallTop = false;
            }
            else
            {
                b.WallBottom = false;
                a.WallTop = false;
            }
        }
        else
        {
            if (a.X > b.X)
            {
                a.WallLeft = false;
                b.WallRight = false;
            }
            else
            {
                b.WallLeft = false;
                a.WallRight = false;
            }
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];
        /*
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[width - 2, y];
            if (maze[0, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[0, y];
        }
        */
        furthest = maze[8, 8];
        if (furthest.X == 0) furthest.WallLeft = false;//если в левом ряду
        else if (furthest.Y == 0) furthest.WallBottom = false;//в нижнем
        else if (furthest.X == width - 2)//в правом
        {
            maze[furthest.X + 1, furthest.Y].WallLeft = false;
            furthest.WallRight = false;
        }
        else if (furthest.Y == height - 2)//в верхнем
        {
            maze[furthest.X, furthest.Y + 1].WallBottom = false;
            furthest.WallTop = false;
        }
        maze[furthest.X, furthest.Y].Finish = true;
        //else if (furthest.Y == height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;
    }
}