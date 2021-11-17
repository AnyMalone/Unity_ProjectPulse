using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinPuzzle : MonoBehaviour
{
    public bool puzzleCompleted = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (puzzleCompleted)
        {
            print(puzzleCompleted);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            puzzleCompleted = true;
        }
    }
}
