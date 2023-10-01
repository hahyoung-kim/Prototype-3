using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float distance = 5.0f;
    public float height = 1.0f;
    public float width = 1.0f;
    public float heightDamping = 3.0f;
    public float widthDamping = 2.0f;
    public float rotationDamping = 3.0f;
    Transform _selfTransform;
    public Transform targetObject;

    void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        if (!targetObject) return;
        float wantedRotationAngle = targetObject.eulerAngles.y;
        float wantedHeight = targetObject.position.y + height;
        float wantedWidth = targetObject.position.x + width;
        float currentRotationAngle = _selfTransform.eulerAngles.y;
        float currentHeight = _selfTransform.position.y;
        float currentWidth = _selfTransform.position.x;
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        currentWidth = Mathf.Lerp(currentWidth, wantedWidth, widthDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        _selfTransform.position = targetObject.position;
        _selfTransform.position -= currentRotation * Vector3.forward * distance;
        Vector3 currentPosition = transform.position;
        currentPosition.y = currentHeight;
        currentPosition.x = currentWidth;
        _selfTransform.position = currentPosition;
    }
}