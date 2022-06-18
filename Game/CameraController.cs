using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance {get; private set;}

    Transform target;
    [SerializeField]
    Vector4 camera_bounds;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        target = Body.Instance.gameObject.transform;
        transform.position = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 new_position = (new Vector2(target.position.x, target.position.y) - (Vector2) transform.position) *
                                5f * // catches up in 5 seconds
                                Time.deltaTime + 
                                (Vector2) transform.position;
        transform.position = new Vector3(Mathf.Clamp(new_position.x, camera_bounds.x, camera_bounds.y), 
                                         Mathf.Clamp(new_position.y, camera_bounds.z, camera_bounds.w),
                                         -10);
    }

    public Vector2 GetPosition() {
        return (Vector2) transform.position;
    }
}
