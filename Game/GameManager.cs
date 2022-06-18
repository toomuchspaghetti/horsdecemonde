using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    [SerializeField]
    FadeScreen fade_screen;
    [SerializeField]
    Player player;
    [SerializeField]
    Body player_body;

    bool game_started;

    [SerializeField]
    AudioClip music;

    [SerializeField]
    Button exploder_button;

    [SerializeField]
    Gametimeplanettime gamerplanet;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        game_started = false;

        fade_screen.FadeOut(1f, StartGame);

        AudioController.Instance.PlayMusic(music);

        exploder_button.AddAction(EndGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (game_started) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                fade_screen.FadeIn(1f, Quit);
            }
        }
    }

    public void SendToRomainLand() {
        fade_screen.FadeIn(1f, LoadRomainLandScene);
    }

    //FadeScreenActions

    void StartGame() {
        game_started = true;
    } 

    void Quit() {
        Application.Quit();
    }

    void LoadRomainLandScene() {
        SceneManager.LoadScene("RomainLand");
    }

    void ShowEndScreen() {
        UIController.Instance.ShowEndScreen();
    }

    //ButtonActions

    void EndGame() {
        gamerplanet.Explode();
        fade_screen.SetColor(Color.white);
        fade_screen.FadeIn(1f, ShowEndScreen);
        Destroy(Player.Instance);
    }
}
