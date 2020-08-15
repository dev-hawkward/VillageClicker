using UnityEngine;

namespace HW
{
    public class ResourceButton : HWBehaviour
    {
        [SerializeField] ResourceType resourceType;
        [SerializeField] bool isResource;
        [SerializeField] ParticleSystem particle;
        private void OnMouseDown()
        {
            Debug.Log("Clicked!");
            if (isResource)
                GameManager.Inst.AddResource(resourceType, 1);
            else
                GameManager.Inst.AddVillager(resourceType, 1);

            if (particle != null)
            {
                particle.Play();
            }
        }
    }
}
