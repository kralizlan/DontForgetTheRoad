using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayout : MonoBehaviour
{
    public GridLayoutGroup gridLayout; // GridLayoutGroup bileşeni
    public RectTransform panelRect; // Panelin RectTransform bileşeni

     int rowCount ; // Varsayılan satır sayısı
     int columnCount ; // Varsayılan sütun sayısı

    public static DynamicGridLayout instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateGridSize()
    {
        if (gridLayout == null || panelRect == null)
        {
            Debug.LogError("GridLayout veya Panel atanmadı!");
            return;
        }

        // Panelin genişliği ve yüksekliği
        float panelWidth = panelRect.rect.width;
        float panelHeight = panelRect.rect.height;

        // Spacing oranı (Hücre boyutuna göre %10 boşluk)
        float spacingFactor = 0.1f; // %10 boşluk bırak

        // Spacing hariç hücre genişliği ve yüksekliği hesapla
        float cellWidth = (panelWidth - ((columnCount - 1) * gridLayout.spacing.x)) / columnCount;
        float cellHeight = (panelHeight - ((rowCount - 1) * gridLayout.spacing.y)) / rowCount;

        // Hücre oranlarını kontrol et
        LimitCellRatio(ref cellWidth, ref cellHeight);

//      gridLayout.spacing = new Vector2(cellWidth * spacingFactor, cellHeight * spacingFactor);
        gridLayout.spacing = Vector2.zero;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }


    public void SetGridSize(int newRows, int newColumns)
    {
        rowCount = newRows;
        columnCount = newColumns;
        UpdateGridSize();
    }

    private void LimitCellRatio(ref float width, ref float height)
    {
        // Eğer genişlik, yüksekliğin 2 katından büyükse
        if (width > height * 2)
        {
            width = height * 2; // Genişliği sınırla
        }
        // Eğer yükseklik, genişliğin 2 katından büyükse
        else if (height > width * 2)
        {
            height = width * 2; // Yüksekliği sınırla
        }
    }
}
