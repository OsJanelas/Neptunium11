using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Texto em ASCII Art (Fonte: Big)
        string[] logo = {
            "__        __   _                                _             ",
            "\\ \\      / /__| | ___ ___  _ __ ___   ___      | |_ ___       ",
            " \\ \\ /\\ / / _ \\ |/ __/ _ \\| '_ ` _ \\ / _ \\     | __/ _ \\      ",
            "  \\ V  V /  __/ | (_| (_) | | | | | |  __/     | || (_) |     ",
            "   \\_/\\_/ \\___|_|\\___\\___/|_| |_| |_|\\___|      \\__\\___/      ",
            " _   _             _              _                  _  _     ",
            "| \\ | | ___ _ __ | |_ _   _ _ __ (_)_   _ _ __ ___ / || |    ",
            "|  \\| |/ _ \\ '_ \\| __| | | | '_ \\| | | | | '_ ` _ \\| || |    ",
            "| |\\  |  __/ |_) | |_| |_| | | | | | |_| | | | | | | || |    ",
            "|_| \\_|\\___| .__/ \\__\\__,_|_| |_|_|\\__,_|_| |_| |_|_||_|    ",
            "           |_|                                                "
        };

        Console.CursorVisible = false;
        double hue = 0;

        // Loop de animação RGB
        while (true)
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < logo.Length; i++)
            {
                // Altera a cor ligeiramente para cada linha para dar efeito de movimento
                double rowHue = hue + (i * 0.1);
                var (r, g, b) = HsvToRgb(rowHue % 1.0, 1.0, 1.0);

                // Aplica a cor ANSI: \u001b[38;2;R;G;Bm
                Console.Write($"\u001b[38;2;{r};{g};{b}m");
                Console.WriteLine(logo[i]);
            }

            hue += 0.02; // Velocidade da troca de cor
            Thread.Sleep(30); // Controla o FPS
        }
    }

    // Função para converter HSV (Matiz) para RGB para um gradiente suave
    static (int r, int g, int b) HsvToRgb(double h, double s, double v)
    {
        int hi = (int)Math.Floor(h * 6);
        double f = h * 6 - hi;
        double p = v * (1 - s);
        double q = v * (1 - f * s);
        double t = v * (1 - (1 - f) * s);

        return hi switch
        {
            0 => ((int)(v * 255), (int)(t * 255), (int)(p * 255)),
            1 => ((int)(q * 255), (int)(v * 255), (int)(p * 255)),
            2 => ((int)(p * 255), (int)(v * 255), (int)(t * 255)),
            3 => ((int)(p * 255), (int)(q * 255), (int)(v * 255)),
        };
    }
}