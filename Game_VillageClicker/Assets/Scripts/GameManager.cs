using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace HW
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] AudioClip se_woodGather = default;
        [SerializeField] AudioClip se_stoneGather = default;
        [SerializeField] AudioClip se_metalGather = default;
        [SerializeField] AudioSource audioSource = default;
        [SerializeField] Sprite[] resourceIcons = new Sprite[(int)ResourceType.Max];
        private const float TickInterval = 1.0f;
        private bool isStarted = false;
        public float currTime { get; private set; }
        private float deltaTime = 0f;
        private int[] currResources = new int[(int)ResourceType.Max];
        private int[] prevResources = new int[(int)ResourceType.Max];
        private int[] currVillagers = new int[(int)ResourceType.Max];
        private int[] prevVillagers = new int[(int)ResourceType.Max];
        private float[] currIntervals = new float[(int)ResourceType.Max];
        public Sprite GetResourceIcon(ResourceType resourceType) => resourceIcons[(int)resourceType];
        public int GetResource(ResourceType resourceType, bool isCurrent = true) => (isCurrent) ? currResources[(int)resourceType] : prevResources[(int)resourceType];
        public void AddResource(ResourceType resourceType, int count)
        {
            prevResources[(int)resourceType] = currResources[(int)resourceType];
            currResources[(int)resourceType] += count;
            OnAdd(resourceType);
        }
        public bool TryUseResource(ResourceType[] resourceTypes, int[] counts)
        {
            for (var i = 0; i < resourceTypes.Length; i++)
            {
                var resourceType = resourceTypes[i];
                var resourceCount = counts[i];
                if (resourceType == ResourceType.NA || resourceType == ResourceType.Click || resourceType == ResourceType.Max || resourceCount == 0)
                {
                    continue;
                }
                else
                {
                    var curr = GetResource(resourceType);
                    var req = resourceCount;
                    if (curr < req) return false;
                }
            }

            for (var i = 0; i < resourceTypes.Length; i++)
            {
                var resourceType = resourceTypes[i];
                var resourceCount = counts[i];
                if (resourceType == ResourceType.NA || resourceType == ResourceType.Click || resourceType == ResourceType.Max || resourceCount == 0)
                {
                    continue;
                }
                else
                {
                    UseResource(resourceType, resourceCount);
                }
            }
            return true;
        }
        public void UseResource(ResourceType resourceType, int count)
        {
            prevResources[(int)resourceType] = currResources[(int)resourceType];
            currResources[(int)resourceType] -= count;
            OnUse();
        }
        public int GetResourcePerSec(ResourceType resourceType, ResourceType[] reqResources, int[] reqCosts)
        {
            var canGather = true;
            for (var i = 0; i < reqResources.Length; i++)
            {
                var reqResource = reqResources[i];
                var reqCost = reqCosts[i];
                if (reqResource == ResourceType.NA || reqResource == ResourceType.Click || reqResource == ResourceType.Max || reqCost == 0)
                {
                    continue;
                }
                else
                {
                    var curr = GetResource(reqResource);
                    if (curr < reqCost) canGather = false;
                }
            }

            return (canGather) ? GetResourcePerSec(resourceType) : 0;
        }
        private int GetResourcePerSec(ResourceType resourceType) => GetVillager(resourceType, isCurrent: true) * 1;
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
            if (GetResource(ResourceType.Villager) >= count)
            {
                UseResource(ResourceType.Villager, count);
                AddVillager(resourceType, count);
            }
        }
        public void TryRemoveVillager(ResourceType resourceType, int count = 1)
        {
            if (GetVillager(resourceType) >= 1)
            {
                UseVillager(resourceType, count);
                AddResource(ResourceType.Villager, count);
            }
        }

        public void TryGatherResource(ResourceType resourceType, ResourceType[] resourceTypes, int[] counts)
        {
            if (GetInterval(resourceType) < 0f)
            {
                var count = GetVillager(resourceType, isCurrent: true);
                if (count > 0)
                {
                    for (var i = 0; i < count; i++)
                    {
                        var success = TryUseResource(resourceTypes, counts);
                        if (success)
                            AddResource(resourceType, 1);
                        else
                            break;
                    }
                    SetInterval(resourceType);
                }
            }
        }
        private float GetInterval(ResourceType resourceType) => currIntervals[(int)resourceType];
        private void SetInterval(ResourceType resourceType) => currIntervals[(int)resourceType] = TickInterval;

        private void OnAdd(ResourceType resourceType)
        {

        }
        private void OnUse() { }
        public void OnClick(ResourceType resourceType)
        {
            if (!isStarted)
            {
                isStarted = true; //To start the game
            }

            //SE must be added for villager and raft
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
        protected override void UnityAwake()
        {
        }
        private void Update()
        {
            if (!isStarted) return;
            deltaTime = Time.deltaTime;

            currTime += deltaTime;
            for (var i = 0; i < currIntervals.Length; i++)
            {
                currIntervals[i] -= deltaTime;
            }
        }
    }

    public enum ResourceType
    {
        NA,
        Click,
        Wood,
        Stone,
        Metal,
        Villager,
        Raft,
        Max
    }
}
