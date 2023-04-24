using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour
{
    public int Row, Column;
    public int RandomColor;
    public int PaddingTop;
    [SerializeField] private GameObject[] NormalCubePrefabs;
    [SerializeField] private Transform SetBg, Cubes;
    public static Dictionary<Tuple<int, int>, PickUp> Item = new Dictionary<Tuple<int, int>, PickUp>();
    private static List<GameObject> DeleteObject = new List<GameObject>();

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int x = 0; x < Row; x++)
        {
            for (int y = 0; y < Column; y++)
            {
                var clone = Instantiate(NormalCubePrefabs[UnityEngine.Random.Range(0, RandomColor)], new Vector2(x, y), Quaternion.identity);
                clone.transform.SetParent(Cubes);
                clone.AddComponent<CapsuleCollider2D>();
                clone.tag = "Item";
                clone.AddComponent<PickUp>();
                clone.name = x.ToString() + "." + y.ToString();
                clone.GetComponent<PickUp>().x = x;
                clone.GetComponent<PickUp>().y = y;
                Item.Add(new Tuple<int, int>(x, y), clone.GetComponent<PickUp>());
            }
        }

        for (int x = 0; x < Row; x++)
        {
            for (int y = 0; y < Column; y++)
            {
                var clone = Instantiate(NormalCubePrefabs[UnityEngine.Random.Range(0, 
                NormalCubePrefabs.Length)], new Vector2(x, y), 
                Quaternion.identity);
                clone.transform.SetParent(SetBg);
                Destroy(clone.GetComponent<SpriteRenderer>());
                clone.AddComponent<BoxCollider2D>();
                clone.GetComponent<BoxCollider2D>().isTrigger = true;
                clone.AddComponent<Change>();
                clone.name = x.ToString() + "." + y.ToString();
                clone.GetComponent<Change>().x = x;
                clone.GetComponent<Change>().y = y;
                clone.GetComponent<Rigidbody2D>().gravityScale = 0f;
                clone.GetComponent<BoxCollider2D>().size = new Vector2(0.3f , 0.3f);
                clone.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        Invoke("Change_bg", 1f);
    }
    private void Change_bg()
    {
        for (int i = 0; i < SetBg.childCount; i++)
        {
            var _change = SetBg.GetChild(i).GetComponent<Change>();
            SetBg.GetChild(i).transform.position =
                Item[new Tuple<int, int>(_change.x, _change.y)].transform.position;
        }
    }

    public static void Calcul(PickUp p, IDName i)
    {
        var Top = new Tuple<int, int>(p.x, p.y + 1);
        var Down = new Tuple<int, int>(p.x, p.y - 1);
        var Right = new Tuple<int, int>(p.x + 1, p.y);
        var Left = new Tuple<int, int>(p.x - 1, p.y);

        if (Item.ContainsKey(Top))
        {
            if (i.ID == Item[Top].GetComponent<IDName>().ID)
            {
                if (!DeleteObject.Contains(i.gameObject))
                    DeleteObject.Add(i.gameObject);
                if (!DeleteObject.Contains(Item[Top].gameObject))
                {
                    DeleteObject.Add(Item[Top].gameObject);
                    Item[Top].Continue();
                }
            }
        }
        if (Item.ContainsKey(Down))
        {
            if (i.ID == Item[Down].GetComponent<IDName>().ID)
            {
                if (!DeleteObject.Contains(i.gameObject))
                    DeleteObject.Add(i.gameObject);
                if (!DeleteObject.Contains(Item[Down].gameObject))
                {
                    DeleteObject.Add(Item[Down].gameObject);
                    Item[Down].Continue();
                }
            }
        }
        if (Item.ContainsKey(Right))
        {
            if (i.ID == Item[Right].GetComponent<IDName>().ID)
            {
                if (!DeleteObject.Contains(i.gameObject))
                    DeleteObject.Add(i.gameObject);
                if (!DeleteObject.Contains(Item[Right].gameObject))
                {
                    DeleteObject.Add(Item[Right].gameObject);
                    Item[Right].Continue();
                }
            }
        }
        if (Item.ContainsKey(Left))
        {
            if (i.ID == Item[Left].GetComponent<IDName>().ID)
            {
                if (!DeleteObject.Contains(i.gameObject))
                    DeleteObject.Add(i.gameObject);
                if (!DeleteObject.Contains(Item[Left].gameObject))
                {
                    DeleteObject.Add(Item[Left].gameObject);
                    Item[Left].Continue();
                }
            }
        }
    }

    public void Delete()
    {
        Invoke("Delete_Cubes", 0.1f);
        Invoke("Enable", 1f);   
    }

    private void SpawnBack(PickUp p)
    {
        var clone = Instantiate(NormalCubePrefabs[UnityEngine.Random.Range(0, RandomColor)], new Vector2(p.x, p.y + PaddingTop), Quaternion.identity);
        clone.transform.SetParent(Cubes);
        clone.AddComponent<CapsuleCollider2D>();
        clone.tag = "Item";
        clone.AddComponent<PickUp>();
    }

    private void Delete_Cubes()
    {
        for (int i = 0; i < DeleteObject.Count; i++)
        {
            SpawnBack(DeleteObject[i].GetComponent<PickUp>());
            Destroy(DeleteObject[i]);
        }
        Item.Clear();
        DeleteObject.Clear();
    }

    private void Enable()
    {
        for (int i = 0; i < SetBg.childCount; i++)
        {
            SetBg.GetChild(i).GetComponent<BoxCollider2D>().enabled = true;
        }
        Invoke("Disable", 0.1f);
    }

    private void Disable()
    {
        for (int i = 0; i < SetBg.childCount; i++)
        {
            SetBg.GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
