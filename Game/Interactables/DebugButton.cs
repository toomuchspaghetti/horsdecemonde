using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : Interactable
{
    [SerializeField]
    string message;
    [SerializeField]
    int amount_of_uses; //-1 is infinite

    int times_used;

    // Start is called before the first frame update
    void Start()
    {
        interactable = (amount_of_uses != 0);
        times_used = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction() {
        ++times_used;

        if (times_used == amount_of_uses)
            interactable = false;

        UIController.Instance.CreateScrollingText(message, 2f);
    }
}
