//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;
using SubjectNerd.Utilities;

public class CameraController : MonoBehaviour
{
    public Transform Target = null;
    public float Speed = 5.0f;
    public Vector3 Offset = Vector3.zero;

    public Camera cameraObject;

    private Vector3 anchorPoint = Vector3.zero;
    private float shakePower = 0.0f;
    private float shakeAmount = 7.5f;

    private bool isXShake = true;
    private bool isYShake = true;

    private float angleAmount = 0.0f;

    private float scaleAmount = 1.0f;

    private RGBSplit rgbSplit;
    private bool isRGBSplit = false;

    private void Start()
    {
        cameraObject = GetComponent<Camera>();
        rgbSplit = GetComponent<RGBSplit>();
    }

    private void LateUpdate()
    {
        if (Target != null)
        {
            anchorPoint = Vector3.Lerp(anchorPoint, Target.position, Speed * Time.deltaTime);
        }

        shakePower -= shakePower / shakeAmount;

        Vector3 shakeVec = Vector3.zero;
        shakeVec.x = (isXShake) ? Random.Range(-shakePower, shakePower) : 0.0f;
        shakeVec.y = (isYShake) ? Random.Range(-shakePower, shakePower) : 0.0f;
        transform.position = anchorPoint + Offset + shakeVec;

        rgbSplit.RCoord = (isRGBSplit) ? new Vector2(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower)) : Vector2.zero;
        rgbSplit.GCoord = (isRGBSplit) ? new Vector2(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower)) : Vector2.zero;
        rgbSplit.BCoord = (isRGBSplit) ? new Vector2(Random.Range(-shakePower, shakePower), Random.Range(-shakePower, shakePower)) : Vector2.zero;

        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0.0f, 0.0f, angleAmount), 5.0f * Time.deltaTime);
        cameraObject.orthographicSize = Mathf.Lerp(cameraObject.orthographicSize, 5.0f * scaleAmount, 5.0f * Time.deltaTime);
    }

    public void Shake(float power, float amount, bool rgbsplit = false, bool xshake = true, bool yshake = true)
    {
        shakePower = power;
        shakeAmount = amount;
        isRGBSplit = rgbsplit;
        isXShake = xshake;
        isYShake = yshake;
    }

    public void SetAngle(float angle)
    {
        angleAmount = angle;
    }

    public void Rotating(float angle)
    {
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
    }

    public void SetScale(float scale)
    {
        scaleAmount = scale;
    }

    public void Scaling(float scale)
    {
        cameraObject.orthographicSize = 5.0f * scale;
    }
}