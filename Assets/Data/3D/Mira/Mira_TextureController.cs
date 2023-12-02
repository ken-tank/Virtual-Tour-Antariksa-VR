using UnityEngine;

public class Mira_TextureController : MonoBehaviour
{
    Material material;

    [SerializeField] Texture2D[] texturesList; 

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public void SetTexture(int index) 
    {
        material.SetTexture("_Mask", texturesList[index]);
    }
}
