using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    public Image musicIcon;       // иконка кнопки (Image внутри кнопки)
    public Sprite musicOnIcon;    // спрайт "музыка вкл"
    public Sprite musicOffIcon;   // спрайт "музыка выкл"

    private void OnEnable()
    {
        // Каждый раз при активации сцены/объекта обновляем иконку
        UpdateIcon();
    }

    public void OnButtonClick()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ToggleMusic();
            UpdateIcon();
        }
    }

    private void UpdateIcon()
    {
        if (AudioManager.Instance != null && musicIcon != null)
        {
            bool isOn = AudioManager.Instance.IsMusicOn();
            musicIcon.sprite = isOn ? musicOnIcon : musicOffIcon;
        }
    }
}
