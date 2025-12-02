using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public static TimerDisplay instance;
    public TextMeshProUGUI timerText;  // UI TextMeshPro nesnesi
    private float elapsedTime = 4f;    // Geçen süre
    public bool SayacAzalsinMi;
    private void Awake()
    {

        instance = this;
        SayacAzalsinMi = true;

    }

    void Update()
    {
        SureAyarla();
    }
    private void SureAyarla()
    {
        if (!Game.instance.isPause)
        {

            if (SayacAzalsinMi)
            {
                SureyiAzalt();
            }
            else
            {
                SureyiArtir();
            }
        }
    }

    public void SureyiArtir()
    {
        elapsedTime += Time.deltaTime; // Süreyi artır

        SureyiYazdir(elapsedTime);

    }
    public void SureyiAzalt()
    {
        elapsedTime -= Time.deltaTime; // Süreyi azalt
        Path.instance.HidePath(Grid.instance);
        Path.instance.ShowPath(Grid.instance);
        Path.instance.CevaplarPath(Answer.instance.DogruCevaplar);
        SureyiYazdir(elapsedTime);
        if (elapsedTime < 0.2)
        {
            BolumBaslat();
        }
    }

    public void BolumBaslat()
    {
        Game.instance.isStarting = true;
        SayacAzalsinMi = false;
        Path.instance.HidePath(Grid.instance, true);

        Path.instance.CevaplarPath(Answer.instance.DogruCevaplar);
        elapsedTime = 0;
    }
    public void SureyiYazdir(float sure)
    {
        int minutes = Mathf.FloorToInt(sure / 60); // Dakika hesapla
        int seconds = Mathf.FloorToInt(sure % 60); // Saniye hesapla
        timerText.text = $" {minutes:00}:{seconds:00}"; // TextMeshPro'ya yazdır
    }
    public void NewGame()
    {
        Game.instance.isStarting = false;
        SayacAzalsinMi = true;
        elapsedTime = 4f;
    }

}
