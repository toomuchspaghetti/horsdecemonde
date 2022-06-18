using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance {get; private set;} //singleton

    [SerializeField]
    Transform folder;

    [SerializeField]
    GameObject scrolling_text_prefab;
    [SerializeField]
    GameObject end_screen_prefab;
    //[SerializeField]
    //GameObject item_image_prefab;

    //List<Image> item_images;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateScrollingText(string p_text, float p_lifetime) {
        GameObject new_scrolling_text = Instantiate(scrolling_text_prefab, folder);
        new_scrolling_text.GetComponent<ScrollingText>().Init(p_text, p_lifetime);
    }

    public void ShowEndScreen() {
        Instantiate(end_screen_prefab, folder);
    }

    /*public void AddItemImage(Item p_item) {
        GameObject new_item_image_gameobject = Instantiate(item_image_prefab, folder);
        Image new_item_image = new_item_image_gameobject.GetComponent<Image>();
        new_item_image.sprite = p_item.sprite;
        item_images.Add(new_item_image);
    }

    public void RemoveItemImage(Item p_item) {
        foreach (Image item_image in item_images) {
            if (item_image.sprite == p_item.sprite)
                Destroy(item_image.gameObject);
                item_images.Remove(item_image);
        }
    }*/
}
