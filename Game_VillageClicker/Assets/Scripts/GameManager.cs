using UnityEngine;
using TMPro;

namespace HW
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] TextMeshProUGUI woodCount;
        [SerializeField] TextMeshProUGUI stoneCount;
        [SerializeField] TextMeshProUGUI metalCount;
        [SerializeField] TextMeshProUGUI woodVillagerCount;
        [SerializeField] TextMeshProUGUI stoneVillagerCount;
        [SerializeField] TextMeshProUGUI metalVillagerCount;
        private const float TickInterval = 1.0f;
        private float interval;
        private int[] resources = new int[(int)ResourceType.Max];
        private int[] villagers = new int[(int)ResourceType.Max];
        public int GetResource(ResourceType resourceType) => resources[(int)resourceType];
        public void AddResource(ResourceType resourceType, int count) => resources[(int)resourceType] += count;
        public void UseResource(ResourceType resourceType, int count) => resources[(int)resourceType] -= count;
        public int GetVillager(ResourceType resourceType) => villagers[(int)resourceType];
        public int AddVillager(ResourceType resourceType, int count) => villagers[(int)resourceType] += count;
        public int UseVillager(ResourceType resourceType, int count) => villagers[(int)resourceType] -= count;
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
                RefreshResourceCount();
                RefreshVillagerCount();
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

        private void RefreshResourceCount()
        {
            woodCount.text = $"{GetResource(ResourceType.Wood)}";
            stoneCount.text = $"{GetResource(ResourceType.Stone)}";
            metalCount.text = $"{GetResource(ResourceType.Metal)}";
        }

        private void RefreshVillagerCount()
        {
            woodVillagerCount.text = $"{GetVillager(ResourceType.Wood)}";
            stoneVillagerCount.text = $"{GetVillager(ResourceType.Stone)}";
            metalVillagerCount.text = $"{GetVillager(ResourceType.Metal)}";
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
