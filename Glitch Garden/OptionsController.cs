using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [Range(0f,1f)][SerializeField] float defaultVolume = 0.8f;
    [SerializeField] Slider difficultySlider;
    [Range(1,10)][SerializeField] int defaultDifficulty = 1;

    private void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        difficultySlider.value = PlayerPrefsController.GetDifficulty();
    }

    private void Update()
    {
        var musicPlayer = FindObjectOfType<MusicPlayer>();
        if (!musicPlayer) { Debug.LogWarning("No music player found"); return; }
        musicPlayer.SetVolume(volumeSlider.value);
    }

    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetDifficulty((int) difficultySlider.value);
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }

    public void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
    }
}
