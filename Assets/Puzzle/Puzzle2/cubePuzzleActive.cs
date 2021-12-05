using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubePuzzleActive : MonoBehaviour
{
    public static int puzzleCounter;
    public static bool puzzleKeyBlue = false;
    public static bool puzzleKeyGreen = false;
    public static bool puzzleKeyYellow = false;
    void Start()
    {
        puzzleCounter = 0;
    }
    void Update()
    {
        puzzleEnd();
    }
    void puzzleEnd()
    {
        if (puzzleKeyBlue && puzzleKeyGreen && puzzleKeyYellow)
        {
            StartCoroutine(endOfPuzzle());
        }
    }
    IEnumerator endOfPuzzle()
    {
        puzzleCounter = 3;
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }
}
