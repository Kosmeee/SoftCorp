namespace Client.Tools
{
    public static class Reader
    {
        public static string EndLine = Environment.NewLine;
        public static int ReadInt(string text)
        {
            Console.Clear();
            Console.Write($"{text}:{EndLine}{EndLine}Id:");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("We need an integer here");
                Console.Write("Id:");
            }

            return id;
        }

        public static string ReadStatus()
        {
            int pressedNumber;
            while (true)
            {
                Console.Write($"{EndLine}1 = New, 2 = Active, 3 = Blocked, 4 = Deleted{EndLine}Status:");
                var key = Console.ReadKey();
                if (char.IsDigit(key.KeyChar))
                {
                    pressedNumber = int.Parse(key.KeyChar.ToString());
                    if (pressedNumber is > 0 and < 5)
                        break;
                }

                Console.WriteLine($"{EndLine}Wrong status, press another key");

            }
            return ((Statuses)pressedNumber).ToString();
        }

        public static string ReadName()
        {
            Console.Write($"{EndLine}Name:");
            return Console.ReadLine();
        }
    }
}
