using UnityEngine;
using UnityEngine.UI;

public class Slot<T> where T : class, ISlotItem<T>
{
    // Internal values
    [SerializeField]
    internal T item;

    // GameObject references
    public GameObject slot { get; private set; }
    public Image image { get; private set; }

    public TooltipFormatter formatter { get; private set; }

    public GameObject gameObject { get { return slot; } }

    // Setter Overrides
    // That way the slot manages its own sprite :)
    public T Item {
        get { return item; }
        set {
            item = value;
            if (value != null) {
                image.sprite = item.ASprite;
                image.enabled = true;
            } else {
                image.sprite = null;
                image.enabled = false;
            }
            
        }
    }

    public Slot(GameObject slot, T item) {
        this.slot = slot;
        image = slot.transform.GetChild(0).GetComponent<Image>();
        formatter = slot.transform.GetComponent<TooltipFormatter>();
        Item = item;
    }

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
