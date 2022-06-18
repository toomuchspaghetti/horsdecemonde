using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    SpriteRenderer sprite_renderer;

    [SerializeField]
    Item item;

    Vector2 original_position;
    float start_time;
    BoxCollider2D body_collid;

    AudioClip pickup_sound;

    // Start is called before the first frame update
    void Start()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();

        original_position = transform.position;
        start_time = Time.time;

        sprite_renderer.sprite = item.sprite;

        body_collid = Body.Instance.GetCollider();

        pickup_sound = Resources.Load<AudioClip>("Sounds/pickup_item");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(original_position.x, Mathf.Sin((Time.time - start_time) * 5) / 10 + original_position.y);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider != body_collid)
            return;

        Player.Instance.AddItem(item);
        UIController.Instance.CreateScrollingText("Tu as ramassé " + item + ".", 5f);
        AudioController.Instance.PlaySound(pickup_sound);

        Destroy(gameObject);
    }
}
