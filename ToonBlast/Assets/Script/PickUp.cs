using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int x, y;

    private void OnMouseDown()
    {
        Game.Calcul(GetComponent<PickUp>(), GetComponent<IDName>());
        FindObjectOfType<Game>().Delete();
    }

    public void Continue()
    {
        Game.Calcul(GetComponent<PickUp>(), GetComponent<IDName>());
    }
}