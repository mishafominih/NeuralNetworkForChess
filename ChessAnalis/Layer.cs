using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetwork
{
    public class Layer
    {
        public List<Neuron> neurons = new List<Neuron>();
        public Layer next;

        public Layer(int count, int countInput)
        {
            for (int i = 0; i < count; i++)
                neurons.Add(new Neuron(countInput));
        }

        public Layer(List<Neuron> ns) => neurons = ns;

        public void SetNext(Layer l) => next = l;

        public int GetCountElem() => neurons == null ? 0 : neurons.Count;

        public List<double> ActivateNeurons(List<double> values)
        {
            var res = neurons
                .Select(x => x.Activate(values))
                .ToList();
            return next == null ? res : next.ActivateNeurons(res);
        }

        public Layer GetClone()
        {
            var newNeurons = neurons
                .Select(x => x.GetClone())
                .ToList();
            var res = new Layer(newNeurons);
            if (next != null)
                res.SetNext(next.GetClone());
            return res;
        }
    }
}
