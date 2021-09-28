using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Cloth Object", menuName = "Inventory System/Items/Cloth")]
    public class SOClothObject : SOItem
    {
        [Space(15), Header("Item Specific Fields")]
        public RuntimeAnimatorController Animator;
    }
}
