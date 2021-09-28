using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public enum ClothType 
    {
        Body,
        Glasses,
        Belt
    }

    [CreateAssetMenu(fileName = "New Cloth Object", menuName = "Inventory System/Items/Cloth")]
    public class SOClothObject : SOItem
    {
        [Space(15), Header("Item Specific Fields")]
        public ClothType TypeCloth;
        public RuntimeAnimatorController Animator;

        private void Awake()
        {
            Type = ItemType.Cloth;
        }
    }
}
