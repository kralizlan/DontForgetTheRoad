using UnityEngine;
using UnityEngine.SceneManagement;

public class AnaMenuCanvas : MonoBehaviour
{
    public GameObject AnaMenuCv;
    public Canvas SettingsCanvas;
    public Canvas BolumlerCanvas;


    public void SettingBtn()
    {
        AnaMenuCv.gameObject.SetActive(false);
        SettingsCanvas.gameObject.SetActive(true);
        AudioManager.instance.TiklamaSesiCal();
    }

    public void StartBtn()
    {
        AnaMenuCv.gameObject.SetActive(false);
        BolumlerCanvas.gameObject.SetActive(true);
    }

    public void SettingsBackBtn()
    {
        SettingsCanvas.gameObject.SetActive(false);
        AnaMenuCv.gameObject.SetActive(true);
    }

    public void BolumlerBackBtn()
    {
        BolumlerCanvas.gameObject.SetActive(false);
        AnaMenuCv.gameObject.SetActive(true);
    }

    public void MuzikBtn()
    {
        AudioManager.instance.MuzikSesiDegis();
    }

    public void EfecktBtn()
    {
        AudioManager.instance.TiklamaSesiDegis();
    }



    #region Btn Komutlari
    public void Btn1()
    {

        PlayerPrefs.SetInt("SelectedLevel", 1); // Örneğin, 3. bölümü kaydet
        PlayerPrefs.Save();
        SceneManager.LoadScene(1); // Hedef sahneye git
    }

    public void Btn2()
    {

        if (LevelManager.instance.Level >= 2)
        {
            PlayerPrefs.SetInt("SelectedLevel", 2); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }

    }
    public void Btn3()
    {
        if (LevelManager.instance.Level >= 3)
        {
            PlayerPrefs.SetInt("SelectedLevel", 3); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    public void Btn4()
    {
        if (LevelManager.instance.Level >= 4)
        {
            PlayerPrefs.SetInt("SelectedLevel", 4); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    public void Btn5()
    {
        if (LevelManager.instance.Level >= 5)
        {
            PlayerPrefs.SetInt("SelectedLevel", 5); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    public void Btn6()
    {
        if (LevelManager.instance.Level >= 6)
        {
            PlayerPrefs.SetInt("SelectedLevel", 6); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    public void Btn7()
    {
        if (LevelManager.instance.Level >= 7)
        {
            PlayerPrefs.SetInt("SelectedLevel", 7); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    public void Btn8()
    {
        if (LevelManager.instance.Level >= 8)
        {
            PlayerPrefs.SetInt("SelectedLevel", 8); // Örneğin, 3. bölümü kaydet
            PlayerPrefs.Save();
            SceneManager.LoadScene(1); // Hedef sahneye git
        }
    }
    #endregion

}
