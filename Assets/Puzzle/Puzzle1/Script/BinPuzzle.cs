using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinPuzzle : MonoBehaviour
{
    public bool puzzleCompleted = false;
    public GameObject cubes;

    private void Start()
    {
        cubes.SetActive(false);
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Puzzle"))
        {
            puzzleCompleted = true;
            if (puzzleCompleted)
            {
                cubes.SetActive(true);
            }
        }
    }
}
