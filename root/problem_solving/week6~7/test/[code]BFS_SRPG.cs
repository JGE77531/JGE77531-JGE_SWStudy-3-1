using System;
using System.Collections.Generic;

class Map
{
    // 맵의 크기
    public const int MapSize = 10;

    // 맵 (0은 이동 가능, 1은 이동 불가)
    // 장애물을 두고 싶은 곳에 1을 입력
    public int[,] data = new int[MapSize, MapSize] {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
    };

    // 플레이어와 적의 위치 정보
    public int playerX;
    public int playerY;
    public int enemyX;
    public int enemyY;

    public int enemymoveCount = 0;

    // BFS 탐색을 위한 큐
    public Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

    // 방문 여부를 저장하는 배열
    public bool[,] visited = new bool[MapSize, MapSize];

    // 상, 하, 좌, 우 방향
    public readonly int[] dx = { 0, 0, -1, 1, 0 };
    public readonly int[] dy = { -1, 1, 0, 0, 0 };



    //사용자에게 플레이어, 적의 좌표를 입력 받음
    public void getEnemyPlayerPosition()
    {
        while (true)
        {
            Console.Write("플레이어의 x 좌표를 입력해주세요. (0~9까지): ");
            if (int.TryParse(Console.ReadLine(), out int x) && x >= 0 && x < MapSize)
            {
                playerX = x;
                break;
            }
            else
            {
                Console.WriteLine($"0과 {MapSize - 1} 사이의 숫자를 입력하세요.\n");
            }
        }

        while (true)
        {
            Console.Write("플레이어의 y 좌표를 입력해주세요. (0~9까지): ");
            if (int.TryParse(Console.ReadLine(), out int y) && y >= 0 && y < MapSize)
            {
                playerY = y;
                break;
            }
            else
            {
                Console.WriteLine($"0과 {MapSize - 1} 사이의 숫자를 입력하세요.\n");
            }
        }

        while (true)
        {
            Console.Write("적의 x 좌표를 입력해주세요. (플레이어의 위치와 겹치지 않도록 0~9까지 입력해주세요.) : ");
            if (int.TryParse(Console.ReadLine(), out int x) && x >= 0 && x < MapSize && x != playerX)
            {
                enemyX = x;
                break;
            }
            else
            {
                Console.WriteLine($"0과 {MapSize - 1} 사이의 숫자를 입력하세요.");
            }
        }

        while (true)
        {
            Console.Write("적의 y 좌표를 입력해주세요. (플레이어의 위치와 겹치지 않도록 0~9까지 입력해주세요.");
            if (int.TryParse(Console.ReadLine(), out int y) && y >= 0 && y < MapSize && y != playerY)
            {
                enemyY = y;
                break;
            }
            else
            {
                Console.WriteLine($"0과 {MapSize - 1} 사이의 숫자를 입력하세요.");
            }
        }
    }

    // 맵 출력 함수
    public void DrawMap()
    {
        for (int y = 0; y < MapSize; y++)
        {
            for (int x = 0; x < MapSize; x++)
            {
                if (x == playerX && y == playerY)
                {
                    Console.Write("●"); // 플레이어
                }
                else if (x == enemyX && y == enemyY)
                {
                    Console.Write("◆"); // 적
                }
                else if (data[x, y] == 0)
                {
                    Console.Write("□"); // 이동 가능한 길
                }
                else if (data[x, y] == 1)
                {
                    Console.Write("■"); // 이동 불가한 길
                }
                else
                {
                    Console.Write("▣"); // 적의 이동 경로
                }
            }
            Console.WriteLine();
        }
    }

    // (BFS)적을 움직이는 함수
    public void MoveEnemy()
    {
        //현재 적의 위치 저장
        queue.Enqueue((enemyX, enemyY));

        // 적 위치 표시
        data[enemyX, enemyY] = 2;

        // BFS를 위한 배열
        int[,] dist = new int[MapSize, MapSize];

        while (queue.Count > 0)
        {
            // 큐에서 꺼낸 노드
            var (x, y) = queue.Dequeue();

            // 상하좌우 이동
            for (int i = 0; i < 4; i++)
            {
                int ny = y + dy[i];
                int nx = x + dx[i];

                // 맵의 범위를 벗어난 경우
                if (ny < 0 || nx < 0 || ny >= MapSize || nx >= MapSize)
                {
                    continue;
                }

                // 이동할 수 없는 경우
                if (data[nx, ny] == 1)
                {
                    continue;
                }

                // 이미 방문한 노드인 경우
                if (visited[nx, ny])
                {
                    continue;
                }

                // 거리를 계산하여 배열에 저장
                dist[nx, ny] = dist[x, y] + 1;

                // 방문한 노드를 표시
                visited[nx, ny] = true;

                // 큐에 노드 추가
                queue.Enqueue((nx, ny));
            }
        }

        // 적 위치 초기화
        data[enemyX, enemyY] = 0;

        // 다음 위치로 이동할 방향 찾기
        int directionY = 0;
        int directionX = 0;
        int minDist = int.MaxValue;

        // 상하좌우 이동
        for (int i = 0; i < 4; i++)
        {
            int ny = enemyY + dy[i];
            int nx = enemyX + dx[i];

            // 맵의 범위를 벗어난 경우
            if (ny < 0 || nx < 0 || ny >= MapSize || nx >= MapSize)
            {
                continue;
            }

            // 이동할 수 없는 경우
            if (data[nx, ny] == 1)
            {
                continue;
            }

            // 이동 거리가 최소인 경우
            if (dist[nx, ny] < minDist)
            {
                directionY = ny;
                directionX = nx;
                minDist = dist[nx, ny];
            }
        }

        // 적 위치 이동
        enemyY = directionY;
        enemyX = directionX;

        // 적 이동 위치 표시와 이동 수 증가
        data[enemyX, enemyY] = '3';
        enemymoveCount++;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map();
            map.getEnemyPlayerPosition();
            Console.Clear();

            while (true)
            {
                map.MoveEnemy();
                map.DrawMap();
                Console.WriteLine($"적의 위치: ({map.enemyX}, {map.enemyY})");
                Console.WriteLine($"적의 이동 수 : {map.enemymoveCount}");
                System.Threading.Thread.Sleep(800);
                if (map.enemyY == map.playerY && map.enemyX == map.playerX)
                {
                    break;
                }
                Console.Clear();
            }
        }
    }
}