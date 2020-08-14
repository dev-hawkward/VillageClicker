using UnityEngine;
using TMPro;

namespace HW
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] TextMeshProUGUI woodCount;
        [SerializeField] TextMeshProUGUI stoneCount;
        [SerializeField] TextMeshProUGUI metalCount;
        private const float TickInterval = 1.0f;
        private float interval;
        private int[] resources = new int[(int)ResourceType.Max];
        private int[] villagers = new int[(int)ResourceType.Max];
        public int GetResource(ResourceType resourceType) => resources[(int)resourceType];
        public void AddResource(ResourceType resourceType, int count) => resources[(int)resourceType] += count;
        public void UseResource(ResourceType resourceType, int count) => resources[(int)resourceType] -= count;
        protected override void UnityAwake()
        {

        }
        private void Update()
        {
            interval -= Time.deltaTime;
            if (interval < 0f)
            {
                //Automatically gather resources by villager
                GatherResourcesByVillager();
                interval += TickInterval;
            }
        }

        private void GatherResourcesByVillager()
        {
            for (var i = 1; i < (int)ResourceType.Max; i++)
            {
                var villagerCount = villagers[i];
                if (villagerCount > 0)
                {
                    AddResource((ResourceType)i, villagerCount);
                }
            }
        }

        private void LateUpdate()
        {

        }
    }

    public enum ResourceType
    {
        NA,
        Wood,
        Stone,
        Metal,
        Max
    }
}
