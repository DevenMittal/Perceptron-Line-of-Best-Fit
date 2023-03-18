using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron_Line_of_Best_Fit
{


    public class Perceptron
    {
        double[] weights;
        double bias;
        double mutationAmount;
        Random random;
        Func<double, double, double> errorFunc;
        double slope;

        public Perceptron(double[] initialWeightValues, double initialBiasValue, double mutationAmount, Random random, Func<double, double, double> errorFunc)
        {
            weights = initialWeightValues;
            bias = initialBiasValue;
            this.mutationAmount = mutationAmount;
            this.random = random;
            this.errorFunc = errorFunc;


        }



        public Perceptron(int amountOfInputs, double mutationAmount, Random random, Func<double, double, double> errorFunc)
            : this(new double[amountOfInputs], random.NextDouble(), mutationAmount, random, errorFunc)
        {
            for (int i = 0; i < amountOfInputs; i++)
            {
                weights[i] = random.NextDouble();
            }
            /*Initializes the weights array given the amount of inputs*/
        }

        public void Randomize(Random random, double min, double max)
        {
            Random rand = new Random();

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = rand.NextDouble() * (max - min) + min;
            }
            bias = rand.NextDouble() * (max - min) + min;
            /*Randomly generates values for every weight including the bias*/
        }

        public double Compute(double[] inputs)
        {
            double sum = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * weights[i];
            }
            return sum + bias;
            /*computes the output with given input*/
        }

        public double[] Compute(double[][] inputs)
        {
            double[] sums = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                double temp = Compute(inputs[i]);
                if (temp < .50) sums[i] = 0;

                else sums[i] = 1;
            }
            return sums;
            /*computes the output for each row of inputs*/
        }

        public double GetError(double[][] inputs, double[] desiredOutputs)
        {
            double sum = 0;
            double[] results = Compute(inputs);
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += errorFunc.Invoke(desiredOutputs[i], results[i]);
            }
            return sum / inputs.Length;
            /*computes the output using the inputs returns the average error between each output row and each desired output row using errorFunc*/
        }


        public double TrainWithHillClimbingGate(double[][] inputs, double[] desiredOutputs)
        {
            double currentError = GetError(inputs, desiredOutputs);
            double[] tempWeights = new double[weights.Length];
            weights.CopyTo(tempWeights, 0);
            double tempBias = bias;
            MutateGate(weights);
            double newError = GetError(inputs, desiredOutputs);
            if (newError < currentError)
            {
                currentError = newError;
                //right here you need to continue from step 4 on the wiki
            }
            else
            {
                weights = tempWeights;
                bias = tempBias;
            }
            return currentError;
            /*attempts one hill climbing training iteration and returns the new current error*/
        }


        public double[] MutateGate(double[] weights)
        {
            int index = random.Next(0, weights.Length + 1);
            //randomze from 0 to weights.length + 1 and if it is weights.length
            //5 10  20 
            double mutation = random.NextDouble() * (mutationAmount * 2) - mutationAmount;

            if (index == weights.Length) bias += mutation;

            else weights[index] += mutation;

            return weights;
        }



    }
}
