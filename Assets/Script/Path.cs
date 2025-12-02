using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public void FenerOzelligi(Node merkez)
    {
        StartCoroutine(FenerRoutine(merkez, Grid.instance));
    }

    private IEnumerator FenerRoutine(Node merkez, Grid grid)
    {
        List<Node> acilanlar = new List<Node>();

        for (int ix = merkez.x - 1; ix <= merkez.x + 1; ix++)
        {
            for (int iy = merkez.y - 1; iy <= merkez.y + 1; iy++)
            {
                if (ix < 0 || iy < 0 || ix >= grid.Row || iy >= grid.Column)
                    continue;

                Node n = grid.nodes[ix, iy];

                bool pathUzerinde = path.Contains(n);
                bool cevaptaVar = Answer.instance.CevaptaVarMi(n);

                if (cevaptaVar)
                {
                    // Cevapta olanlar KALICI açık
                    n.AcikYol();
                }
                else if (pathUzerinde)
                {
                    // Path üzerinde ama cevapta değil → geçici olarak göster
                    n.AcikYol();
                    acilanlar.Add(n); // Sonra geri dönecek
                }
                else
                {
                    // Normal node → geçici rastgele sprite
                    n.RastgeleSpriteAta();
                    acilanlar.Add(n);
                }
            }
        }

        yield return new WaitForSeconds(2f);

        foreach (var n in acilanlar)
        {
            // Bu sırada oyuncu doğru cevaba tıklamış olabilir
            bool cevaptaVar = Answer.instance.CevaptaVarMi(n);

            if (cevaptaVar)
            {
                n.AcikYol();      // Artık cevapta, açık kalsın
            }
            else
            {
                n.EskiSpriteyeDon();
            }
        }

    }

    public void HidePath(Grid grid, bool bulutgecisi = false)
    {
        foreach (var item in grid.nodes)
        {
            item.EskiSpriteyeDon();
            if (bulutgecisi)
            {
                item.AlphaBekleVeAc(0, 0.5f);
            }
        }
        //    path[0].AcikYol();

    }
    public void CevaplarPath(List<Node> Nodes)
    {
        foreach (var item in Nodes)
        {
            item.AcikYol();
        }
    }

}
