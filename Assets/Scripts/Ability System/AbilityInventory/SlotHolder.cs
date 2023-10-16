using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotHolder<T> : IEnumerable<Slot<T>> where T : class, ISlotItem<T> {

    // Formatting properties
    [SerializeField]
    private int _width;
    private int _length;

    public int Length { get { return _length; }}

    // Internal properties
    private Slot<T>[] slots; 

    public ISlotHolderItems Items { get; private set; }
    public SlotHolder() {
        Items = new ISlotHolderItems(this);
    }

    public SlotHolder (GameObject root, int begin = 0) : this() {
        _length = root.transform.childCount - begin;
        slots = new Slot<T>[_length];
        for (int i = begin; i < root.transform.childCount; i++) {
            slots[i - begin] = new Slot<T>(root.transform.GetChild(i).gameObject);
        }
    }

    // Overriding [] operator to access individual slots
    public Slot<T> this[int key] {
        get {
            return slots[key];
        }
    }

    // Easy traversal yay
    public IEnumerator<Slot<T>> GetEnumerator() {
        foreach (Slot<T> slot in slots) {
            yield return slot;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }

    // Easy item acess
    public class ISlotHolderItems {
        private SlotHolder<T> _parent;
        public ISlotHolderItems(SlotHolder<T> parent) {
            _parent = parent;
        }
        public T this[int key] {
            get => _parent.slots[key].Item;
            set => _parent.slots[key].Item = value;
        }
    }
}