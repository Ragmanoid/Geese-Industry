using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("GameObject")] 
    public Transform _gameObject;

    [Header("Camera position restrictions")]
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;

    private void UpdateCameraPosition()
    {
        try
        {
            var position = _gameObject.position;
            transform.position = new Vector3(
                Mathf.Clamp(position.x, minX, maxX), 
                Mathf.Clamp(position.y, minY, maxY),
                transform.position.z
            );
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void Update()
    {
        UpdateCameraPosition();
    }
}