using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    Renderer rend;
    public Sprite icon;
    public bool IsDestroyed { get; private set; }

    public bool IsVisibleByCamera
    {
        get
        {
            return rend.isVisible;
        }
    }

    public bool IsAttackOrDef {
        get {
            return false;
        }
    }

    public Transform position
    {
        get {return transform;  }
    }


    public Sprite HeroIcon
    {
        get { return icon; }
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnDestroy()
    {
        IsDestroyed = true;
    }
}
