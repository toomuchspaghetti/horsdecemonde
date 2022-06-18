using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void TitleScreenButtonAction();

public class TitleScreenButton : MonoBehaviour
{
    [SerializeField]
    RectTransform sprite_transform;

    bool hovered;

    float original_rot;
    float start_seed;

    TitleScreenButtonAction action;

    [SerializeField]
    AudioClip hover;
    [SerializeField]
    AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        original_rot = sprite_transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (hovered) {
            float seed = (Time.time - start_seed) * 10;
            float sin = Mathf.Sin(seed);

            sprite_transform.localEulerAngles = new Vector3(0, 0, sin * 5 + original_rot);

            if (Input.GetMouseButtonDown(0)) {
                AudioController.Instance.PlaySound(click);
                action();
            }
        }
    }

    public void SetHovered(bool p_hovered) {
        if (hovered == p_hovered)
            return;

        hovered = p_hovered;

        if (hovered) {
            start_seed = Time.time;
            AudioController.Instance.PlaySound(hover);
        } else {
            sprite_transform.localEulerAngles = new Vector3(0, 0, original_rot);
        }
    }

    public void SetAction(TitleScreenButtonAction p_action) {
        action = p_action;
    }
}
