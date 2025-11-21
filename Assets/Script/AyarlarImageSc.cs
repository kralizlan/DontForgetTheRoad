using UnityEngine;
using UnityEngine.UI;

public class AyarlarImageSc : MonoBehaviour
{
    public static AyarlarImageSc instance;

    [SerializeField] private Sprite MuzinAcik;
    [SerializeField] private Sprite MuzinKapali;
    [SerializeField] private Image Muzik;

    [SerializeField] private Sprite EfektAcik;
    [SerializeField] private Sprite EfektKapali;
    [SerializeField] private Image Efekt;

    private void Awake()
    {
        Efekt.sprite = EfektAcik;
        Muzik.sprite = MuzinAcik;
        instance = this;
    }
    public void MuzikSesiAcik()
    {
        Muzik.sprite = MuzinAcik;
    }
    public void MuzikSesiKapali()
    {
        Muzik.sprite = MuzinKapali;
    }
    public void EfektSesiAc()
    {
        Efekt.sprite = EfektAcik;
    }
    public void EfektSesiKapat()
    {
        Efekt.sprite = EfektKapali;
    }
}
