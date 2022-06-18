using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gametimeplanettime : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite_renderer;

    [SerializeField]
    Sprite exploded_sprite; 

    [SerializeField]
    AudioClip explosion_sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode() {
        sprite_renderer.sprite = exploded_sprite;
        AudioController.Instance.PlaySound(explosion_sound);
    }
}
