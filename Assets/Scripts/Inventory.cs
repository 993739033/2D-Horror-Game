﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {

	public Dictionary<string, int> MyInventory = new Dictionary<string, int>();
	int MaxItems = 4;

	public int Scrap = 0;
	int MaxScrap = 8;

	public void Init(int _maxItems)
	{
		MaxItems = _maxItems;
	}

	public bool AddItem(string item, int amount)
	{		
        if (MyInventory.Count >= MaxItems)
            return false;

		if(HasItem(item))
			MyInventory[item] += amount;
		else
			MyInventory[item] = amount;

		UpdateInventory();

        return true;
	}

	public int AddMaterial(string item, int amount)
	{
		int ret = Scrap + amount - MaxScrap;
		Scrap = Mathf.Clamp(Scrap + amount, 0, MaxScrap);

		UpdateInventory();

		return ret;
	}

	void UpdateInventory()
	{
		GameUIManager.Instance.SetInventory(MyInventory, MaxItems);
	}

	public bool HasItem(string item)
	{
		return MyInventory.ContainsKey(item);
	}

	public ItemData GetItem(string s)
	{
		return ItemFactory.Instance.GetItem(s);
	}
}
