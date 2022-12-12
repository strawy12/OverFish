using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCalculate
{
    protected float height;
    float minHeight = 10f;
    float maxHeight = 100f;
    protected float weight;

    protected FishCalculate()
    {
        height = Rand(minHeight, maxHeight);
        weight = WeightCalculate();
    }

    private float WeightCalculate()
    {
        float result = height / 10;
        float pivot = Random.Range(0.8f, 1.2f);
        result *= pivot;
        return result;
    }
    public float Rand(float min, float max)
    {
        float[] inputdatas = new float[10];
        for (int i = 0; i < 10; i++)
        {
            inputdatas[i] = Random.Range(min, max);
        }
        return GetRandom(inputdatas);
    }

    public float GetRandom(float[] inputDatas)
    {
        float total = 0;
        for (int i = 0; i < inputDatas.Length; i++)
        {
            total += inputDatas[i];
        }
        total /= inputDatas.Length;
        return Rand(total);
    }

    public float Rand(float value)
    {
        float pivot = Random.Range(0.0f, 1f);
        value *= pivot;
        return value;
    }
    
    protected float Calculate()
    {
        float result = 2f;
        result += (height * weight) / 2;
        return result;
    }
}
