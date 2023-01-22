using System;

namespace TrynaRenderACube
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new AnimationSample())
                game.Run();
        }
    }
}
