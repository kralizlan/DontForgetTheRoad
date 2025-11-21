using System.Collections.Generic;
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
  
    public void CheckCevap(Node node)
    {
        if (Path.instance.path.Count == CevapSayisi)
        {
            return;
        }

        if (Path.instance.path[CevapSayisi] == node)
        {
            AudioManager.instance.TiklamaSesiCal();
            DogruCevaplar.Add(node);
            ++CevapSayisi;
            cevapDogruMu = true;
            ComboBar.instance.AddCombo(0.15f);
            Oyuncu.instance.SkorEkle();
            node.AcikYol();
            if (Path.instance.path.Count == CevapSayisi)
            {
                Game.instance.NewGame();
                return;
            }
        }
        else
        {
            foreach (var item in Cevaplar)
            {
                if (item == node)
                {
                    return;
                }
            }
            node.RastgeleSpriteAta();   
            AudioManager.instance.YanlisTiklamaSesiCal();
            Oyuncu.instance.CanAzalt();
        }
    }



}
