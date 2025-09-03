using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("UI Elements")]
    public Image musicIcon;       // иконка на кнопке
    public Sprite musicOnIcon;
    public Sprite musicOffIcon;

    private bool isMusicOn = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // сохраняем музыку при смене сцен
    }

    private void Start()
    {
        UpdateMusicIcon();
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;
        UpdateMusicIcon();
    }

    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }

    private void UpdateMusicIcon()
    {
        if (musicIcon != null)
            musicIcon.sprite = isMusicOn ? musicOnIcon : musicOffIcon;
    }
}
