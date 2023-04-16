using UnityEngine;

public interface IRefractable
{
    public void Refract(Vector3 hitPoint, Vector3 direction);
    public void ClearRefraction();
}
