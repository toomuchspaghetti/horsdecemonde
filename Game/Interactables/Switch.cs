using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Button
{
    SpriteRenderer spr_renderer;

    AudioClip switch_sound;

    Sprite off_sprite;
    Sprite on_sprite;

    // Start is called before the first frame update
    void Start()
    {
        spr_renderer = GetComponent<SpriteRenderer>();

        switch_sound = Resources.Load<AudioClip>("Sounds/button_use");

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Switch");
        off_sprite = sprites[0];
        on_sprite = sprites[1];

        spr_renderer.sprite = off_sprite;

        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interaction() {
        AudioController.Instance.PlaySound(switch_sound);
        spr_renderer.sprite = (spr_renderer.sprite == off_sprite ? on_sprite : off_sprite);

        DoActions();
    }
}
