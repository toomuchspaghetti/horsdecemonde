using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void FadeScreenAction();

public class FadeScreen : MonoBehaviour
{
    [SerializeField]
    Image image;

    float rate; //-1 is fading out, 1 is fading in, 0.5 is fading in in 2 seconds

    FadeScreenAction action;

    // Start is called before the first frame update
    void Start()
    {
        image.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Fade(float p_rate) {
        Vector4 temp_color = image.color;
        while (true) {
            temp_color.w += p_rate * Time.deltaTime; //changing alpha at rate

            if (temp_color.w < 0 || temp_color.w > 1) {
                image.color = new Vector4(image.color.r,
                                          image.color.g,
                                          image.color.b,
                                          Mathf.Sign(p_rate) == 1 ? 1 : 0);
                break;
            }

            image.color = temp_color;

            yield return null;
        }

        if (action != null) {
            action();
        }
    }

    public void FadeOut(float p_rate, FadeScreenAction p_action) {
        action = p_action;

        StopAllCoroutines();
        StartCoroutine(Fade(-p_rate));
    }

    public void FadeIn(float p_rate, FadeScreenAction p_action) {
        action = p_action;

        StopAllCoroutines();
        StartCoroutine(Fade(p_rate)); //wow
    }

    public void SetColor(Color p_color) {
        image.color = new Vector4(p_color.r, p_color.g, p_color.b, image.color.a);
    }
}
