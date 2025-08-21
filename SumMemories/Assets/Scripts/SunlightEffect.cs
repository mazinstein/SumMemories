using UnityEngine;
using UnityEngine.UI;

public class SunlightEffect : MonoBehaviour
{
    public float fadeSpeed = 2f;
    public Color sunColor = new Color(1f, 1f, 1f, 0.4f); // светлый экран
    public Color shadowColor = new Color(0f, 0f, 0f, 0.3f); // затемнение

    private Image overlayImage;
    private Color targetColor;

    void Start()
    {
        overlayImage = GetComponent<Image>();
        targetColor = sunColor; // по умолчанию игрок на солнце
    }

    void Update()
    {
        // плавное изменение цвета
        overlayImage.color = Color.Lerp(overlayImage.color, targetColor, fadeSpeed * Time.deltaTime);
    }

    // вызываем извне
    public void SetInShadow(bool inShadow)
    {
        targetColor = inShadow ? shadowColor : sunColor;
    }
}
