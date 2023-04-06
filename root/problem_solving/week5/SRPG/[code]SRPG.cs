using System;

class Map
{
    // 맵의 크기
    private const int MapSize = 10;

    // 맵 (0은 이동 가능, 1은 이동 불가)
    // 장애물을 두고 싶은 곳에 1을 입력
    private int[,] data = new int[MapSize, MapSize] {
        {0, 0, 0, 0, 1, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 1, 0, 0, 0, 0},
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
    private int playerX = 9;
    private int playerY = 9;
    private int enemyX = 1;
    private int enemyY = 0;

    public int EnemyX
    {
        get { return enemyX; }
    }
    public int EnemyY
    {
        get { return enemyY; }
    }

    public int PlayerX
    {
        get { return playerX; }
    }
    public int PlayerY
    {
        get { return playerY; }
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
                else if (data[y, x] == 0)
                {
                    Console.Write("□"); // 이동 가능한 길
                }
                else if (data[y, x] == 1)
                {
                    Console.Write("■"); // 이동 불가한 길
                }
                else
                {
                    Console.Write("▣"); // 적의 이동 경로
                }
            }
            Console.WriteLine(); // 다음 줄로 이동
        }
    }

    // 적의 위치를 플레이어 쪽으로 이동시키는 함수
    public void MoveEnemy()
    {
        // 적과 플레이어의 현재 위치에서 거리를 계산\
        int distanceX = playerX - enemyX;
        int distanceY = playerY - enemyY;

        // 상하좌우 중 이동 가능한 방향을 리스트에 저장
        List<(int dx, int dy)> directions = new List<(int dx, int dy)>();
        if (enemyX > 0 && data[enemyY, enemyX - 1] == 0) directions.Add((-1, 0));
        if (enemyX < MapSize - 1 && data[enemyY, enemyX + 1] == 0) directions.Add((1, 0));
        if (enemyY > 0 && data[enemyY - 1, enemyX] == 0) directions.Add((0, -1));
        if (enemyY < MapSize - 1 && data[enemyY + 1, enemyX] == 0) directions.Add((0, 1));

        // 이동 가능한 방향 중에서 플레이어에게 가까워지는 방향을 선택
        int minDistance = int.MaxValue;
        (int dx, int dy) nextDirection = (0, 0);
        foreach (var direction in directions)
        {
            int newX = enemyX + direction.dx;
            int newY = enemyY + direction.dy;

            // 다음 위치가 플레이어와 겹치는 경우 해당 방향으로 이동하지 않음
            if (newX == playerX + 1 && newY == playerY + 1 && newX == playerX - 1 && newY == playerY - 1)
            {
                continue;
            }

            int newDistanceX = playerX - newX;
            int newDistanceY = playerY - newY;
            int newDistance = Math.Abs(newDistanceX) + Math.Abs(newDistanceY);
            if (newDistance < minDistance)
            {
                minDistance = newDistance;
                nextDirection = direction;
            }
        }

        // 선택된 방향으로 이동, 이동 경로 표시
        enemyX += nextDirection.dx;
        enemyY += nextDirection.dy;
        data[enemyY, enemyX] = '3';
    }
}

class Program
{
    static void Main(string[] args)
    {
        Map map = new Map();

        while (true)
        {
            Console.Clear();
            map.MoveEnemy();
            map.DrawMap();
            Console.WriteLine($"적의 위치: ({map.EnemyX}, {map.EnemyY})");
            System.Threading.Thread.Sleep(800);
            if (map.EnemyX == map.PlayerX && map.EnemyY == map.PlayerY)
            {
                break;
            }
        }
    }
}
