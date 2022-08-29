using UnityEngine;

public interface IRefracted
{
    public void Refract(Vector3 hitPoint, Vector3 direction);
    public void ClearRefraction();
}
