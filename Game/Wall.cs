using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    Button trigger_button;

    [SerializeField]
    bool start_inactive;

    // Start is called before the first frame update
    void Start()
    {
        trigger_button.AddAction(Change);

        if (start_inactive) gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ButtonAction
    void Change() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
