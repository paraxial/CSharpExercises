namespace MathGame
{
  public class MathGameLogic {
    public List<string> GameHistory {get; set; } = new List<string>();

    public void ShowMenu()
    {
      Console.WriteLine("Please enter an option to select the operation you want.");
      Console.WriteLine("1. Addition");
      Console.WriteLine("2. Subtraction");
      Console.WriteLine("3. Multiplication");
      Console.WriteLine("4. Division");
      Console.WriteLine("5. Random Mode");
      Console.WriteLine("6. Show History");
      Console.WriteLine("7. Change Difficulty");
      Console.WriteLine("8. Show Score");
      Console.WriteLine("9. Exit");
    }

    public int MathOperation(int firstNumber, int secondNumber, char operation) {
      switch(operation) {
        case '+':
          GameHistory.Add($"{firstNumber} + {secondNumber} = { firstNumber + secondNumber }");
          return firstNumber + secondNumber;
        case '-':
          GameHistory.Add($"{firstNumber} - {secondNumber} = { firstNumber - secondNumber }");
          return firstNumber - secondNumber;
        case '*':
          GameHistory.Add($"{firstNumber} * {secondNumber} = { firstNumber * secondNumber }");
          return firstNumber * secondNumber;
        case '/':
          while(firstNumber < 0 || firstNumber > 100) {
            try {
              Console.WriteLine("Please enter a number between 0 and 100");
              firstNumber = Convert.ToInt32(Console.ReadLine());
            } catch ( System.Exception ) {}
          }
          GameHistory.Add($"{firstNumber} / {secondNumber} = { firstNumber / secondNumber }");
          return firstNumber / secondNumber;
      }

      return 0;
    }
  }
}