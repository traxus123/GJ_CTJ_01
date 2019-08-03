using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBag : MonoBehaviour
{
    public GameObject[] Toys;

    private void Awake()
    {
        for (int i = 0; i < Toys.Length; i++)
        {
           Toys[i].SetActive(false);
        }
    }

    public void ChangedToy(int index)
    {
        for (int i = 0; i < Toys.Length; i++)
        {
            if (i != index)
                Toys[i].SetActive(false);
            else
                Toys[i].SetActive(true);
        }
    }
}
