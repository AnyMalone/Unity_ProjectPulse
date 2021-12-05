using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubePuzzle : MonoBehaviour
{
    public string cubeName;
    

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == cubeName)
        {
            print(cubeName);
            
        }
    }
    
}
