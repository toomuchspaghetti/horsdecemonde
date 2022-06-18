using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PipePoint {
    public PipePoint(Vector2 p_position) {
        position = p_position;
        vector = Vector2.zero;
        distance = 0f;
    }

    public Vector2 position;
    public Vector2 vector;
    public float distance;
}

public class Pipe : MonoBehaviour
{
    [SerializeField]
    float speed;

    List<PipePoint> path;

    int current_point;

    AudioClip pipe_sound;

    // Start is called before the first frame update
    void Start()
    {
        //makes a path of positions that the player will traverse
        path = new List<PipePoint>();

        Transform entries = transform.GetChild(0);
        Transform points = transform.GetChild(1);

        path.Add(new PipePoint((Vector2) entries.GetChild(0).position)); // there is only 2 entries

        foreach (Transform point in points) {
            path.Add(new PipePoint((Vector2) point.position));
        }

        if (entries.childCount == 2)
            path.Add(new PipePoint((Vector2) entries.GetChild(1).position)); // there is only 2 entries

        CalculateVectorsAndDistances();

        pipe_sound = Resources.Load<AudioClip>("Sounds/pipe");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPiping(Vector2 p_start) {
        if (p_start != path[0].position) {
            path.Reverse();
            CalculateVectorsAndDistances();
        }

        current_point = 0;

        Player.Instance.SetPiping(true);
        Player.Instance.exiting_pipe = true;
        Body.Instance.SetPosition(path[0].position);
        AudioController.Instance.PlaySound(pipe_sound);

        StartCoroutine(Pipement());
    }

    IEnumerator Pipement() { //ces noms font aucun sens mais c'est drôle
        while (true) {
            if (Vector2.Distance(path[current_point].position, Body.Instance.GetPosition()) > path[current_point].distance) {
                current_point++;
                Body.Instance.ResetVelocity();
                Body.Instance.SetPosition(path[current_point].position);
            }

            if (current_point + 1 == path.Count) //end of path;
                break;

            Body.Instance.Glide(speed * path[current_point].vector); //go towards the next point

            yield return null;
        }

        Player.Instance.SetPiping(false);
    }

    void CalculateVectorsAndDistances() {
         for (int i = 0; i < path.Count - 1; i++) {
            path[i].vector = (path[i + 1].position - path[i].position).normalized;
            path[i].distance = Vector2.Distance(path[i].position, path[i + 1].position);
        }
    }
}
