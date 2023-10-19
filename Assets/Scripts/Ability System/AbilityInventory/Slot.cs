using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Slot<T> where T : class, ISlotItem<T>
{
    // Internal values
    [SerializeField]
    private T item;

    // GameObject references
    public GameObject slot { get; private set; }
    public Image image { get; private set; }

    public TooltipFormatter formatter { get; private set; }

    public GameObject gameObject { get { return slot; } }

    // Looks funky but it's actually very useful
    // lets us to define callbacks for slot change events
    // basically this allows the slot to notify interested classes when the item in the slot changes
    // useful for keeping track of which augments and skills are slotted
    public delegate void onItemChangeCallback(Slot<T> slot, T _old);

    // We won't really need to 'get' the callback ever, so it's privated to reduce exposure
    public onItemChangeCallback callback { private get; set; }

    // Setter Overrides
    // That way the slot manages its own sprite :)
    public T Item {
        get { return item; }
        set {
            T _old = item;
            item = value;
            if (value != null) {
                image.sprite = item.ASprite;
                image.enabled = true;
                SetFillAmount(1);
            } else {
                image.sprite = null;
                image.enabled = false;
            }
            callback?.Invoke(this, _old);
        }
    }

    public Slot(GameObject slot, T item, onItemChangeCallback callback) {
        this.slot = slot;
        this.callback = callback;
        image = slot.transform.GetChild(0).GetComponent<Image>();
        formatter = slot.transform.GetComponent<TooltipFormatter>();
        Item = item;
    }

    public Slot(GameObject slot, T item) : this(slot, item, null) {}

    public Slot(GameObject slot) : this(slot, null) {}

    // Removed references to deep copying slots, it doesn't make sense now that we have slots managing in-game objects

    public bool IsClear() {
        return item == null;
    }

    // Visual Methods
    public void SetFillAmount(float amount) {
        image.fillAmount = amount;
    }
}
