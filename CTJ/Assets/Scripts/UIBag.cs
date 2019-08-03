using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBag : MonoBehaviour
{
    public GameObject[] Toys;
    public GameObject RedCross;

    private void Awake()
    {
        for (int i = 0; i < Toys.Length; i++)
        {
           Toys[i].SetActive(false);
        }
        RedCross.SetActive(false);
    }

    public void ChangedToy(int index, bool alreadyUse)
    {
        for (int i = 0; i < Toys.Length; i++)
        {
            if (i != index)
                Toys[i].SetActive(false);
            else
                Toys[i].SetActive(true);
        }
        RedCross.SetActive(alreadyUse);
    }
}
