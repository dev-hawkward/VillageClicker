using UnityEngine;

namespace HW
{
    public class ResourceButton : HWBehaviour
    {
        [SerializeField] ResourceType resourceType;
        [SerializeField] bool isResource;
        private void OnMouseDown()
        {
            Debug.Log("Clicked!");
            if (isResource)
                GameManager.Inst.AddResource(resourceType, 1);
            else
                GameManager.Inst.AddVillager(resourceType, 1);
        }
    }
}
