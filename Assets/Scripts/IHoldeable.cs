using UnityEngine;

public interface IHoldable
{
    void Hold();
}

public interface IGestures
{
    GameObject GetGameObject();
    Sprite GetSprite();
    Color GetColor();
}