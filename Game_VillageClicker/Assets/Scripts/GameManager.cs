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
        [SerializeField] AudioClip se_woodGather;
        [SerializeField] AudioClip se_stoneGather;
        [SerializeField] AudioClip se_metalGather;
        [SerializeField] AudioClip se_Click;
        [SerializeField] AudioSource audioSource;
        private const float TickInterval = 1.0f;
        private float interval;
        private int[] currResources = new int[(int)ResourceType.Max];
        private int[] prevResources = new int[(int)ResourceType.Max];
        private int[] currVillagers = new int[(int)ResourceType.Max];
        private int[] prevVillagers = new int[(int)ResourceType.Max];
        public int GetResource(ResourceType resourceType, bool isCurrent = true) => (isCurrent) ? currResources[(int)resourceType] : prevResources[(int)resourceType];
        public void AddResource(ResourceType resourceType, int count)
        {
            prevResources[(int)resourceType] = currResources[(int)resourceType];
            currResources[(int)resourceType] += count;
            OnAdd(resourceType);
        }
        public void UseResource(ResourceType resourceType, int count)
        {
            prevResources[(int)resourceType] = currResources[(int)resourceType];
            currResources[(int)resourceType] -= count;
            OnUse();
        }
        public int GetVillager(ResourceType resourceType, bool isCurrent = true) => (isCurrent) ? currVillagers[(int)resourceType] : prevVillagers[(int)resourceType];
        public void AddVillager(ResourceType resourceType, int count)
        {
            prevVillagers[(int)resourceType] = currVillagers[(int)resourceType];
            currVillagers[(int)resourceType] += count;
            OnAdd(resourceType);
        }
        public void UseVillager(ResourceType resourceType, int count)
        {
            prevVillagers[(int)resourceType] = currVillagers[(int)resourceType];
            currVillagers[(int)resourceType] -= count;
            OnUse();
        }
        private void OnAdd(ResourceType resourceType)
        {
            RefreshScreen();
            switch (resourceType)
            {
                case ResourceType.Wood:
                    audioSource.clip = se_woodGather;
                    break;
                case ResourceType.Stone:
                    audioSource.clip = se_stoneGather;
                    break;
                case ResourceType.Metal:
                    audioSource.clip = se_metalGather;
                    break;
            }
            audioSource.Play();
        }
        private void OnUse() => RefreshScreen();
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
            RefreshScreen();
        }

        private void GatherResourcesByVillager()
        {
            for (var i = 1; i < (int)ResourceType.Max; i++)
            {
                var villagerCount = currVillagers[i];
                if (villagerCount > 0)
                {
                    AddResource((ResourceType)i, villagerCount);
                }
            }
        }

        private void RefreshScreen()
        {
            RefreshResourceCount();
            RefreshVillagerCount();
        }

        //TODO: You need to modify the timing of reshres screen
        private void RefreshResourceCount()
        {
            TryRefreshResourceCountText(woodCount, ResourceType.Wood);
            TryRefreshResourceCountText(stoneCount, ResourceType.Stone);
            TryRefreshResourceCountText(metalCount, ResourceType.Metal);
        }

        private void TryRefreshResourceCountText(TextMeshProUGUI countText, ResourceType resourceType)
        {
            var curr = GetResource(resourceType, isCurrent: true);
            var prev = GetResource(resourceType, isCurrent: false);
            if (curr != prev) countText.text = $"{curr}";
        }

        private void RefreshVillagerCount()
        {
            TryRefreshVillagerCountText(woodVillagerCount, ResourceType.Wood);
            TryRefreshVillagerCountText(stoneVillagerCount, ResourceType.Stone);
            TryRefreshVillagerCountText(metalVillagerCount, ResourceType.Metal);
        }

        private void TryRefreshVillagerCountText(TextMeshProUGUI countText, ResourceType resourceType)
        {
            var curr = GetVillager(resourceType, isCurrent: true);
            var prev = GetVillager(resourceType, isCurrent: false);
            if (curr != prev) countText.text = $"{curr}";
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
