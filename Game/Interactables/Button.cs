using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ButtonAction();

public class Button : Interactable
{
    List<ButtonAction> actions = new List<ButtonAction>();

    SpriteRenderer sprite_renderer;
    BoxCollider2D collid;

    Sprite pressed_sprite;

    AudioClip press_sound;

    // Start is called before the first frame update
    void Start()
    {
        //actions 

        sprite_renderer = GetComponent<SpriteRenderer>(); 
        collid = GetComponent<BoxCollider2D>();

        pressed_sprite = Resources.LoadAll<Sprite>("Sprites/Button")[1];

        press_sound = Resources.Load<AudioClip>("Sounds/button_use");

        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddAction(ButtonAction p_action) {
        actions.Add(p_action);
    }

    public override void Interaction() {
        interactable = false;

        DoActions();

        AudioController.Instance.PlaySound(press_sound);

        //changes the button's appearance and collision box.
        sprite_renderer.sprite = pressed_sprite;
        collid.offset = new Vector2(0, -0.3125f); 
        collid.size = new Vector2(0.625f, 0.375f);
    }

    protected void DoActions() {
        foreach (ButtonAction action in actions) {
            action();
        }
    }
}
