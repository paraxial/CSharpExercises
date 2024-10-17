class Calculator
{
  private static readonly Dictionary<char, Func<int, int, int>> TrueOperations = new Dictionary<char, Func<int, int, int>>
  {
    { '+', (int a, int b) => a + b },
    { '-', (int a, int b) => a - b },
    { '*', (int a, int b) => a * b },
    { '/', (int a, int b) => a / b },
    { '^', (int a, int b) => (int) Math.Pow(a, b) },
  };
  private static readonly List<char> OPERATIONS = new List<char>(TrueOperations.Keys);

  public string ListOperations() {
    return string.Join(", ", OPERATIONS.ToArray());
  }

  public bool IsValidOperator(char? operation)
  {
    if(operation == null) { return false; }

    return OPERATIONS.Contains((char) operation);
  }

  public int Calculate(int firstNumber, int secondNumber, char operation) {
    return TrueOperations[operation].Invoke(firstNumber, secondNumber);
  }
}