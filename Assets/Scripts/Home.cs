using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public Button PlayButton;
    public Button SettingsButton;
    public Button ExitButton;
    public AudioMixer AudioMixer;

    private void Start()
    {
        var playBtn = PlayButton.GetComponent<Button>();
        var settingsBtn = SettingsButton.GetComponent<Button>();
        var exitBtn = ExitButton.GetComponent<Button>();

        AudioMixer.SetFloat("Volume", PlayerPrefs.GetFloat("Volume"));
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();

        playBtn.onClick.AddListener(() => SceneManager.LoadScene("Levels"));
        settingsBtn.onClick.AddListener(() =>
        {
            Debug.Log("Open settings");
            SceneManager.LoadScene("Settings");
        });
        exitBtn.onClick.AddListener(() =>
        {
            Debug.Log("Exit");
            Application.Quit();
        });
    }
}