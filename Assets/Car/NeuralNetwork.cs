using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    public int hiddenLayers = 1;
    public int size_hidden_layers = 10;
    public int outputs = 2;
    public int inputs = 5;
    public float maxInitialValue = 5f;

    private const float EULER_NUMBER = 2.71828f;
    private List<List<float>> neurons;
    private List<float[][]> weights;

    private int totalLayers = 0; 

    public NeuralNetwork()
    {
        totalLayers = hiddenLayers + 2;// hidden layers + inputslayer+outputlayer
        //Initialize weights and the neurons array
        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        //Fill neurons and weights
        for(int i = 0; i < totalLayers; i++)
        {
            float[][] layerWeights;
            List<float> layer = new List<float>();
            int sizeLayer = getSizeLayer(i);
            if (i != 1 + hiddenLayers)
            {
                layerWeights = new float[sizeLayer][];
                int nextSizeLayer = getSizeLayer(i + 1);
                for(int j = 0; j < sizeLayer; j++)//current
                {
                    layerWeights[j] = new float[nextSizeLayer];
                    for(int k = 0; k < nextSizeLayer; k++)// next layer
                    {
                        layerWeights[j][k] = genRandomValue();
                    }
                }
                weights.Add(layerWeights);
            }
            for(int j = 0; j < sizeLayer; j++)
            {
                layer.Add(0);
            }
            neurons.Add(layer);
        }

    }
    public NeuralNetwork(DNA dna)
    {
        List<float[][]> weightsDNA = dna.getDNA();
        totalLayers = hiddenLayers + 2;// hidden layers + inputslayer+outputlayer
        //Initialize weights and the neurons array
        weights = new List<float[][]>();
        neurons = new List<List<float>>();

        //Fill neurons and weights
        for (int i = 0; i < totalLayers; i++)
        {
         
            float[][] layerWeights;
            float[][] weightsDNALayer;
            List<float> layer = new List<float>();
            int sizeLayer = getSizeLayer(i);
            if (i != 1 + hiddenLayers)
            {
                weightsDNALayer = weightsDNA[i];
                layerWeights = new float[sizeLayer][];
                int nextSizeLayer = getSizeLayer(i + 1);
                for (int j = 0; j < sizeLayer; j++)//current
                {
                    layerWeights[j] = new float[nextSizeLayer];
                    for (int k = 0; k < nextSizeLayer; k++)// next layer
                    {
                        layerWeights[j][k] = weightsDNALayer[j][k];
                    }
                }
                weights.Add(layerWeights);
            }
            for (int j = 0; j < sizeLayer; j++)
            {
                layer.Add(0);
            }
            neurons.Add(layer);
        }
    }
    public void feedForward(float[]inputs)
    {
    
        //Set inputs in input layer
        List<float> inputLayer = neurons[0];
        for(int i = 0; i < inputs.Length; i++)
        {
            inputLayer[i] = inputs[i];
        }
        //Update neurons from the input Layer to the output Layer
        for(int layer =0;layer< neurons.Count-1; layer++) {
            
            float[][] weightsLayer = weights[layer];
            int nextLayer = layer + 1;
            List<float> neuronsLayer = neurons[layer];
            List<float> neuronsNextLayer = neurons[nextLayer];
            for(int i = 0; i < neuronsNextLayer.Count; i++) //Next layer
            {
                float sum = 0;
                for(int j = 0; j < neuronsLayer.Count; j++)
                {
                    sum += weightsLayer[j][i] * neuronsLayer[j];//Feed-forward multiplication
                }
                neuronsNextLayer[i] = sigmoid(sum);

            }
        }
    
    }
    public int getSizeLayer(int i)
    {
        //Layer position i
        int sizeLayer = 0;
        //Depending on the layer it will give a different size
        if (i == 0)
        {
            sizeLayer = inputs;

        }else if(i == hiddenLayers + 1)
        {
            sizeLayer = outputs;
        }
        else
        {
            sizeLayer = size_hidden_layers;
        }
        return sizeLayer;
    }
    public List<float> getOutputs()
    {
        return neurons[neurons.Count - 1];
    }
    public float sigmoid(float x)
    {
        return 1 / (float)(1 + Mathf.Pow(EULER_NUMBER, -x));
    }
    public float genRandomValue()
    {
        return Random.Range(-maxInitialValue, maxInitialValue);
    }
    public List<List<float>> getNeurons()
    {
        return neurons;
    }
    public List<float[][]> getWeights()
    {
        return weights;
    }
}
