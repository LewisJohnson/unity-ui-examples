/* 
MIT License

Copyright (c) 2017 Lewis Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections.Generic;
using UnityEngine;

namespace Assets.WorldSpace.Lootbox.Scripts {

    [AddComponentMenu("Scripts/Lootbox/LootBoxInventoryHandler")]
    public class LootBoxInventoryHandler : MonoBehaviour {

        private readonly List<string> itemNameList = new List<string> {
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

        public LootBoxInventoryHandler() {
            LootBoxInventory = new List<string>();
        }

        public List<string> LootBoxInventory { get; private set; }

        public void AddToInventory(string s) {
            LootBoxInventory.Add(s);
        }

        public void PopulateList() {
            foreach (string itemName in itemNameList) {
                AddToInventory(itemName);
            }
        }

        public void RemoveFromInventory(string s) {
            foreach (string item in LootBoxInventory) {
                if (item.Equals(s)) {
                    LootBoxInventory.Remove(item);
                }
            }
        }
    }

}