using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyhole : Button
{
    [SerializeField]
    Item desired_item;

    AudioClip successful_sound;
    AudioClip fail_sound;

    // Start is called before the first frame update
    void Start()
    {
        successful_sound = Resources.Load<AudioClip>("Sounds/button_use"); //might change later
        fail_sound = Resources.Load<AudioClip>("Sounds/button_use"); //might change later

        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interaction() {
        if (Player.Instance.HasItem(desired_item)) {
            interactable = false;

            AudioController.Instance.PlaySound(successful_sound);
            UIController.Instance.CreateScrollingText("Tu as utilisé " + desired_item + ".", 5f);
            Player.Instance.RemoveItem(desired_item);

            DoActions();
        } else {
            UIController.Instance.CreateScrollingText("Tu n'as pas " + desired_item + ".", 5f);
            AudioController.Instance.PlaySound(fail_sound);
        }
    }
}
