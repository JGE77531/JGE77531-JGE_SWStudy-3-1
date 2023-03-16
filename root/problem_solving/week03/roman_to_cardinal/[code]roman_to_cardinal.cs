using System.Linq;
using System.Threading;

internal class Class1
{
    private static void Main(string[] args)
    {

        Thread thread = new Thread(escFunc);
        thread.Start();

        while (true)
        {
            while (true)
            {
                IntegerToRoman();
                break;
            }

            while (true)
            {
                RomanToInteger();
                break;
            }
            Console.WriteLine("프로그램을 종료합니다. ESC를 눌러주세요.");
            break;
        }
    }

    private static void IntegerToRoman()
    {
        string[] one = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };
        string[] ten = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] hundred = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] thousand = { "", "M", "MM", "MMM" };

        int one_index;
        int ten_index;
        int hundred_index;
        int thousand_index;

        string result;

        while (true)
        {

            Console.Write("1~3999의 정수를 입력해주세요. : ");
            string num = Console.ReadLine();

            try
            {
                int num_2 = int.Parse(num);
                if (num_2 < 1 || num_2 > 3999)
                {
                    Console.WriteLine("잘못 입력되었습니다.");
                    Console.Write("\n");
                    continue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("잘못 입력되었습니다.");
                Console.Write("\n");
                continue;
            }

            int count = int.Parse(num);

            one_index = count % 10;
            ten_index = (count / 10) % 10;
            hundred_index = (count / 100) % 10;
            thousand_index = (count / 1000) % 10;

            result = thousand[thousand_index] + hundred[hundred_index] + ten[ten_index] + one[one_index];

            Console.Write("입력된 수 " + count + "을 로마자로 변환한 결과 값은 : ");
            Console.WriteLine("[" + result + "] 입니다.\n");

            break;
        }
    }


    private static void RomanToInteger()
    {
        Dictionary<char, int> romanValues = new Dictionary<char, int>
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
        };

        List<char> romanList = new List<char> { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

        int result = 0;
        int prev = 0;

        while (true)
        {
            Console.WriteLine("로마 숫자를 숫자로 변환 합니다. ");
            Console.Write("로마 숫자를 입력해주세요. : ");
            string roman = Console.ReadLine();

            bool isCheck = true;
            foreach (char c in roman)
            {
                if (!romanList.Contains(c))
                {
                    isCheck = false;
                    break;
                }
            }

            if (!isCheck)
            {
                Console.WriteLine("잘못 입력되었습니다.");
                Console.Write("\n");
                continue;
            }

            for (int i = roman.Length - 1; i >= 0; i--)
            {
                int current = romanValues[roman[i]];

                if (current < prev)
                {
                    result -= current;
                }
                else
                {
                    result += current;
                }

                prev = current;
            }

            Console.Write("입력된 로마자 " + roman + "을 숫자로 변환한 결과 값은 : ");
            Console.WriteLine("[" + result + "] 입니다.\n");
            break;
        }
    }

    private static void escFunc()
    {
        while (true)
        {
            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
            {
                Console.WriteLine();
                Console.WriteLine("프로그램을 종료합니다.");
                Environment.Exit(0);
            }
        }
    }
}