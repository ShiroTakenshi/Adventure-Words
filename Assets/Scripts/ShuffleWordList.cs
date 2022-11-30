using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShuffleWordList
{
    public static List<E> ShuffleListItems<E>(List<E> inputList)
    {
        List<E> originalList = new List<E>();
        originalList.AddRange(inputList);
        List<E> randomList = new List<E>();

        System.Random r = new System.Random();
        int randomIndex = 0;
        while (originalList.Count > 0)
        {
            randomIndex = r.Next(0, originalList.Count);
            randomList.Add(originalList[randomIndex]);
            originalList.RemoveAt(randomIndex); 
        }

        return randomList;
    }
}