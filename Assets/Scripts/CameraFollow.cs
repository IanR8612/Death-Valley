using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This was shamelessly stolen from the script provided by Boyd for the
// 2D platformer project and then adapted to have a vertical dimension.

public class CameraFollow : MonoBehaviour
{
    public Transform FollowTransform;
    public GameObject CameraLeftBorder;
    public GameObject CameraRightBorder;
    public GameObject CameraTopBorder;
    public GameObject CameraBottomBorder;
    private Vector3 smoothPosition;
    private float smoothSpeed = 0.2f;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    // Start is called before the first frame update
    void Start()
    {
        cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        cameraHalfHeight = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float borderLeft = CameraLeftBorder.transform.position.x + cameraHalfWidth;
        float borderRight = CameraRightBorder.transform.position.x - cameraHalfWidth;
        float borderTop = CameraTopBorder.transform.position.y - cameraHalfHeight;
        float borderBottom = CameraBottomBorder.transform.position.y + cameraHalfHeight;

        smoothPosition = Vector3.Lerp(this.transform.position,
            new Vector3(Mathf.Clamp(FollowTransform.position.x, borderLeft, borderRight),
            Mathf.Clamp(FollowTransform.position.y, borderBottom, borderTop),
            this.transform.position.z), smoothSpeed);

        this.transform.position = smoothPosition;
    }
}
