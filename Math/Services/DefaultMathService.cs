using Math.Abstractions;


namespace Math.Services
{
    public class DefaultMathService : IMathService
    {
        public int Addition(int a, int b) => a + b;
        public int Division(int a, int b) => a / b;
        public int Multiplication(int a, int b) => a * b;
        public int Substraction(int a, int b) => a - b;
    }
}
