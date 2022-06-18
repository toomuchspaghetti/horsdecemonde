using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField]
    RectTransform title;
    [SerializeField]
    RectTransform gamerplanet;
    [SerializeField]
    RectTransform galaxy;

    Vector3 title_original_pos;
    Vector3 gamerplanet_original_pos;
    Vector3 galaxy_original_pos;

    // Start is called before the first frame update
    void Start()
    {
        title_original_pos = title.localPosition;
        gamerplanet_original_pos = gamerplanet.localPosition;
        galaxy_original_pos = galaxy.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float seed = Time.time;
        float sin = Mathf.Sin(seed);
        float cos = Mathf.Cos(seed);

        title.localPosition = title_original_pos + new Vector3(0, sin * 20);
        gamerplanet.localPosition = gamerplanet_original_pos + new Vector3(0, cos * 20);
        galaxy.localPosition = galaxy_original_pos + new Vector3(cos * 20, sin * 5);
    }
}
