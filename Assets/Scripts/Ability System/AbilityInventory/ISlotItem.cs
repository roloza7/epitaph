using UnityEngine;

public interface ISlotItem<T> {

    public T Item { get; }
    public Sprite ASprite { get; }

}