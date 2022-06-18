using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField]
    FadeScreen fade_screen;

    [SerializeField]
    TitleScreenButton jouer_button;
    [SerializeField]
    TitleScreenButton quitter_button;

    [SerializeField]
    GraphicRaycaster raycaster;

    [SerializeField]
    List<RaycastResult> results;

    [SerializeField]
    AudioClip music;

    bool allowing_button_actions;

    // Start is called before the first frame update
    void Start()
    {
        jouer_button.SetAction(PlayAction);
        quitter_button.SetAction(QuitAction);

        results = new List<RaycastResult>();

        allowing_button_actions = false;

        fade_screen.FadeOut(1f, AllowButtonActions);

        AudioController.Instance.PlayMusic(music);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition != new Vector3(Screen.width, Screen.height)) {
            if (allowing_button_actions) {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                results.Clear();
                raycaster.Raycast(eventData, 
                                  results);

                List<GameObject> results_gameObject = new List<GameObject>();

                foreach (RaycastResult result in results) {
                    results_gameObject.Add(result.gameObject);
                }

                jouer_button.SetHovered(results_gameObject.Contains(jouer_button.gameObject));
                quitter_button.SetHovered(results_gameObject.Contains(quitter_button.gameObject));
            }
        }
    }

    void UnallowButtonActions() {
        allowing_button_actions = false;
        jouer_button.SetHovered(false);
        quitter_button.SetHovered(false);
    }

    //ButtonActions

    void PlayAction() {
        if (!allowing_button_actions)
            return;

        UnallowButtonActions();

        fade_screen.FadeIn(1f, Play);
    }

    void QuitAction() {
        if (!allowing_button_actions)
            return;

        UnallowButtonActions();

        fade_screen.FadeIn(1f, Quit);
    }

    //FadeScreenActions

    void Play() {
        SceneManager.LoadScene("Game");
    } 

    void Quit() {
        Application.Quit();
    }

    void AllowButtonActions() {
        allowing_button_actions = true;
    }
}
