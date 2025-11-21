using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanSistemi : MonoBehaviour
{

    public List<Image> Canlar;
    public Sprite Canli;
    public Sprite Cansiz;


    public static CanSistemi instance;
    private void Awake()
    {
        instance = this;
    }
    public void CanEkle(int can)
    {
        for (int i = 0; i < Canlar.Count; i++)
        {
            if (can == i)
            {
                Canlar[i].sprite = Canli;

            }
        }
    }

    public void CanAzalt(int can)
    {
        for (int i = 0; i < Canlar.Count; i++)
        {
            if (can==i)
            {
             //   Debug.Log("cikardim");
                Canlar[i].sprite = Cansiz;
            }
        }
    }



}
