using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetwork;

namespace chassAnaliz
{
    class Program
    {
        const int countNetworks = 2;
        private const int countStep = 100;

        static void Main(string[] args)
        {
            var whiteGamer = CraeteNetwork();
            var blackGamer = CraeteNetwork();

            whiteGamer.Load("whiteGamer_v1");//загрузка нейронных сетей
            blackGamer.Load("blackGamer");

            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(i);
                var winner = PlayGame(whiteGamer, blackGamer);
                if (winner == whiteGamer)
                    blackGamer = blackGamer.GetClone();
                else
                    whiteGamer = whiteGamer.GetClone();
            }

            whiteGamer.Save("whiteGamer_v2");
            blackGamer.Save("blackGamer_v2");

            Console.WriteLine("обучение закончено");
            Console.Read();
        }

        private static NeuralNetwork CraeteNetwork()
        {
            return new NeuralNetwork(3, new int[] { 96, 96, 4 });
        }

        private static NeuralNetwork PlayGame(NeuralNetwork whiteGamer, NeuralNetwork blackGamer)
        {
            var game = new Game();
            bool isWhite = true;
            for (int i = 0; i < countStep; i++)
            {
                var output = new List<double>();
                var input = game.GetInfo(isWhite);
                if (isWhite)
                    output = whiteGamer.Run(input);
                else
                    output = blackGamer.Run(input);
                game.Update(output);

                //game.DrowMap(true);//отрисовка карты
                //Console.Read();
                
                isWhite = !isWhite;
            }
            return  game.GetCountFigure(false) > game.GetCountFigure(true) ? whiteGamer : blackGamer;
        }
    }
}
