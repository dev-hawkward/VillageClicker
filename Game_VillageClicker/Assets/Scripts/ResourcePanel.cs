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
        [SerializeField] ResourceType[] requiredType = default;
        [SerializeField] int[] requiredCost = default;
        private int currCount = 0;
        private int prevCount = 0;
        private int currPerSecCount = 0;
        private int prevPerSecCount = 0;
        private ResourceCost[] costs;
        private void Start()
        {
            costs = GetComponentsInChildren<ResourceCost>(true);
            for (var i = 0; i < requiredType.Length; i++)
            {
                costs[i].Init(requiredType[i], requiredCost[i]);
            }
        }
        public void OnClickButton()
        {
            var success = GameManager.Inst.TryUseResource(requiredType, requiredCost);
            if (success)
            {
                GameManager.Inst.AddResource(resourceType, 1);
                if (particle != null)
                {
                    particle.Play();
                }
            }
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
