using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Node : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler
{
    public int y { get; set; }
    public int x { get; set; }

    private static bool isTouchHeld = false;

    public List<Node> neighbors = new List<Node>();

    private Image uiImage;

    [SerializeField] private List<Sprite> spriteListe = new List<Sprite>();
    [SerializeField] Sprite AcikYolSprite;
    private Sprite orijinalSprite;

    private void Awake()
    {
        uiImage = GetComponent<Image>();
        if (uiImage != null)
            orijinalSprite = uiImage.sprite;
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
        else
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

    public void ChangeColor(Color newColor)
    {
        if (uiImage != null)
        {
           uiImage.color = newColor;
        }
        else
        {
            Debug.LogError("Image bileşeni bulunamadı!");
        }
    }

    #region Resimler Yol Gizleme

    private bool zatenSecildi = false;
    private Sprite rastgeleSecilenSprite;

    public void RastgeleSpriteAta()
    {
        if (uiImage == null) return;
        if (spriteListe == null || spriteListe.Count == 0) return;
        if (zatenSecildi)
        {
            uiImage.sprite = rastgeleSecilenSprite;
            return;
        }

        rastgeleSecilenSprite = spriteListe[Random.Range(0, spriteListe.Count)];
        uiImage.sprite = rastgeleSecilenSprite;

        zatenSecildi = true;
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
    }
    #endregion
}
