using TMPro;
using UnityEngine;

namespace HW
{
    public class ResourcePanel : HWBehaviour
    {
        [SerializeField] ResourceType resourceType = default;
        [SerializeField] TextMeshProUGUI currCountText = default;
        [SerializeField] TextMeshProUGUI perSecCountText = default;
        [SerializeField] ParticleSystem particle = default;
        private int currCount = 0;
        private int prevCount = 0;
        private int currPerSecCount = 0;
        private int prevPerSecCount = 0;
        public void OnClickButton()
        {
            GameManager.Inst.AddResource(resourceType, 1);
            if (particle != null)
            {
                particle.Play();
            }
        }
        public void OnClickVillagerAssignButton()
        {
            GameManager.Inst.TryAssignVillager(resourceType);
        }
        public void OnClickVillagerRemoveButton()
        {
            GameManager.Inst.TryRemoveVillager(resourceType);
        }
        private void Update()
        {
            currCount = GameManager.Inst.GetResource(resourceType);
            if (currCount != prevCount)
            {
                currCountText.text = $"{currCount}";
                prevCount = currCount;
            }

            currPerSecCount = GameManager.Inst.GetResourcePerSec(resourceType);
            if (currPerSecCount != prevPerSecCount)
            {
                perSecCountText.text = $"per second: {currPerSecCount}";
                prevPerSecCount = currPerSecCount;
            }
        }
    }
}
