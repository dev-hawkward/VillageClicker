using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HW
{
    public class ResourceCost : HWBehaviour
    {
        [SerializeField] Image resourceIcon = default;
        [SerializeField] TextMeshProUGUI countText = default;
        private ResourceType resourceType = default;
        private int requiredCount = default;
        public void Init(ResourceType resourceType, int requiredCount)
        {
            this.resourceType = resourceType;
            this.requiredCount = requiredCount;

            var isActive = resourceType != ResourceType.NA || requiredCount > 0;
            this.gameObject.SetActive(isActive);

            resourceIcon.sprite = GameManager.Inst.GetResourceIcon(resourceType);
            countText.text = $"{this.requiredCount}";
        }
    }
}
