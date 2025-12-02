using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public RectTransform panel;  // Panelin boyutunu almak için
    public int Row=1;
    public int Column=1;
    public GameObject NodePrefab;
    public GameObject NodePrefabImage;

    public float nodeSpacing;

    GridLayoutGroup gridLayout;
    public Node[,] nodes;
    public static Grid instance;
    private void Awake()
    {
        instance = this;
        gridLayout = panel.GetComponent<GridLayoutGroup>();
        //CreateImageGrid();
    }
    public void CreatePath()
    {

        if (Path.instance != null)
        {
            Path.instance.RandomPath(this);
         
        }
        else
        {
            Debug.LogError("Path.instance bulunamadı! Path nesnesi sahnede mi?");
        }
    }

    //sol ustten baslar sag alta dogru olusturur

    public void SetRowAndColumn(int column, int row)
    {
        Row = row;
        Column = column;

    }

    public void CreateImageGrid()
    {
        if (gridLayout != null)
        {
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            gridLayout.constraintCount = Column;
            gridLayout.spacing = new Vector2(nodeSpacing, nodeSpacing);
        }
        nodes = new Node[Row, Column];
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {

                GameObject newImage = Instantiate(NodePrefabImage, panel);
                newImage.name = "Image_" + i + "_" + j;
                nodes[i, j] = newImage.GetComponent<Node>();
                nodes[i, j].x = i;
                nodes[i, j].y = j;
            }
        }
        DynamicGridLayout.instance.SetGridSize(Row, Column);

    }



    public void CreateGrid()
    {
        //nodes = new Node[Row, Column];
        //for (int i = 0; i < Row; i++)
        //{
        //    for (int j = 0; j < Column; j++)
        //    {
        //        float xPos = j * (NodePrefab.transform.localScale.x + nodeSpacing);
        //        float zPos = -i * (NodePrefab.transform.localScale.z + nodeSpacing);

        //        Vector3 spawnPosition = startPosition + new Vector3(xPos, 0, zPos);
        //        GameObject newNode = Instantiate(NodePrefab, spawnPosition, Quaternion.identity);

        //        nodes[i, j] = newNode.GetComponent<Node>();
        //        nodes[i, j].x = i;
        //        nodes[i, j].y = j;
        //    }
        //}

        //CameraPosition.instance.ChangeCameraPosition(CameraPositionVector3());

    }




}
