using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    [SerializeField]
    BoxCollider2D collid;
    [SerializeField]
    Transform body_tsf;

    List<Interactable> interactables_colliding;

    // Start is called before the first frame update
    void Start()
    {
        interactables_colliding = new List<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) { //adds the interactable to the list when it enters
        if (collider.gameObject.CompareTag("Interactable")) {
            interactables_colliding.Add(collider.gameObject.GetComponent<Interactable>());
        }
    }

    void OnTriggerExit2D(Collider2D collider) { //removes the interactable from the list when it leaves
        if (collider.gameObject.CompareTag("Interactable")) {
            interactables_colliding.Remove(collider.gameObject.GetComponent<Interactable>());
        }
    }

    public void Interact() {
        List<Interactable> interactable_interactables = new List<Interactable>(); //lol

        //wow....
        //if an interactable has the interactable proprety, add it to the interactables_with_the_interactable_proprety list
        foreach (Interactable interactable in interactables_colliding) {
            if (interactable.interactable) {
                interactable_interactables.Add(interactable);
            }
        }

        if (interactable_interactables.Count == 0)
            return;

        if (interactable_interactables.Count == 1) {
            interactable_interactables[0].Interaction();
            return;
        }
        

        //picks interactable object closest to body :)
        int index_of_closest_interactable = 0;
        float distance_between_closest_interactable_and_body = Vector2.Distance(interactable_interactables[index_of_closest_interactable].transform.position, body_tsf.position);

        for (int i = 1; i < interactable_interactables.Count; i++) {
            float new_distance = Vector2.Distance(interactable_interactables[i].transform.position, body_tsf.position);
            if (new_distance < distance_between_closest_interactable_and_body) {
                index_of_closest_interactable = i;
                distance_between_closest_interactable_and_body = new_distance;
            }
        }

        interactable_interactables[index_of_closest_interactable].Interaction();
    }
}
