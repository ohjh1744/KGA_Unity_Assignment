//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//public class PathFinder : MonoBehaviour
//{
//    [SerializeField] Transform startPos;
//    [SerializeField] Transform endPos;

//    [SerializeField] List<Vector2Int> path;
//    [SerializeField] float time;
//    List<ASNode> openList;
//    Dictionary<Vector2Int, bool> closeSet;

//    private bool success;
//    private Coroutine coroutine;
//    private WaitForSeconds seconds;

//    private void Start()
//    {
//        seconds = new WaitForSeconds(time);
//        Vector2Int start = new Vector2Int((int)startPos.position.x, (int)startPos.position.y);
//        Vector2Int end = new Vector2Int((int)endPos.position.x, (int)endPos.position.y);

//        coroutine = StartCoroutine(AStar(start, end));
//        if (success)
//        {
//            Debug.Log("경로 탐색 성공!");
//        }
//        else
//        {
//            Debug.Log("경로 탐색 실패!");
//        }
//    }

//    private void Update()
//    {
//        for (int i = 0; i < path.Count - 1; i++)
//        {
//            Vector3 from = new Vector3(path[i].x, path[i].y, 0);
//            Vector3 to = new Vector3(path[i + 1].x, path[i + 1].y, 0);
//            Debug.DrawLine(from, to);
//        }
//    }

//    static Vector2Int[] direction =
//    {
//        new Vector2Int( 0, +1), // 상
//        new Vector2Int( 0, -1), // 하
//        new Vector2Int(-1,  0), // 좌
//        new Vector2Int(+1,  0), // 우
//        new Vector2Int(+1, +1), // 우상
//        new Vector2Int(+1, -1), // 우하
//        new Vector2Int(-1, +1), // 좌상
//        new Vector2Int(-1, -1), // 좌하
//    };

//    IEnumerator AStar(Vector2Int start, Vector2Int end)
//    {
//        // 0. 사전 세팅
//        openList = new List<ASNode>();     // 탐색할 정점 후보들을 보관
//        closeSet = new Dictionary<Vector2Int, bool>();    // 탐색을 완료한 정점들을 보관
//        path = new List<Vector2Int>();  // 경로들을 보관할 리스트

//        // 처음으로 탐색할 정점을 openList에 추가
//        openList.Add(new ASNode(start, null, 0, Heuristic(start, end)));

//        while (openList.Count > 0)  // 탐색할 정점이 없을 때까지 반복
//        {
//            // 1. 다음으로 탐색할 정점 선택하기 (F가 가장 낮은, F가 같다면 H가 가장 낮은 선택)
//            ASNode nextNode = NextNode(openList);

//            // 2. 선택한 정점을 탐색했다고 표시
//            openList.Remove(nextNode);          // 다음으로 탐색할 정점을 후보들 중에서 제거
//            closeSet.Add(nextNode.pos, true);   // 탐색을 완료한 정점들에 추가

//            // 3. 다음으로 탐색할 정점이 도착지인 경우 (경로 탐색을 성공 => path 반환하면서 종료)
//            if (nextNode.pos == end)
//            {
//                ASNode current = nextNode;
//                while (current != null)
//                {
//                    path.Add(current.pos);
//                    current = current.parent;
//                }
//                path.Reverse();
//                success = true;
//                StopCoroutine(coroutine);
//            }

//            // 4. 주변 정점들의 점수를 계산
//            for (int i = 0; i < direction.Length; i++)      // 방향에 대한 반복
//            {
//                // 점수를 넣어줄 위치
//                Vector2Int pos = nextNode.pos + direction[i];

//                // 탐색하면 안되는 경우 제외
//                // 4-1. 이미 탐색한 정점이면
//                if (closeSet.ContainsKey(pos))
//                    continue;

//                // 4-2. 못가는 지형일 경우
//                // tilemap.HasTile : 타일맵을 분석하거나,
//                // Physics.Overlap : 충돌체 존재여부를 확인하거나,
//                // Physics.Raycast : 중간에 장애물이 없거나)
//                if (Physics2D.OverlapCircle(pos, 0.4f) != null)
//                    continue;

//                // 4-a. 대각선 중에서도 둘다 막힌 경우
//                if (i >= 4 &&
//                    Physics2D.OverlapCircle(new Vector2(pos.x, nextNode.pos.y), 0.4f) != null &&
//                    Physics2D.OverlapCircle(new Vector2(nextNode.pos.x, pos.y), 0.4f) != null)
//                    continue;

//                // 4-b. 대각선 중에서도 둘다 뚫려 있어야지만 허용
//                if (i >= 4 &&
//                    (Physics2D.OverlapCircle(new Vector2(pos.x, nextNode.pos.y), 0.4f) != null ||
//                    Physics2D.OverlapCircle(new Vector2(nextNode.pos.x, pos.y), 0.4f) != null))
//                    continue;

//                // 4-3. 점수 계산
//                int g;
//                // 직선으로 움직인 경우
//                if (pos.x == nextNode.pos.x ||
//                    pos.y == nextNode.pos.y)
//                {
//                    g = nextNode.g + CostStraight;
//                }
//                // 대각선으로 움직인 경우
//                else
//                {
//                    g = nextNode.g + CostDiagonal;
//                }
//                int h = Heuristic(pos, end);
//                int f = g + h;

//                // 4-4. 정점의 점수 갱신이 필요한 경우
//                ASNode findNode = FindNode(openList, pos);
//                // 점수가 없었던 경우
//                if (findNode == null)
//                {
//                    openList.Add(new ASNode(pos, nextNode, g, h));
//                }
//                // f 가 더 작아지거나
//                else if (findNode.f > f)
//                {
//                    findNode.f = f;
//                    findNode.g = g;
//                    findNode.h = h;
//                    findNode.parent = nextNode;
//                }
//            }

//            yield return seconds;
//        }

//        path = null;
//        success = false;
//    }

//    public const int CostStraight = 10;
//    public const int CostDiagonal = 14;
//    // 휴리스틱 (Heuristic) : 최상의 경로를 추정하는 순위값, 휴리스틱에 의해 경로탐색 효율이 결정됨
//    public  int Heuristic(Vector2Int start, Vector2Int end)
//    {
//        int xSize = Mathf.Abs(start.x - end.x);
//        int ySize = Mathf.Abs(start.y - end.y);

//        // 3중 하나 알맞는 거 골라서 사용. 현재는 타일맵을 이용하므로 타일맵 거리 구하는방식 적용.
//        // 맨해튼 거리 : 직선을 통해 이동하는 거리
//        // return xSize + ySize;

//        // 유클리드 거리 : 대각선을 통해 이동하는 거리
//        // return (int)Vector2Int.Distance(start, end);

//        // 타일맵 거리: 직선과 대각선을 통해 이동하는 거리
//        int straightCount = Mathf.Abs(xSize - ySize);
//        int diagonalCount = Mathf.Max(xSize, ySize) - straightCount;
//        return CostStraight * straightCount + CostDiagonal * diagonalCount;
//    }

//    public  ASNode NextNode(List<ASNode> openList)
//    {
//        // F가 가장 낮은, F가 같다면 H가 가장 낮은 선택
//        int curF = int.MaxValue;
//        int curH = int.MaxValue;
//        ASNode minNode = null;

//        for (int i = 0; i < openList.Count; i++)
//        {
//            if (curF > openList[i].f)
//            {
//                curF = openList[i].f;
//                curH = openList[i].h;
//                minNode = openList[i];
//            }
//            else if (curF == openList[i].f &&
//                curH > openList[i].h)
//            {
//                curF = openList[i].f;
//                curH = openList[i].h;
//                minNode = openList[i];
//            }
//        }

//        return minNode;
//    }

//    public  ASNode FindNode(List<ASNode> openList, Vector2Int pos)
//    {
//        for (int i = 0; i < openList.Count; i++)
//        {
//            if (openList[i].pos == pos)
//            {
//                return openList[i];
//            }
//        }

//        return null;
//    }

//    private void OnDrawGizmos()
//    {
//        foreach(ASNode node in openList)
//        {
//            Gizmos.color = Color.green;
//            Gizmos.DrawWireCube(new Vector3(node.pos.x, node.pos.y, 0), new Vector3(1, 1, 0));
//        }


//        foreach (Vector2Int pos in closeSet.Keys)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawWireCube(new Vector3(pos.x, pos.y, 0), new Vector3(1, 1, 0));
//        }
//    }
//}

//public class ASNode
//{
//    public Vector2Int pos;  // 현재 정점 위치
//    public ASNode parent;   // 이 정점을 탐색한 정점

//    public int f;           // 예상 최종 거리 => f = g + h
//    public int g;           // 걸린 거리
//    public int h;           // 예상 남은 거리

//    public ASNode(Vector2Int pos, ASNode parent, int g, int h)
//    {
//        this.pos = pos;
//        this.parent = parent;
//        this.f = g + h;
//        this.g = g;
//        this.h = h;
//    }
//}
