using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float distance = 3.0f;
    public float height = 3.0f;
    public float heightDamping = 2.0f;
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
        float currentRotationAngle = _selfTransform.eulerAngles.y;
        float currentHeight = _selfTransform.position.y;
        currentRotationAngle =
            Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        _selfTransform.position = targetObject.position;
        _selfTransform.position -= currentRotation * Vector3.forward * distance;
        Vector3 currentPosition = transform.position;
        currentPosition.y = currentHeight;
        _selfTransform.position = currentPosition;
    }
}