using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    
    private void Awake() {
        if (PlayButton != null) {
            PlayButton.onClick.AddListener(() => {
                SceneData.nextScene = "GameScene";
                SceneManager.LoadScene("LoadingScene");
            });
        }
        if (QuitButton != null) {
            QuitButton.onClick.AddListener(() => {
                Application.Quit();
            });
        }
    }


    // Start is called before the first frame update
     private void Start() {
        if (SceneData.nextScene == "GameScene") {
            SceneData.nextScene = "";
            SceneManager.LoadScene("GameScene");

            //Invoke(nameof(NavigateToSceneX), 1f);
        }
        if (SceneData.nextScene == "MainMenuScene") {
            SceneData.nextScene = "";
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
