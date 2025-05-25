using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private string _firstLevel;
    [SerializeField] private string _thisSceneName;
    [SerializeField] private Canvas _settingsMenu;
    [SerializeField] private Canvas _startMenu;
    [SerializeField] private Canvas _gameUI;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioSource _audioSource;
    private float _volume = 1f;

    //void Start()
    //{
    //    _settingsMenu.enabled = false;
    //}

    public void StartGame()
    {
        SceneManager.LoadScene(_firstLevel);
    }

    public void OpenSettings()
    {
        _settingsMenu.enabled = true;
        if (_startMenu.enabled)
            _startMenu.enabled = false;
        if (_gameUI)
            _gameUI.enabled = false;
    }

    public void CloseSettings()
    {
        _settingsMenu.enabled = false;
        if (SceneManager.GetActiveScene().name == _thisSceneName)
            _startMenu.enabled = true;
        if (SceneManager.GetActiveScene().name.Contains("Level"))
            _gameUI.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateVolume(float volume)
    {
        _volume = volume;
        _audioSource.volume = _volume;
        _audioSource.mute = (_volume <= 0);
    }
}
