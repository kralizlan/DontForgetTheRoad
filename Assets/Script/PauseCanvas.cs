using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour
{
    public GameObject Pausecanvas;
    public GameObject BolumBittiCanvas;

    public static PauseCanvas insance;

    private void Awake()
    {
        insance = this;
    }
    public void BolumBitti()
    {
        Game.instance.PauseGame();
        Path.instance.HidePath(Grid.instance);
        BolumBittiCanvas.gameObject.SetActive(true);
    }
    public void PauseGame()
    {
        Game.instance.PauseGame();
        Pausecanvas.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Game.instance.ResumeGame();
        Pausecanvas.gameObject.SetActive(false);
        Path.instance.CevaplarPath(Answer.instance.DogruCevaplar);
    }
    public void DevamEdenOyun()
    {
        BolumBittiCanvas.gameObject.SetActive(false);
        Game.instance.ResumeGame();
    }
    public void AnaMenu()
    {
        Game.instance.ResumeGame();
        Game.instance.LevelBittiMi = false;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Game.instance.ResumeGame();
        SceneManager.LoadScene(1); // Hedef sahneye git
    }
    public void CanvasSet()
    {
        // İsme göre Canvas'ı bul
        GameObject canvasObject = GameObject.Find("Canvas");
        CanvasScaler canvasScaler;

        if (canvasObject != null)
        {
            canvasScaler = canvasObject.GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = 0.9f; // Match değerini 0.9 yap

            if (canvasScaler != null)
            {
                Debug.Log("Canvas Scaler bulundu!");
            }
            else
            {
                Debug.LogWarning("Canvas Scaler bileşeni bulunamadı!");
            }
        }
        else
        {
            Debug.LogWarning("Belirtilen isimde bir Canvas bulunamadı!");
        }

    }
    public void OdulluDevamEt()
    {
        //  AdManager.instance.ShowInterstitialAd();
    }
    public void NextLevel()
    {
        Oyuncu.instance.SkorReset();
        int x = PlayerPrefs.GetInt("SelectedLevel", 0);
        ++x;
        PlayerPrefs.SetInt("SelectedLevel", x);
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
        Debug.Log("level atlama bas");
    }


}
