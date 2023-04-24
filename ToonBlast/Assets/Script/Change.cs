using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Change : MonoBehaviour
{
    public int x, y;

    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.name = x.ToString() + "." + y.ToString();
            collision.gameObject.GetComponent<PickUp>().x = x;
            collision.gameObject.GetComponent<PickUp>().y = y;
            if (!Game.Item.ContainsKey(new Tuple<int, int>(x, y)))
                Game.Item.Add(new Tuple<int, int>(x, y), collision.gameObject.GetComponent<PickUp>());
        }
    }
}
