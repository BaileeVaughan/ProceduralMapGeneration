﻿using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;
    private MazeDataGenerator dG;
    private MazeMeshGenerator mG;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    public int[,] data
    {
        get; private set;
    }
    private void Awake()
    {
        data = new int[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        dG = new MazeDataGenerator();
        mG = new MazeMeshGenerator();
    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for dungeon size.");
        }
        data = dG.FromDimensions(sizeRows, sizeCols);
        DisplayMaze();
    }

    private void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += ",,,,";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }
    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";
        MeshFilter mF = go.AddComponent<MeshFilter>();
        mF.mesh = mG.FromData(data);
        MeshCollider mC = go.AddComponent<MeshCollider>();
        mC.sharedMesh = mF.mesh;
        MeshRenderer mR = go.AddComponent<MeshRenderer>();
        mR.materials = new Material[2] { mazeMat1, mazeMat2 };
    }
}



