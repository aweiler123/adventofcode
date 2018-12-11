using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day3part1
{
    class Program
    {
        public class Point
        {
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int x;
            public int y;
        }
        public class Patch
        {
            public Point LeftUpper;
            public int Height;
            public int Width;
            public int Id;

            public Patch(string line)
            {
                var splitLine = line.Split(' ');
                Id = Int32.Parse(splitLine[0].Substring(1));

                string pointStr = splitLine[2];
                string dimensionsStr = splitLine[3];

                var splitPointStr = pointStr.Split(',');
                var splitDimensionsStr = dimensionsStr.Split('x');

                LeftUpper = new Point(Int32.Parse(splitPointStr[0]),
                    Int32.Parse(splitPointStr[1].Substring(0, splitPointStr[1].Length - 1)));

                Width = Int32.Parse(splitDimensionsStr[0]);
                Height = Int32.Parse(splitDimensionsStr[1]);
            }
        }

        private const int FabricHeight = 1000;
        private const int FabricWidth = 1000;

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("tiles.txt");
            var patches = lines.Select(x => new Patch(x));

            StringBuilder sb = new StringBuilder();
            foreach (var patch in patches)
            {
                sb.AppendLine($"#{patch.Id} @ {patch.LeftUpper.x},{patch.LeftUpper.y}: {patch.Width}x{patch.Height}");
            }

            File.WriteAllText("tiles2.txt",sb.ToString());
            var fabric = GenerateFabric(patches);

            int numberInchesOverutilized = 0;
            for(int i = 0; i < FabricWidth; i++)
                for(int j = 0; j < FabricHeight; j++)
                    if (fabric[i,j] > 1) numberInchesOverutilized++;

            Console.WriteLine($"Number of overutilized squares: {numberInchesOverutilized}");
            Console.ReadKey();
        }

        private static int[,] GenerateFabric(IEnumerable<Patch> patches)
        {
            int[,] fabric = new int[FabricWidth, FabricHeight];

            foreach (var patch in patches)
            {
                for (int i = patch.LeftUpper.x; i <= patch.LeftUpper.x + patch.Width && i < FabricWidth; i++)
                    for (int j = patch.LeftUpper.y; j <= patch.LeftUpper.y + patch.Height && j < FabricHeight; j++)
                        fabric[i, j]++;                
            }
            return fabric;
        }
    }
}
