using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomainLandPortal : MonoBehaviour
{
    BoxCollider2D body_collid;

    // Start is called before the first frame update
    void Start()
    {
        body_collid = Body.Instance.GetCollider();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider != body_collid)
            return;

        GameManager.Instance.SendToRomainLand();
    }
}
