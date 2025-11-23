using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [HideInInspector] public int Level;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Sahne değişse bile yok olmasın
        }
        else
        {
            Destroy(gameObject); // Eğer ikinci bir kopya oluşursa onu yok et
        }
        if (PlayerPrefs.HasKey("Level"))
        {
            Level = PlayerPrefs.GetInt("Level");
            return;
        }
        else
        {
            PlayerPrefs.SetInt("Level", 1);
            Level = PlayerPrefs.GetInt("Level");
            PlayerPrefs.Save();
        }
    }

    public void LevelUp()
    {
        if (PlayerPrefs.GetInt("SelectedLevel", 0) == Level && Oyuncu.instance.Skor >= 1000)
        {
            Debug.Log("level atladim");
            ++Level;
            PlayerPrefs.SetInt("Level", Level);
            PlayerPrefs.Save();
            Game.instance.LevelUpBildirisi.SetActive(true);
            if (Level == 3)
            {
                Debug.Log("degerlendi ");
            }
        }
    }


}
