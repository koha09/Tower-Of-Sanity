using System;
using UnityEngine;

/// <summary>
/// Скрипт управления камерой, слежение за игроком для 2D Платформера
/// </summary>
public class FollowCamera : MonoBehaviour
{
    [Header("Camera Target")]
    [SerializeField] [HideInInspector]
    public CameraTarget target;

    [Header("Отступ слежения за игроком и плавность")]
    public Vector3 followOffset = new Vector3(0,0,-20);
    public float Smoothness = 5f;
    public float FreeRange = 0.2f;

    [Header("Границы уровня")]
    public float maxBottom = -5;
    public float maxLeft = -10;
    public float maxRight = 50;

    private Camera cam;
    private Vector3 curVelocity;
    private Vector3 targetPos;

    #region UNITY RUN POINTS 

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        SelectFirstTargetIfNotExist(); 
    }

    void LateUpdate()
    {
        if (!target || !cam) return;

        targetPos = target.transform.position;

        HandelAddFollowOffset();
        HandleSetLevelBorder();
        HandleMoveCamera();
    }
    #endregion

    private void HandleSetLevelBorder()
    {
        //Вычисление краёв уровня
        float fh = cam.orthographicSize;
        float fw = cam.orthographicSize * cam.aspect;
        targetPos.x = Mathf.Max(maxLeft + fw, targetPos.x);
        targetPos.x = Mathf.Min(maxRight - fw, targetPos.x);
        targetPos.y = Mathf.Max(maxBottom + fh, targetPos.y);
    }
    
    private void HandleMoveCamera()
    {
        if ((targetPos - transform.position).magnitude > (FreeRange/10f))
        {
            //Move camera
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curVelocity, 1f / Smoothness, Mathf.Infinity, Time.deltaTime);
        }
    }

    private void HandelAddFollowOffset()
    {
        targetPos = target.transform.position + followOffset;
    }

    protected float FrustrumHeightFromPerspective => Mathf.Abs(transform.position.z) * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);

    protected float FrustrumWidthPerspective => FrustrumHeightFromPerspective * cam.aspect;


    public void SelectFirstTargetIfNotExist()
    {
        if (target == null)
        {
            CameraTarget[] followCamerasInScene = FindObjectsOfType<CameraTarget>();
            target = followCamerasInScene.Length > 0 ? followCamerasInScene[0] : null;
        }
    }

    public void ChangeTarget(CameraTarget _target)
    {
        target = _target;
    }

    public void Shake(float intensity = 2f, float duration = 0.5f)
    {
        //TODO Set DOTWEEN camera Shake
    }

}
