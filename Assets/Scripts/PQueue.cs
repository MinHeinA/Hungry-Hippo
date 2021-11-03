using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PQueue<T>
{
    List<Tuple<T>> elements = new List<Tuple<T>>();

    /// <summary>
    /// Return the total number of elements currently in the Queue.
    /// </summary>
    /// <returns>Total number of elements currently in Queue</returns>
    public int Count
    {
        get { return elements.Count; }
    }

    /// <summary>
    /// Add given item to Queue and assign item the given priority value.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    /// <param name="priorityValue">Item priority value as Double.</param>
    public void Enqueue(T item, int priorityValue)
    {
        elements.Add(new Tuple<T>(item, priorityValue));
    }


    /// <summary>
    /// Return lowest priority value item and remove item from Queue.
    /// </summary>
    /// <returns>Queue item with lowest priority value.</returns>
    public T Dequeue()
    {
        int bestPriorityIndex = 0;

        // TODO: Use heap to implement this
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Item2 < elements[bestPriorityIndex].Item2)
            {
                bestPriorityIndex = i;
            }
        }

        T bestItem = elements[bestPriorityIndex].Item1;
        elements.RemoveAt(bestPriorityIndex);
        return bestItem;
    }

}
