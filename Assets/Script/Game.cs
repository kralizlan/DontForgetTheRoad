using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    public bool isStarting;
    public bool gameOver = false;
    public int difficulty;
    public bool isPause;
    public GameObject GameOverCanvas;
    public GameObject LevelUpBildirisi;

    private void Awake()
    {
        instance = this;
        difficulty = PlayerPrefs.GetInt("SelectedLevel");

    }
    private void Start()
    {
        StartGame();
        Grid.instance.CreatePath();
    }

    public void Bekle(float sure, System.Action tamamlaninca)
    {
        StartCoroutine(BeklemeRoutine(sure, tamamlaninca));
    }

    IEnumerator BeklemeRoutine(float sure, System.Action tamamlaninca)
    {
        yield return new WaitForSeconds(sure);
        tamamlaninca?.Invoke();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
        AdManager.Instance.ShowBanner();
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPause = false;
        AdManager.Instance.HideBanner();
    }

    public void StartGame()
    {
        AdManager.Instance.HideBanner();

        switch (difficulty)
        {

            case 1:
                Grid.instance.SetRowAndColumn(3, 6);
                Grid.instance.CreateImageGrid();
                NewGame();
                break;
            case 2:
                Grid.instance.SetRowAndColumn(4, 8);
                Grid.instance.CreateImageGrid();
                NewGame();

                break;
            case 3:
                Grid.instance.SetRowAndColumn(5, 7);
                Grid.instance.CreateImageGrid();
                NewGame();

                break;
            case 4:
                Grid.instance.SetRowAndColumn(6, 9);
                Grid.instance.CreateImageGrid();
                NewGame();

                break;
            case 5:
                Grid.instance.SetRowAndColumn(7, 10);
                Grid.instance.CreateImageGrid();
                NewGame();
                break;
            case 6:
                Grid.instance.SetRowAndColumn(7, 11);
                Grid.instance.CreateImageGrid();
                NewGame();
                break;
            case 7:
                Grid.instance.SetRowAndColumn(8, 12);
                Grid.instance.CreateImageGrid();
                NewGame();
                break;
            case 8:
                Grid.instance.SetRowAndColumn(8, 13);
                Grid.instance.CreateImageGrid();
                NewGame();
                break;
            default:
                break;
        }

    }

    public void NewGame()
    {
        AdManager.Instance.HideBanner();
        Time.timeScale = 1;
        TimerDisplay.instance.NewGame();
        Grid.instance.CreatePath();
        Answer.instance.NewGame();
    }



    public void GameOver()
    {
        AdManager.Instance.ShowBanner();
        isStarting = false;
        gameOver = true;
        Path.instance.ShowPath(Grid.instance);
        Bekle(1f, GameoverCanvasAc);
    }
    public void GameoverCanvasAc()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReklamliYenidenDevamEt()
    {
        isStarting = true;
        GameOverCanvas.gameObject.SetActive(false);
        //    AdManager.instance.HideBannerAd();
        Oyuncu.instance.CanEkle();
        TimerDisplay.instance.NewGame();
        Path.instance.CevaplarPath(Answer.instance.DogruCevaplar);
    }

}
