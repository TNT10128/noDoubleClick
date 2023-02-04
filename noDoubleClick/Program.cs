using Figgle;
using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace NoDoubleClick
{
    internal class Program
    {
        public const string Name = "noDoubleClick";

        private static readonly ILoggerFactory s_loggerFactory = 
            LoggerFactory.Create(builder => builder.AddSimpleConsole(options =>
            {
                options.SingleLine = true;
            }));

        public static readonly ILogger Logger = s_loggerFactory.CreateLogger(string.Empty);

        public static readonly DoubleClickPreventer LeftButtonDoubleClickPreventer = new(MouseButtons.Left);
        public static readonly DoubleClickPreventer RightButtonDoubleClickPreventer = new(MouseButtons.Right);

        private static readonly MouseListener s_listener = new(); 

        private static void PrintNameAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(FiggleFonts.Slant.Render(Name));
            Console.ResetColor();
        }

        private static void InitializeClickListener()
        {
            s_listener.Subscribe();
            Logger.LogInformation("Initialized click listener.");
        }

        private static void Main()
        {
            PrintNameAsciiArt();
            Logger.LogInformation("Loading {name}...", Name);
            InitializeClickListener();
            Logger.LogInformation("Loaded!");
            Application.Run();
        }
    }
}