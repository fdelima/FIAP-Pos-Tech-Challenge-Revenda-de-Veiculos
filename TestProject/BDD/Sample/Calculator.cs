namespace TestProject.BDD.Sample
{
    internal class Calculator
    {
        int _firstNumber = 0;
        int _secondNumber = 0;

        public int Result { get; internal set; }

        internal void AddNumbers()
        {
            Console.WriteLine($"{_firstNumber} + {_secondNumber}");
            Result = _firstNumber + _secondNumber;
        }

        internal void SetFirstNumber(int firstNumber)
        {
            _firstNumber = firstNumber;
            Console.WriteLine(firstNumber);
        }

        internal void SetSecondNumber(int secondNumber)
        {
            _secondNumber = secondNumber;
            Console.WriteLine(secondNumber);
        }
    }
}
