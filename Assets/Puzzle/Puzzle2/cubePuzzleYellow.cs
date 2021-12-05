using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubePuzzleYellow : MonoBehaviour
{
    public string cubeName;


    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == cubeName)
        {
            print(cubeName);
            cubePuzzleActive.puzzleKeyYellow = true;
        }
    }
}
