using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubePuzzleGreen : MonoBehaviour
{
    public string cubeName;


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == cubeName)
        {
            print(cubeName);
            cubePuzzleActive.puzzleKeyGreen = true;
        }
    }
}
