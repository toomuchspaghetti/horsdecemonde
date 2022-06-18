using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeExit : MonoBehaviour
{
    [SerializeField]
    Pipe pipe;

    Collider2D body_collid;

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

        if (Player.Instance.exiting_pipe)
            return;
        

        pipe.StartPiping((Vector2) transform.position);
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider != body_collid)
            return;

        Player.Instance.exiting_pipe = false;
    }
}
