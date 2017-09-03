using System.Collections.Generic;
using UnityEngine;

namespace Assets.WorldSpace.Lootbox.Scripts
{
    public class LootBoxInventoryHandler : MonoBehaviour {

        public List<string> LootBoxInventory { get; private set; }

        private readonly List<string> _itemNameList = new List<string>
        {
            "Golden Dagger",
            "Dragon Coins",
            "Magical Dust",
            "Cloth",
            "Hide",
            "Silk",
            "Cotton",
            "Leather",
            "5P Tesco Bag For Life"
        };

        public LootBoxInventoryHandler()
        {
            LootBoxInventory = new List<string>();
        }

        public void PopulateList() {
            foreach (string itemName in _itemNameList) {
                AddToInventory(itemName);
            }
        }

        public void AddToInventory(string s) {
            LootBoxInventory.Add(s);
        }

        public void RemoveFromInventory(string s) {
            foreach (var item in LootBoxInventory) {
                if (item.Equals(s)) {
                    LootBoxInventory.Remove(item);
                }
            }
        }
    }
}
