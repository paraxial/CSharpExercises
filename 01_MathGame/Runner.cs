using System.Diagnostics;
using MathGame;

public class Runner
{
  public enum DifficultyLevel
  {
    Easy = 45,
    Medium = 30,
    Hard = 15
  };
  private static MathGameLogic mathGame = new MathGameLogic();
  private static Random random = new Random();

  private static int userMenuSelection;

  private static int score = 0;
  private static DifficultyLevel difficultyLevel = DifficultyLevel.Easy;

  private static DifficultyLevel SetDifficulty()
  {
    int userSelection = 0;
    Console.WriteLine("Please enter a difficulty level:");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");

    while (!int.TryParse(Console.ReadLine(), out userSelection) && userSelection < 1 || userSelection > 3)
    {
      Console.WriteLine("Please enter a valid option 1-3");
    }

    return (userSelection) switch
    {
      1 => DifficultyLevel.Easy,
      2 => DifficultyLevel.Medium,
      3 => DifficultyLevel.Hard,
      _ => DifficultyLevel.Easy
    };
  }

  private static void DisplayMathGameQuestion(int firstNumber, int secondNumber, char operation)
  {
    Console.WriteLine($"{firstNumber} {operation} {secondNumber} = ?");
  }

  private static int GetUserMenuSelection(MathGameLogic mathGame)
  {
    int selection = -1;
    mathGame.ShowMenu();
    Action prompt = () => Console.WriteLine("Please enter a valid option 1-9.");

    while (selection < 1 || selection > 9)
    {
      while (!int.TryParse(Console.ReadLine(), out selection))
      {
        prompt.Invoke();
      }

      if (!(selection >= 1 && selection <= 8))
      {
        prompt.Invoke();
      }
    }

    return selection;
  }

  private static async Task<int?> GetUserResponse(DifficultyLevel difficulty)
  {
    int response = 0;
    int timeout = (int)difficulty;

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    Task<string?> getUserInputTask = Task.Run(() => Console.ReadLine());

    try
    {
      Console.WriteLine("inside the try?");
      Console.WriteLine(getUserInputTask);
      Console.WriteLine($"input is {await Task.WhenAny(getUserInputTask)}");
      Task<string?> input = (Task<string?>) await Task.WhenAny(getUserInputTask, Task.Delay(timeout * 1000));
      Console.WriteLine(input.Result);
      string? result = input == getUserInputTask ? input.Result : null;

      Console.WriteLine("I don't think this thing is waiting for the userInputTask somehow.");
      stopwatch.Stop();
      if (result != null && int.TryParse(result, out response))
      {
        string elapsedTime = stopwatch.Elapsed.ToString("m\\:ss\\.ff");
        Console.WriteLine($"Time taken to answer {elapsedTime}");
        return response;
      }
      else
      {
        throw new OperationCanceledException();
      }
    }
    catch (OperationCanceledException)
    {
      Console.WriteLine("Time is up!");
    }

    Console.WriteLine("we should not be here");
    return null;
  }

  private static int ValidateResponse(int result, int? userResponse, int score)
  {
    if (result == userResponse)
    {
      Console.WriteLine("You answered correctly! You earn 5 points.");
      score += 5;
    }
    else
    {
      Console.WriteLine($"Incorrect.  The correct answer was {result}");
    }

    return score;
  }

  private static async Task<int> DisplayAndWaitForQuestion(MathGameLogic mathGame, int firstNumber, int secondNumber, char operation, int score, DifficultyLevel difficulty)
  {
    int result;
    int? userResponse;
    DisplayMathGameQuestion(firstNumber, secondNumber, operation);
    result = mathGame.MathOperation(firstNumber, secondNumber, operation);
    userResponse = await GetUserResponse(difficulty);
    score += ValidateResponse(result, userResponse, score);

    return score;
  }

  private static List<char> functions = new List<char>() { '+', '-', '*', '/' };
  private static async Task<int> PerformOperation(int menuSelection) {
    int firstNumber = random.Next(1, 101);
    int secondNumber = random.Next(1, 101);
    char operation = functions[userMenuSelection - 1];
    if (operation == '/')
    {
      while (firstNumber % secondNumber != 0)
      {
        firstNumber = random.Next(1, 101);
        secondNumber = random.Next(1, 101);
      }
    }
    return await DisplayAndWaitForQuestion(mathGame, firstNumber, secondNumber, operation, score, difficultyLevel);
  }

  private async static void GameLoop() {
    bool running = true;
    while (running)
    {
      userMenuSelection = GetUserMenuSelection(mathGame);

      if (userMenuSelection < 5 && userMenuSelection > 0)
      {
        score += await PerformOperation(userMenuSelection);
      }
      else
      {
        switch (userMenuSelection)
        {
          case 5:
            int numberOfQuestions = 99;
            Console.WriteLine("Please enter the number of questions you want to attempt");
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions))
            {
              Console.WriteLine("Please enter the number of questions you want to attempt as an integer.");
            }
            while (numberOfQuestions > 0)
            {
              int randomOperation = random.Next(1, 5);

              score += await PerformOperation(randomOperation);
              numberOfQuestions--;
            }
            break;
          case 6:
            Console.WriteLine("GAME HISTORY: \n");
            foreach (string record in mathGame.GameHistory)
            {
              Console.WriteLine(record);
            }
            break;
          case 7:
            difficultyLevel = SetDifficulty();
            DifficultyLevel difficultyEnum = (DifficultyLevel)difficultyLevel;
            Enum.IsDefined(typeof(DifficultyLevel), difficultyEnum);
            Console.WriteLine($"Your new difficulty level is {difficultyLevel}");
            break;
          case 8:
            Console.WriteLine($"Your current score is {score}");
            break;
          case 9:
            running = false;
            Console.WriteLine($"Your final score is {score}");
            break;
        }
      }
    }
  }

  public static void Main(string[] args)
  {
    GameLoop();
  }
}