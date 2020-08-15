using UnityEngine;
using TMPro;

namespace HW
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
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
        private int idleVillagers = 10;
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
        public int GetResourcePerSec(ResourceType resourceType)
        {
            return GetVillager(resourceType, isCurrent: true) * 1;
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

        public void TryAssignVillager(ResourceType resourceType, int count = 1)
        {
            if (idleVillagers >= count)
            {
                idleVillagers -= count;
                AddVillager(resourceType, count);
            }
        }
        public void TryRemoveVillager(ResourceType resourceType, int count = 1)
        {
            if (GetVillager(resourceType) >= 1)
            {
                UseVillager(resourceType, count);
                idleVillagers += count;
            }
        }

        private void OnAdd(ResourceType resourceType)
        {
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
        private void OnUse() { }
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
                var villagerCount = currVillagers[i];
                if (villagerCount > 0)
                {
                    AddResource((ResourceType)i, villagerCount);
                }
            }
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
