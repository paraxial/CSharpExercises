class Runner
{

  private static Random random = new Random();
  private static readonly List<String> obnoxiousGreetings = new List<string>
  {
    "It's a good day to calculate!",
    "It's all binary to me~",
    "I think we can really *add* some fun to this process",
    "I hope I can help!",
    "૮ ˶ᵔ ᵕ ᵔ˶ ა",
    "(˶˃ ᵕ ˂˶)",
    "≽^•⩊•^≼",
    "Gosh, I just love numbers"
  };

  private static readonly List<String> obnoxiousPrompts = new List<string>
  {
    "Please enter a number",
    "uWu, tell me a number senpai",
    "Number pls? >ᴗ<",
    "I'll be your kouhai if you tell me a number~"
  };

  private static string RandomString(List<string> phrases)
  {
    return phrases[random.Next(0, phrases.Count)];
  }

  public static void Main(String[] args)
  {
    Console.WriteLine($"Hello, welcome to a simple calculator! {RandomString(obnoxiousGreetings)}");
    Console.WriteLine("⋆*⋆*⋆*⋆\n");

    Calculator calculator = new Calculator();

    Console.CancelKeyPress += delegate
    {
      Console.WriteLine("Bye bye~");
    };

    while (true)
    {
      int firstNumber;
      int secondNumber;
      char operation = ' ';

      Console.WriteLine($"\n{RandomString(obnoxiousPrompts)} > ");

      while (!int.TryParse(Console.ReadLine(), out firstNumber))
      {
        Console.WriteLine($"\n{RandomString(obnoxiousPrompts)} > ");
      }

      while (!calculator.IsValidOperator(operation))
      {
        Console.WriteLine($"\nPlease enter a mathematical operation: ({calculator.ListOperations()})");
        operation = Console.ReadKey().KeyChar;
      }

      while (!int.TryParse(Console.ReadLine(), out secondNumber))
      {
        Console.WriteLine("\nuWu, please enter *another* number > ");
      }

      Console.WriteLine($"\nYour result is \x1b[7m{calculator.Calculate(firstNumber, secondNumber, operation)}\x1b[27m");
    }
  }
}