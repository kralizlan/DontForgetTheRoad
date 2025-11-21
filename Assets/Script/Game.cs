using Unity.VisualScripting;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game instance;
    public bool isStarting;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Oyuncu.instance.CanEkle();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Oyuncu.instance.CanAzalt();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ComboBar.instance.AddCombo(0.15f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Oyuncu.instance.SkorEkle();
        }
        LevelManager.instance.LevelUp();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPause = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPause = false;
    }

    public void StartGame()
    {

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
        TimerDisplay.instance.NewGame();
        Grid.instance.CreatePath();
        Answer.instance.NewGame();
    }



    public void GameOver()
    {
        isStarting = false;
        GameOverCanvas.gameObject.SetActive(true);
     //   AdManager.instance.ShowBannerAd();



        //     Path.instance.ShowPath();
    }
    public void YenidenDevamEt()
    {
        isStarting = true;
        GameOverCanvas.gameObject.SetActive(false);
  //    AdManager.instance.HideBannerAd();
        Oyuncu.instance.CanEkle();
        TimerDisplay.instance.NewGame();
        Path.instance.CevaplarPath(Answer.instance.DogruCevaplar);

    }
}
