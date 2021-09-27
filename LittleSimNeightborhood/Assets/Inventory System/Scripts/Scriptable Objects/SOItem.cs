using UnityEngine;

namespace InventorySystem
{
    public enum ItemType
    {
        Food,
        Equipment,
        Cloth,
        Default
    }

    // Used to create asset of the items
    public abstract class SOItem : ScriptableObject
    {
        public string Name;
        public int Id;
        public bool Stackable;

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

        [SerializeField]
        int _id;
        public int Id { get => _id; }

        [SerializeField]
        bool _stackable;
        public bool Stackable { get => _stackable; }

        public Item(SOItem item)
        {
            _name = item.Name;
            _id = item.Id;
            _stackable = item.Stackable;
        }
    }
}
