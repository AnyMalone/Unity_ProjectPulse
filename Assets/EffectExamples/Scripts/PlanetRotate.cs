using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotate : MonoBehaviour
{
    public float Rotate = 1f;
    void Update()
    {
        // Rotate around X Axis
        transform.Rotate(Vector3.up * Rotate * Time.deltaTime);

    }
}
