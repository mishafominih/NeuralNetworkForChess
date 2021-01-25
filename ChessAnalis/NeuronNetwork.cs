using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNetwork
{
    class NeuralNetwork
    {
        private Layer start;
        private int count;
        private int[] lenghts;

        public NeuralNetwork(int count, int[] lenghts)
        {
            this.count = count;
            this.lenghts = lenghts;
            start = newLayer();
        }

        public NeuralNetwork GetClone() {
            var res = new NeuralNetwork(count, lenghts);
            res.start = GetClons(start).First();
            return res;
        }

        private Layer newLayer()
        {
            var res = new Layer(lenghts[0], lenghts[0]);
            var prevLayer = res;
            for (int i = 1; i < count; i++)
            {
                var t = new Layer(lenghts[i], lenghts[i - 1]);
                prevLayer.SetNext(t);
                prevLayer = t;
            }
            return res;
        }

        public List<double> Run(List<double> input) => start.ActivateNeurons(input);

        public void Study(string path, double d)
        {
            var input = new List<List<double>>();
            var output = new List<List<double>>();
            GetData(ref input, ref output, path);

            var start = new List<double>();
            var end = new List<double>();
            while (true)
            {
                var succes = true;
                for (int i = 0; i < input.Count; i++)
                {
                    start = input[i];
                    end = output[i];
                    var res = Run(start);
                    if (Compare(res, end) > d)
                    {
                        succes = false;
                        break;
                    }
                }
                if(succes) 
                    return;
                Learn(d, start, end);
            }
        }

        public void Save(string path)
        {
            var result = new List<string> { count.ToString() };
            var layer = start;
            while (layer != null)
            {
                var str = new StringBuilder();
                foreach (var n in layer.neurons)
                {
                    n.weights.ForEach(weight => str.Append(weight.ToString() + ' '));
                    str = str.Remove(str.Length - 1, 1);
                    str.Append('|');
                }
                str = str.Remove(str.Length - 1, 1);
                result.Add(str.ToString());

                layer = layer.next;
            }
            File.WriteAllLines(path, result);
        }

        public void Load(string path)
        {
            var lines = File.ReadAllLines(path);
            count = int.Parse(lines[0]);
            var lenghts = new List<int>();
            var res = new Layer(0, 0);
            var prev = res;
            for (int i = 1; i < lines.Length; i++)
            {
                var neurons = lines[i]
                    .Split('|')
                    .Select(x => x.Split(' ')
                        .Select(y => double.Parse(y)).ToList())
                    .Select(x => new Neuron(x))
                    .ToList();
                lenghts.Add(neurons.Count());
                var layer = new Layer(neurons);
                prev.SetNext(layer);
                prev = layer;
            }
            this.lenghts = lenghts.ToArray();
            start = res.next;
        }

        private void Learn(double d, List<double> start, List<double> end)
        {
            var layers = Enumerable.Range(0, 5).Select(y => this.start.GetClone()).ToList();
            double v = Compare(layers.First().ActivateNeurons(start), end);
            while (v >= d)
            {
                //0layers.Add(newLayer());
                layers = layers
                    .SelectMany(x => GetClons(x))
                    .OrderBy(x => Compare(x.ActivateNeurons(start), end))
                    .Take(5)
                    .ToList();
                v = Compare(layers.First().ActivateNeurons(start), end);
            }
            this.start = layers.First();
        }

        private static List<Layer> GetClons(Layer layer)
        {
            var list = Enumerable.Range(0, 5).Select(y => layer.GetClone()).ToList();
            list.Add(layer);
            return list;
        }

        private void GetData(ref List<List<double>> start, ref List<List<double>> end, string path)
        {
            var strs = File.ReadAllLines(path);
            start = GetData(strs, 0);
            end = GetData(strs, 1);
        }

        private static List<List<double>> GetData(string[] strs, int n)
        {
            return strs
                .Select(x => x.Split(':')[n])
                .Select(x => x.Split(' '))
                .Select(x => x.Select(y => double.Parse(y)).ToList())
                .ToList();
        }

        private double Compare(List<double> first, List<double> second)
        {
            return Math.Abs(first.Sum() - second.Sum());
        }
    }
}
