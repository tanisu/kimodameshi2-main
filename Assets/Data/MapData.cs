using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="CreateScriptable/Create MapData")]
public class MapData : ScriptableObject
{
    public List<Map> maps = new List<Map>();

}

[System.Serializable]
public class Map
{
    public string text;
    public bool[] collects;
    public GameObject[] items;
    public GameObject[] doors;
}