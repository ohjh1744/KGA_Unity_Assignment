using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MyPathFinder : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;

    [SerializeField] List<Vector2Int> path;


    private void Start()
    {
        Vector2Int start = new Vector2Int((int)startPos.position.x, (int)startPos.position.y);
        Vector2Int end = new Vector2Int((int)endPos.position.x, (int)endPos.position.y);

        bool success = AStar(start, end, out path);
        if (success)
        {
            Debug.Log("��� Ž�� ����!");
        }
        else
        {
            Debug.Log("��� Ž�� ����!");
        }
    }

    private void Update()
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector3 from = new Vector3(path[i].x, path[i].y, 0);
            Vector3 to = new Vector3(path[i + 1].x, path[i + 1].y, 0);
            Debug.DrawLine(from, to);
        }
    }

    static Vector2Int[] direction =
    {
        new Vector2Int( 0, +1), // ��
        new Vector2Int( 0, -1), // ��
        new Vector2Int(-1,  0), // ��
        new Vector2Int(+1,  0), // ��
        new Vector2Int(+1, +1), // ���
        new Vector2Int(+1, -1), // ����
        new Vector2Int(-1, +1), // �»�
        new Vector2Int(-1, -1), // ����
    };

    public static bool AStar(Vector2Int start, Vector2Int end, out List<Vector2Int> path)
    {
        // 0. ���� ����
        List<ASNode> openList = new List<ASNode>();     // Ž���� ���� �ĺ����� ����
        Dictionary<Vector2Int, bool> closeSet = new Dictionary<Vector2Int, bool>();    // Ž���� �Ϸ��� �������� ����
        path = new List<Vector2Int>();  // ��ε��� ������ ����Ʈ

        // ó������ Ž���� ������ openList�� �߰�
        openList.Add(new ASNode(start, null, 0, Heuristic(start, end)));

        while (openList.Count > 0)  // Ž���� ������ ���� ������ �ݺ�
        {
            // 1. �������� Ž���� ���� �����ϱ� (F�� ���� ����, F�� ���ٸ� H�� ���� ���� ����)
            ASNode nextNode = NextNode(openList);

            // 2. ������ ������ Ž���ߴٰ� ǥ��
            openList.Remove(nextNode);          // �������� Ž���� ������ �ĺ��� �߿��� ����
            closeSet.Add(nextNode.pos, true);   // Ž���� �Ϸ��� �����鿡 �߰�

            // 3. �������� Ž���� ������ �������� ��� (��� Ž���� ���� => path ��ȯ�ϸ鼭 ����)
            if (nextNode.pos == end)
            {
                ASNode current = nextNode;
                while (current != null)
                {
                    path.Add(current.pos);
                    current = current.parent;
                }
                path.Reverse();
                return true;
            }

            // 4. �ֺ� �������� ������ ���
            for (int i = 0; i < direction.Length; i++)      // ���⿡ ���� �ݺ�
            {
                // ������ �־��� ��ġ
                Vector2Int pos = nextNode.pos + direction[i];

                // Ž���ϸ� �ȵǴ� ��� ����
                // 4-1. �̹� Ž���� �����̸�
                if (closeSet.ContainsKey(pos))
                    continue;

                // 4-2. ������ ������ ���
                // tilemap.HasTile : Ÿ�ϸ��� �м��ϰų�,
                // Physics.Overlap : �浹ü ���翩�θ� Ȯ���ϰų�,
                // Physics.Raycast : �߰��� ��ֹ��� ���ų�)
                if (Physics2D.OverlapCircle(pos, 0.4f) != null)
                    continue;

                // 4-a. �밢�� �߿����� �Ѵ� ���� ���
                if (i >= 4 &&
                    Physics2D.OverlapCircle(new Vector2(pos.x, nextNode.pos.y), 0.4f) != null &&
                    Physics2D.OverlapCircle(new Vector2(nextNode.pos.x, pos.y), 0.4f) != null)
                    continue;

                // 4-b. �밢�� �߿����� �Ѵ� �շ� �־������ ���
                if (i >= 4 &&
                    (Physics2D.OverlapCircle(new Vector2(pos.x, nextNode.pos.y), 0.4f) != null ||
                    Physics2D.OverlapCircle(new Vector2(nextNode.pos.x, pos.y), 0.4f) != null))
                    continue;

                // 4-3. ���� ���
                int g;
                // �������� ������ ���
                if (pos.x == nextNode.pos.x ||
                    pos.y == nextNode.pos.y)
                {
                    g = nextNode.g + CostStraight;
                }
                // �밢������ ������ ���
                else
                {
                    g = nextNode.g + CostDiagonal;
                }
                int h = Heuristic(pos, end);
                int f = g + h;

                // 4-4. ������ ���� ������ �ʿ��� ���
                ASNode findNode = FindNode(openList, pos);
                // ������ ������ ���
                if (findNode == null)
                {
                    openList.Add(new ASNode(pos, nextNode, g, h));
                }
                // f �� �� �۾����ų�
                else if (findNode.f > f)
                {
                    findNode.f = f;
                    findNode.g = g;
                    findNode.h = h;
                    findNode.parent = nextNode;
                }
            }
        }

        path = null;
        return false;
    }

    public const int CostStraight = 10;
    public const int CostDiagonal = 14;
    // �޸���ƽ (Heuristic) : �ֻ��� ��θ� �����ϴ� ������, �޸���ƽ�� ���� ���Ž�� ȿ���� ������
    public static int Heuristic(Vector2Int start, Vector2Int end)
    {
        int xSize = Mathf.Abs(start.x - end.x);
        int ySize = Mathf.Abs(start.y - end.y);

        // 3�� �ϳ� �˸´� �� ��� ���. ����� Ÿ�ϸ��� �̿��ϹǷ� Ÿ�ϸ� �Ÿ� ���ϴ¹�� ����.
        // ����ư �Ÿ� : ������ ���� �̵��ϴ� �Ÿ�
        // return xSize + ySize;

        // ��Ŭ���� �Ÿ� : �밢���� ���� �̵��ϴ� �Ÿ�
        // return (int)Vector2Int.Distance(start, end);

        // Ÿ�ϸ� �Ÿ�: ������ �밢���� ���� �̵��ϴ� �Ÿ�
        int straightCount = Mathf.Abs(xSize - ySize);
        int diagonalCount = Mathf.Max(xSize, ySize) - straightCount;
        return CostStraight * straightCount + CostDiagonal * diagonalCount;
    }

    public static ASNode NextNode(List<ASNode> openList)
    {
        // F�� ���� ����, F�� ���ٸ� H�� ���� ���� ����
        int curF = int.MaxValue;
        int curH = int.MaxValue;
        ASNode minNode = null;

        for (int i = 0; i < openList.Count; i++)
        {
            if (curF > openList[i].f)
            {
                curF = openList[i].f;
                curH = openList[i].h;
                minNode = openList[i];
            }
            else if (curF == openList[i].f &&
                curH > openList[i].h)
            {
                curF = openList[i].f;
                curH = openList[i].h;
                minNode = openList[i];
            }
        }

        return minNode;
    }

    public static ASNode FindNode(List<ASNode> openList, Vector2Int pos)
    {
        for (int i = 0; i < openList.Count; i++)
        {
            if (openList[i].pos == pos)
            {
                return openList[i];
            }
        }

        return null;
    }

}

//public class ASNode
//{
//    public Vector2Int pos;  // ���� ���� ��ġ
//    public ASNode parent;   // �� ������ Ž���� ����

//    public int f;           // ���� ���� �Ÿ� => f = g + h
//    public int g;           // �ɸ� �Ÿ�
//    public int h;           // ���� ���� �Ÿ�

//    public ASNode(Vector2Int pos, ASNode parent, int g, int h)
//    {
//        this.pos = pos;
//        this.parent = parent;
//        this.f = g + h;
//        this.g = g;
//        this.h = h;
//    }
//}
