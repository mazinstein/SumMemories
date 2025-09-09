using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource musicSource;

    private bool isMusicOn = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Восстановим состояние mute
        isMusicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        musicSource.mute = !isMusicOn;

        // Музыка не останавливается при паузе
        musicSource.ignoreListenerPause = true;
        musicSource.ignoreListenerVolume = true;
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        musicSource.mute = !isMusicOn;

        // Сохраним состояние
        PlayerPrefs.SetInt("MusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsMusicOn()
    {
        return isMusicOn;
    }
}
