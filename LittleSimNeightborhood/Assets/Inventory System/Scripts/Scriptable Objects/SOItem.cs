using UnityEngine;

namespace InventorySystem
{
    public enum ItemType
    {
        Glasses,
        Belt,
        Food,
        Axe,
        Default
    }

    // Used to create asset of the items
    public abstract class SOItem : ScriptableObject
    {
        public string Name;
        public int Price;
        public bool Stackable;
        public Item Data = new Item();

        [Space(7)]
        public ItemType Type;

        [Space(7)]
        public Sprite UIIcon;
        public Sprite UIBackground;

        [TextArea(2, 4), Space(7)]
        public string Description;
    }

    // Serializable item that gets the informations from the scriptable object item
    [System.Serializable]
    public class Item
    {
        [SerializeField]
        string _name;
        public string Name { get => _name; }

        public int Id = -1;

        [SerializeField]
        bool _stackable;
        public bool Stackable { get => _stackable; }

        public Item(SOItem item)
        {
            _name = item.Name;
            Id = item.Data.Id;
            _stackable = item.Stackable;
        }

        public Item()
        {
            _name = "";
            Id = -1;
            _stackable = false;
        }
    }
}
