using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Button HomeButton;
    public AudioMixer AudioMixer;
    public Slider VolumeSlider;
    void Start()
    {
        var homeBtn = HomeButton.GetComponent<Button>();
        var volumeSlider = VolumeSlider.GetComponent<Slider>();
        
        homeBtn.onClick.AddListener(() => { SceneManager.LoadScene("Home"); });
        volumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Volume"));
    }

    public void SetVolume(float volume)
    {
        Debug.Log($"Volume: {volume}");
        AudioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }
}