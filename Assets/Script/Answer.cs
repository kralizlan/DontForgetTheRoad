using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Answer : MonoBehaviour
{
    private List<Node> Cevaplar = new List<Node>();
    public List<Node> DogruCevaplar = new List<Node>();

    bool cevapDogruMu;
    int CevapSayisi;
    public static Answer instance;
    private void Awake()
    {
        CevapSayisi = 0;
        cevapDogruMu = false;
        instance = this;
    }

    public void NewGame()
    {
        ResetCevaplar();
        CevapSayisi = 0;
        cevapDogruMu = false;
    }

    public List<Node> GetCevaplar()
    {
        return Cevaplar;
    }

    public void CevapEkle(Node node)
    {
        CheckCevap(node);
        Cevaplar.Add(node);
    }

    public void ResetCevaplar()
    {
        Cevaplar.Clear();
        DogruCevaplar.Clear();
    }

    //path[0].AcikYol(); duzelt Path.HidePath
    //public void CheckCevap(Node node)  
    //{


    //    if (Path.instance.path.Count == CevapSayisi)
    //    {
    //        return;
    //    }

    //    if (Path.instance.path[CevapSayisi] == node)
    //    {
    //        AudioManager.instance.TiklamaSesiCal();
    //        DogruCevaplar.Add(node);
    //        ++CevapSayisi;
    //        cevapDogruMu = true;
    //        ComboBar.instance.AddCombo(0.15f);
    //        Oyuncu.instance.SkorEkle();
    //        node.AcikYol();
    //        if (Path.instance.path.Count == CevapSayisi)
    //        {
    //            Game.instance.NewGame();
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        if(CevapSayisi == 0)return;
    //        foreach (var item in Cevaplar)
    //        {
    //            if (item == node)
    //            {
    //                return;
    //            }
    //        }
    //        node.RastgeleSpriteAta();
    //        AudioManager.instance.YanlisTiklamaSesiCal();
    //        Oyuncu.instance.CanAzalt();
    //        StartCoroutine(AyniBolumuYenidenBaslat());
    //    }
    //} //eski

    public void CheckCevap(Node node) //yeni
    {

        if (Path.instance.path.Count == CevapSayisi)
        {
            return;
        }
        foreach (var item in DogruCevaplar)
        {
            if (node==item)
            {
                return;
            }
        }
        cevapDogruMu = false;
        foreach (var item in Path.instance.path)
        {
            if (item == node)
            {
                cevapDogruMu = true;
            }
        }
        if (cevapDogruMu)
        {

            AudioManager.instance.TiklamaSesiCal();
            DogruCevaplar.Add(node);
            ++CevapSayisi;
            cevapDogruMu = true;
            ComboBar.instance.AddCombo(0.15f);
            node.AcikYol();
            if (Path.instance.path.Count == CevapSayisi)
            {
                Oyuncu.instance.SkorEkle();
                Game.instance.NewGame();
                return;
            }
        }
        else
        {
            if (CevapSayisi == 0) return;
            foreach (var item in Cevaplar)
            {
                if (item == node)
                {
                    return;
                }
            }
            node.RastgeleSpriteAta();
            AudioManager.instance.YanlisTiklamaSesiCal();
            Oyuncu.instance.CanAzalt(node);
            StartCoroutine(AyniBolumuYenidenBaslat());
        }
    }

    private IEnumerator AyniBolumuYenidenBaslat()
    {
        if (Oyuncu.instance.CanSayisi == 0) yield break;
        Game.instance.isStarting = false;
        yield return new WaitForSeconds(1f);
        Game.instance.isStarting = true;
        TimerDisplay.instance.NewGame();
        NewGame();
        Path.instance.ShowPath(Grid.instance);
        foreach (var item in Grid.instance.nodes)
        {
            item.AlphaBekleVeAc();
        }
    }



}
