using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    BoxCollider2D body_collid;

    SpriteRenderer sprite_renderer;

    [SerializeField]
    float force;

    bool has_launched;

    Sprite normal_sprite;
    Sprite compressed_sprite;

    AudioClip spring_sound;

    // Start is called before the first frame update
    void Start()
    {
        body_collid = Body.Instance.GetCollider();

        sprite_renderer = GetComponent<SpriteRenderer>();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Spring");
        normal_sprite = sprites[0];
        compressed_sprite = sprites[1];

        spring_sound = Resources.Load<AudioClip>("Sounds/spring");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider != body_collid)
            return;

        if (has_launched)
            return;

        Body.Instance.Launch((Vector2) transform.up * force);
        AudioController.Instance.PlaySound(spring_sound);
        StartCoroutine(Animation());
        has_launched = true;
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider != body_collid)
            return;

        has_launched = false;
    }

    IEnumerator Animation() {
        sprite_renderer.sprite = compressed_sprite;
        yield return new WaitForSeconds(0.15f);
        sprite_renderer.sprite = normal_sprite;
    }
}
