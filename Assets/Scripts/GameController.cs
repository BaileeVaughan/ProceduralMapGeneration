using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    private MazeConstructor mC;

    private void Start()
    {
        mC = GetComponent<MazeConstructor>();
        mC.GenerateNewMaze(13, 15);
    }
}
