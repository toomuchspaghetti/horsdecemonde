using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    float start_time;
    float lifetime; //in seconds

    Text text;

    Color temp_color;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(string p_text, float p_lifetime) {
        text = GetComponent<Text>();

        text.text = p_text; //wow!!!!!!!!!!
        text.color = new Color(1f, 1f, 1f, 0f);
        temp_color = text.color;

        lifetime = p_lifetime;

        StartCoroutine(Scroll());
    }

    IEnumerator Scroll() {
        start_time = Time.time;
        while (true) {
            float point_of_life = (Time.time - start_time) / lifetime; //point_of_life will stay between 0 and 1 during the lifetime in seconds

            if (point_of_life >= 1) //end of lifetime
                Destroy(gameObject); //suicide

            float easing_value = Mathf.Cos(point_of_life * 2f * Mathf.PI + Mathf.PI) / 2f + 0.5f; //also always between 0 and 1

            transform.localPosition = new Vector2(0,
                                                  easing_value * 360f - 540f); //bottom to third of screen 

            temp_color.a = easing_value; //changing the opacity... not much to see here
            text.color = temp_color;

            yield return null;
        }
    }
}
