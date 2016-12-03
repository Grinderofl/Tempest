namespace Tempest.Generator.Empty.Template
{
    public class Program
    {
        public static void Main(params string[] args)
        {
            var greeter = new MyProjectGreeter();
            greeter.SayHello();
        }
    }
}
