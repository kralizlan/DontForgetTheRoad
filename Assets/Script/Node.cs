using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class SpriteCifti
{
    public Sprite figur;      // araba, gemi, vs.
    public Sprite arkaPlan;   // o figüre özel arka plan
}

[RequireComponent(typeof(Image))]
public class Node : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    [SerializeField] private List<SpriteCifti> spriteCiftleri;
    private Sprite secilenFigur;
    [HideInInspector] public Sprite secilenArkaPlan;

    private Image uiImage;
    public int y { get; set; }
    public int x { get; set; }
    private static bool isTouchHeld = false;

    [HideInInspector] public List<Node> neighbors = new List<Node>();

    [SerializeField] Sprite AcikYolSprite;
    private Sprite orijinalSprite;
    public bool fenerMi;



    private void Awake()
    {
        uiImage = GetComponent<Image>();
        if (uiImage != null)
            orijinalSprite = uiImage.sprite;
    }
    private void Start()
    {
        AlphaBekleVeAc();
    }
    public List<Node> FindNeighbors(Node[,] grid, int rowCount, int columnCount)
    {
        if (x > 0) neighbors.Add(grid[x - 1, y]);          // Sol
        if (x < rowCount - 1) neighbors.Add(grid[x + 1, y]); // Sağ
        if (y > 0) neighbors.Add(grid[x, y - 1]);          // Aşağı
        if (y < columnCount - 1) neighbors.Add(grid[x, y + 1]); // Yukarı
        return neighbors;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        if (Game.instance.isStarting)
        {
            Answer.instance.CevapEkle(this);
            isTouchHeld = true;
        }
        else if (!Game.instance.gameOver)
        {
            TimerDisplay.instance.BolumBaslat();
            Answer.instance.CevapEkle(this);
            isTouchHeld = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouchHeld = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isTouchHeld && Game.instance.isStarting)
        {
            Answer.instance.CevapEkle(this);
        }
    }

    #region Resimler Yol Gizleme
    public void AlphaBekleVeAc(float x = 0.5f, float y = 1f)
    {
        StartCoroutine(_AlphaBekleVeAc(x, y));
    }
    private IEnumerator _AlphaBekleVeAc(float x, float y)
    {
        if (uiImage == null) yield break;

        uiImage.canvasRenderer.SetAlpha(0f); // Başta görünmez

        yield return new WaitForSeconds(x); // 1 sn bekle

        uiImage.CrossFadeAlpha(1, y, false); // 1 sn’de alpha 1 olsun
    }

    private bool zatenSecildi = false;
    public void RastgeleSpriteAta()
    {
        if (uiImage == null) uiImage = GetComponent<Image>();
        if (uiImage == null) return;
        if (spriteCiftleri == null || spriteCiftleri.Count == 0) return;

        if (zatenSecildi)
        {
            uiImage.sprite = secilenFigur;
            return;
        }

        int idx = UnityEngine.Random.Range(0, spriteCiftleri.Count);
        var cift = spriteCiftleri[idx];

        // PATH üzerindeyse Fener seçme
        bool pathUzerinde = Path.instance.path.Contains(this);

        if (pathUzerinde && cift.figur.name == "Fener")
        {
            // tekrar başka bir sprite seç
            do
            {
                idx = UnityEngine.Random.Range(0, spriteCiftleri.Count);
                cift = spriteCiftleri[idx];
            }
            while (cift.figur.name == "Fener");
        }

        secilenFigur = cift.figur;
        secilenArkaPlan = cift.arkaPlan;
        uiImage.sprite = secilenFigur;

        zatenSecildi = true;

        // FENER Mİ?
        fenerMi = (!pathUzerinde && secilenFigur.name == "Fener");

    }
    public void EskiSpriteyeDon()
    {
        if (uiImage == null) return;
        uiImage.sprite = orijinalSprite;
    }
    public void AcikYol()
    {
        if (uiImage == null) return;
        uiImage.sprite = AcikYolSprite;
        fenerMi = false; // path’te beyaz olduysa fener özelliği gitsin
    }
        #endregion

}
