using JetBrains.Annotations;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    private int CanSayisi;
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

    public void CanAzalt()
    {

        if (CanSayisi == 1)
        {
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
        SkorCarpani = image.fillAmount + 1;
        float artir = 10 * SkorCarpani;
        Skor += Convert.ToInt32(artir);
        SkorYazdir();
    }

    public void SkorYazdir()
    {
        SkorTxt.text = Skor.ToString();
    }



}
