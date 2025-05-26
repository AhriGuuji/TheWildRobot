using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public static MenuUI Instance;
    [SerializeField] private Canvas _settingsMenu;
    [SerializeField] private Canvas _startMenu;
    [SerializeField] private Canvas _gameUI;
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private AudioSource _audioSource;
    private float _volume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Level"))
        {
            _startMenu.enabled = false;
            _gameUI.enabled = true;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Final"))
            _gameUI.enabled = false;

        if (_audioSource == null)
            _audioSource = FindAnyObjectByType<AudioSource>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
        if (SceneManager.GetActiveScene().buildIndex == 0)
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
