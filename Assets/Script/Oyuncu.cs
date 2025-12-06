using JetBrains.Annotations;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    [SerializeField] GameObject GameoverScreen;
    public int CanSayisi;
    private int AltinMiktari;
    public int Skor;
    private float SkorCarpani;
    public Image image;
    public TextMeshProUGUI SkorTxt;
    public static Oyuncu instance;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        NewGame();
    }
    public void NewGame()
    {
        SkorReset();
        for (int i = 0; i < 3; i++)
        {
            CanEkle();
        }
    }
    public void SkorReset()
    {
        Skor = 0;
        SkorYazdir();
    }

    public void CanAzalt(Node x)
    {

        if (CanSayisi == 1)
        {
            GameoverScreen.GetComponent < Image>().sprite = x.secilenArkaPlan;
            Game.instance.Bekle(1f, Game.instance.GameoverCanvasAc);
            Game.instance.GameOver();
        }
        CanSistemi.instance.CanAzalt(CanSayisi - 1);
        --CanSayisi;
    }

    public void CanEkle()
    {
        if (CanSayisi < 3)
        {
            CanSistemi.instance.CanEkle(CanSayisi);
            ++CanSayisi;
        }
        else
        {
            Debug.Log("maksCan");
        }

    }


    public void SkorEkle()
    {
        ++Skor;
        SkorYazdir();
        LevelManager.instance.LevelUp();

    }
    public void SkorYazdir()
    {
        SkorTxt.text = Skor.ToString();
    }



}
