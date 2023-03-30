using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PieceType
{
    [SerializeField] List<int> types = new();

    public void Add(int pieceIndex)
    {
        types.Add(pieceIndex);
    }

    public void PerfectShuffle()
    {
        System.Random random = new();
        List<int> tempList = new(types);

        for (int i = types.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = types[i];
            types[i] = types[j];
            types[j] = temp;
        }
        for (int i = 0; i < types.Count; i++)
        {
            if (types[i] == tempList[i])
            {
                for (int j = 0; j < types.Count; j++)
                {
                    if (j != i)
                    {
                        int temp = types[i];
                        types[i] = types[j];
                        types[j] = temp;
                        break;
                    }
                }
            }
        }
    }

    public void Print()
    {
        string list = "{ ";

        for (int i = 0; i < types.Count; i++)
        {
            if (i == types.Count - 1)
                list += types[i] + " }";
            else
                list += types[i] + ", ";
        }

        Debug.Log(list);
    }

    public int Get(int index)
    {
        return types[index];
    }
}
