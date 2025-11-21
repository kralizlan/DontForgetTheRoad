using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path instance;

    private void Awake()
    {
         instance = this;
    }

    Node StartNode;
    List<Node> neighbors = new List<Node>();
    public List<Node> path = new List<Node>();
    private List<Node> ClosedList = new List<Node>();

    public List<Node> RandomPath(Grid grid)
    {
        path.Clear();
        ClosedList.Clear();
        neighbors.Clear();
        
        Answer.instance.ResetCevaplar();

        int randomColumn = Random.Range(0, grid.Column);

        StartNode = grid.nodes[grid.Row - 1, randomColumn];

        path.Add(StartNode);
        ClosedList.Add(StartNode);

        Node currentNode = StartNode;

        while (true)
        {
            bool gir = true;
            // Kullanılabilir komşuları bul
            neighbors = currentNode.FindNeighbors(grid.nodes, grid.Row, grid.Column);
            List<Node> availableNeighbors = new List<Node>();


            // Eğer hedef noktaya ulaşıldıysa çık
            if (currentNode.x == 0)
            {

                return path;
            }

            foreach (Node neighbor in neighbors)
            {
                if (!ClosedList.Contains(neighbor))
                {
                    availableNeighbors.Add(neighbor);
                }
            }


            if (availableNeighbors.Count == 0)
            {
                Debug.LogWarning("Hedef bulunamadı, yol tıkandı.");
                StartNode = null;
                path.Clear();
                ClosedList.Clear();
                neighbors.Clear();
                Answer.instance.ResetCevaplar();

                randomColumn = Random.Range(0, grid.Column);
                StartNode = grid.nodes[grid.Row - 1, randomColumn];

                path.Add(StartNode);

                ClosedList.Add(StartNode);

                currentNode = StartNode;

                gir = false;
            }

            if (gir)
            {

                // Rastgele bir komşu düğüm seç
                int randomIndex = Random.Range(0, availableNeighbors.Count);
                Node nextNode = availableNeighbors[randomIndex];

                // Seçilen düğümü yola ekle ve beyaz yap
                path.Add(nextNode);
                ClosedList.Add(nextNode);

                // Kullanılmayan komşuları siyah yap ve kapalı listeye ekle
                foreach (Node neighbor in neighbors)
                {
                    if (!ClosedList.Contains(neighbor))
                    {
                        ClosedList.Add(neighbor);
                    }
                }
                availableNeighbors.Clear();
                neighbors.Clear();
                // Güncelleme işlemi
                currentNode = nextNode;
                gir = false;
            }

        }


    }




    public void ShowPath(Grid g)
    {
        foreach (var item in g.nodes)
        {
            item.RastgeleSpriteAta();
        }
        for (int i = 0; i < path.Count; i++)
        {
            path[i].AcikYol();
        }
    }

    public void HidePath(Grid grid)
    {

        foreach (var item in grid.nodes)
        {
                item.EskiSpriteyeDon();
        }

    }
    public void CevaplarPath(List<Node> Nodes)
    {
        foreach (var item in Nodes)
        {
          item.AcikYol();
        }
    }

}
