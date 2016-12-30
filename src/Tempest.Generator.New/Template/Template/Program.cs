namespace SelfHostedGenerator.Template
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            var greeter = new ReplaceMeGreeter();
            greeter.SayHello();
        }
    }
}
