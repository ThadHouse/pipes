using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThadPipes
{
    class Program
    {
        private static void visualize(int[] pipes, int[] waters)
        {
            //Encoding setup, draw chars, and spacing calculations
            Console.OutputEncoding = Encoding.UTF8;

            char pipe = (char)0x2588;
            char water = (char)0x2591;
            var pipesHeight = pipes.Max();
            var pipesSpacing = pipesHeight.ToString().Length;

            var waterHeight = waters.Max();
            var waterSpacing = waterHeight.ToString().Length;

            for (int x = 0; x < pipes.Length; x++)
            {
                //Print waters labels (vertically if double-digit numbers)
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                var waterLabel = waters[x].ToString();
                for (int i = 0; i < waterLabel.Length; i++)
                {
                    Console.SetCursorPosition(x, waterSpacing - waterLabel.Length + i);
                    Console.Write(waterLabel[i]);
                }

                //Draw pipes and waters
                for (int y = pipesHeight + waterSpacing; y > pipesHeight + waterSpacing - pipes[x] - waters[x]; y--)
                {
                    Console.SetCursorPosition(x, y);

                    if (pipesHeight + waterSpacing - y < pipes[x])
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(pipe);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(water);
                    }
                }

                //Print pipes labels (vertically if double-digit numbers)
                Console.ForegroundColor = ConsoleColor.Gray;
                var pipeLabel = pipes[x].ToString();
                for (int i = 0; i < pipeLabel.Length; i++)
                {
                    Console.SetCursorPosition(x, pipesHeight + waterSpacing + pipesSpacing + pipesSpacing - pipeLabel.Length + i);
                    Console.Write(pipeLabel[i]);
                }

            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Random random = new Random();


            while (true)
            {
                int[] pipes = new int[100];
                for (int i = 0; i < pipes.Length; i++)
                {
                    pipes[i] = random.Next(1, 20);
                }

                // Remove left side
                int startIndex = 0;
                int endIndex = pipes.Length - 1;

                int iterCount = 0;

                if (pipes[1] >= pipes[0])
                {
                    for (int i = 1; i < pipes.Length; i++)
                    {
                        iterCount++;
                        if (pipes[i - 1] > pipes[i])
                        {
                            startIndex = i - 1;
                            break;
                        }
                    }
                }

                if (pipes[pipes.Length - 2] >= pipes[pipes.Length - 1])
                {
                    for (int i = pipes.Length - 2; i >= 0; i--)
                    {
                        iterCount++;
                        if (pipes[i] < pipes[i + 1])
                        {
                            endIndex = i + 1;
                            break;
                        }
                    }
                }

                int[] pipeHeight = new int[pipes.Length];

                bool moveRight = pipes[startIndex] < pipes[endIndex];



                int sum = 0;

                int currentBaseHeight = moveRight ? pipes[startIndex] : pipes[endIndex];

                if (moveRight)
                {
                    startIndex++;
                }
                else
                {
                    endIndex--;
                }

                while (startIndex < endIndex)
                {
                    iterCount++;
                    if (moveRight)
                    {
                        if (pipes[startIndex] > currentBaseHeight)
                        {
                            // Found a greater one, recalculate max, and then go
                            moveRight = pipes[startIndex] < pipes[endIndex];
                            currentBaseHeight = moveRight ? pipes[startIndex] : pipes[endIndex];
                            if (moveRight)
                            {
                                startIndex++;
                            }
                            else
                            {
                                endIndex--;
                            }
                        }
                        else
                        {
                            sum += currentBaseHeight - pipes[startIndex];
                            pipeHeight[startIndex] = currentBaseHeight - pipes[startIndex];
                            startIndex++;
                        }
                    }
                    else
                    {
                        if (pipes[endIndex] > currentBaseHeight)
                        {
                            // as above
                            moveRight = pipes[startIndex] < pipes[endIndex];
                            currentBaseHeight = moveRight ? pipes[startIndex] : pipes[endIndex];
                            if (moveRight)
                            {
                                startIndex++;
                            }
                            else
                            {
                                endIndex--;
                            }
                        }
                        else
                        {
                            sum += currentBaseHeight - pipes[endIndex];
                            pipeHeight[endIndex] = currentBaseHeight - pipes[endIndex];
                            endIndex--;
                        }
                    }
                    ;
                }

                visualize(pipes, pipeHeight);

                Console.WriteLine($"iter {iterCount}");
                Console.WriteLine($"sum {sum}");

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
