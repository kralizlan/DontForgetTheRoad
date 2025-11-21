using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource TiklamaSesi;
    [SerializeField] private AudioSource YanlisTiklamaSesi;
    [SerializeField] private AudioSource Muzik;

    
    public static AudioManager instance;
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
    }

    public void TiklamaSesiCal()
    {
        TiklamaSesi.Play();
    }

    public void YanlisTiklamaSesiCal()
    {
        YanlisTiklamaSesi.Play();

    }
    private void MuzikAc()
    {
        Muzik.volume = 0.1f;
    }
    private void MuzikKapat()
    {
        Muzik.volume = 0f;
    }
    private void TiklamaSesiAc()
    {
        TiklamaSesi.volume = 0.4f;
        YanlisTiklamaSesi.volume = 0.4f;
    }
    private void TiklamaSesiKapat()
    {
        YanlisTiklamaSesi.volume = 0f;
        TiklamaSesi.volume = 0f;
    }
    public void TiklamaSesiDegis()
    {
        if (TiklamaSesi.volume == 0f)
        {
            TiklamaSesiAc();
            AyarlarImageSc.instance.EfektSesiAc();
        }
        else
        {
            TiklamaSesiKapat();
            AyarlarImageSc.instance.EfektSesiKapat();
        }
    }
    public void MuzikSesiDegis()
    {
        if (Muzik.volume == 0f)
        {
            MuzikAc();
            AyarlarImageSc.instance.MuzikSesiAcik();

        }
        else
        {
            MuzikKapat();
            AyarlarImageSc.instance.MuzikSesiKapali();

        }
    }


}
