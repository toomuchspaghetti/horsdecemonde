using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    Vector2 origin;
    float z;

    [SerializeField]
    float movement_multiplier;

    // Start is called before the first frame update
    void Start()
    {
        origin = (Vector2) transform.position;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, z) +
                             (Vector3) (origin + CameraController.Instance.GetPosition() * movement_multiplier);
    }
}
