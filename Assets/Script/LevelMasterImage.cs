using UnityEngine;
using UnityEngine.UI;

public class LevelMasterImage : MonoBehaviour
{
    [SerializeField] private Sprite Kilitli;
    [SerializeField] private Sprite Kilitsiz;
    [SerializeField] private int lvl;

    private void Awake()
    {
        Image myImage = GetComponent<Image>();

        if (PlayerPrefs.GetInt("Level",0)>=lvl)
        {
            myImage.sprite = Kilitsiz; // Yeni görseli ata
        }
        else if (PlayerPrefs.GetInt("Level", 0) < lvl)
        {
            myImage.sprite = Kilitli; // Yeni görseli ata

        }
        else
        {
            Debug.LogWarning("Image bileşeni veya yeni Sprite atanmadı!");
        }
    }

}
