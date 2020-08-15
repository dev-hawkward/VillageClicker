using TMPro;
using UnityEngine;

namespace HW
{
    public class VillagerPanel : HWBehaviour
    {
        [SerializeField] ResourceType resourceType = default;
        [SerializeField] TextMeshProUGUI currCountText = default;
        private int currCount = 0;
        private int prevCount = 0;
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
            currCount = GameManager.Inst.GetVillager(resourceType);
            if (currCount != prevCount)
            {
                currCountText.text = $"{currCount}";
                prevCount = currCount;
            }
        }
    }
}
