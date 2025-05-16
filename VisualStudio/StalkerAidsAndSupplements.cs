using StalkerAidsAndSupplementsMod;
using Il2Cpp;
using Il2CppRewired.Utils;
using Il2CppTLD.BigCarry;
using Il2CppTLD.Gear;
using System.Runtime.InteropServices;
using MelonLoader;
using Il2CppNodeCanvas.Tasks.Conditions;
using ModComponent.API;
using Il2CppTLD.Gameplay.Condition;
using Unity.VisualScripting;
using Il2CppNodeCanvas.Tasks.Actions;
using UnityStandardAssets.ImageEffects;
using ModComponent.API.Behaviours;
using Il2CppTLD.IntBackedUnit;
using UnityEngine.Playables;
using System.Reflection.Emit;

namespace StalkerAidsAndSupplementsMod
{
    public class StalkerAidsAndSupplements : MelonMod
    {
        private static AssetBundle? assetBundle;

        internal static AssetBundle MoreMedsBundle
        {
            get => assetBundle ?? throw new System.NullReferenceException(nameof(assetBundle));
        }
        public override void OnInitializeMelon()
        {
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Welcome to the zone, stalker...");
            assetBundle = LoadAssetBundle("StalkerAidsAndSupplements.moremedsassets");
            Settings.instance.AddToModSettings("Stalker Aids And Supplements");
        }
        public static bool Sceneloaded;
        public override void OnSceneWasLoaded(int buildindex, string scenename)
        {
            if (StalkerAidsAndSupplementsUtils.IsScenePlayable(scenename)) 
            {
                Sceneloaded = true;

                ChangeItemProperties();
            }
        }
        private static AssetBundle LoadAssetBundle(string path)
        {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            MemoryStream memoryStream = new MemoryStream((int)stream.Length);
            stream.CopyTo(memoryStream);

            return memoryStream.Length != 0
                ? AssetBundle.LoadFromMemory(memoryStream.ToArray())
                : throw new System.Exception("No data loaded!");
        }
        private static void ChangeItemProperties() 
        {
            FoodItem.Nutrient VitaminC = new FoodItem.Nutrient();
            VitaminC.m_Amount = Settings.instance.VitaminCSmall;
            VitaminC.m_Nutrient = new Il2CppTLD.Gameplay.AssetReferenceNutrientDefinition("13a8bda1e12982e428b7551cc01b01df");
            FoodItem.Nutrient VitaminCJam = new FoodItem.Nutrient();
            VitaminCJam.m_Amount = Settings.instance.VitaminCJam;
            VitaminCJam.m_Nutrient = new Il2CppTLD.Gameplay.AssetReferenceNutrientDefinition("13a8bda1e12982e428b7551cc01b01df");

            GameObject gear;
            gear = GearItem.LoadGearItemPrefab("GEAR_BottleVitaminC").gameObject;

            gear.GetComponent<FoodItem>().m_CaloriesTotal = Settings.instance.VitaminCalories;
            gear.GetComponent<FoodItem>().m_CaloriesRemaining = Settings.instance.VitaminCalories;
            gear.GetComponent<FoodItem>().m_Nutrients = new Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient>();
            gear.GetComponent<FoodItem>().m_Nutrients.Add(VitaminC);

            gear = GearItem.LoadGearItemPrefab("GEAR_SleepingPills").gameObject;

            gear.GetComponent<ConditionRestBuff>().m_ConditionRestBonus = Settings.instance.SleepingHP;
            gear.GetComponent<ConditionRestBuff>().m_NumHoursRestAffected = Settings.instance.SleepingBonusDuration;
            gear.GetComponent<FatigueBuff>().m_InitialPercentDecrease = Settings.instance.SleepingIncrease;
            gear.GetComponent<FoodItem>().m_CaloriesTotal = Settings.instance.SleepingCalories;
            gear.GetComponent<FoodItem>().m_CaloriesRemaining = Settings.instance.SleepingCalories;

            gear = GearItem.LoadGearItemPrefab("GEAR_BottleCaffeine").gameObject;

            gear.GetComponent<FatigueBuff>().m_InitialPercentDecrease = Settings.instance.CaffeineDecrease;
            gear.GetComponent<FatigueBuff>().m_DurationHours = Settings.instance.CaffeineTime;
            gear.GetComponent<FoodItem>().m_CaloriesTotal = Settings.instance.CaffeineCalories;
            gear.GetComponent<FoodItem>().m_CaloriesRemaining = Settings.instance.CaffeineCalories;

            gear = GearItem.LoadGearItemPrefab("GEAR_FirstAidKitPainKiller").gameObject;

            gear.GetComponent<FirstAidItem>().m_AppliesBandage = false;
            gear.GetComponent<FirstAidItem>().m_HPIncrease = Settings.instance.FirstAidKit;
            gear.AddComponent<ConditionOverTimeBuff>();
            gear.GetComponent<ConditionOverTimeBuff>().m_ConditionIncreasePerHour = Settings.instance.FirstAidAmount;
            gear.GetComponent<ConditionOverTimeBuff>().m_NumHours = Settings.instance.FirstAidTime;

            gear = GearItem.LoadGearItemPrefab("GEAR_NaturalBandage").gameObject;

            gear.AddComponent<ConditionOverTimeBuff>();
            gear.GetComponent<ConditionOverTimeBuff>().m_ConditionIncreasePerHour = Settings.instance.BandageAmount;
            gear.GetComponent<ConditionOverTimeBuff>().m_NumHours = Settings.instance.BandageTime;
            gear.GetComponent<FirstAidItem>().m_StabalizesSprains = true;

            gear = GearItem.LoadGearItemPrefab("GEAR_RosehipJam").gameObject;

            gear.GetComponent<FoodItem>().m_CaloriesTotal = Settings.instance.JamCalories;
            gear.GetComponent<FoodItem>().m_CaloriesRemaining = Settings.instance.JamCalories;
            gear.GetComponent<FoodItem>().m_Nutrients = new Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient>();
            gear.GetComponent<FoodItem>().m_Nutrients.Add(VitaminCJam);

            gear = GearItem.LoadGearItemPrefab("GEAR_Ibuprofen").gameObject;

            gear.AddComponent<ConditionOverTimeBuff>();
            gear.GetComponent<ConditionOverTimeBuff>().m_ConditionIncreasePerHour = Settings.instance.IbuprofenAmount;
            gear.GetComponent<ConditionOverTimeBuff>().m_NumHours = 1f;

            gear = GearItem.LoadGearItemPrefab("GEAR_MorphineVial").gameObject;

            gear.AddComponent<ConditionOverTimeBuff>();
            gear.GetComponent<ConditionOverTimeBuff>().m_ConditionIncreasePerHour = 2f;
            gear.GetComponent<ConditionOverTimeBuff>().m_NumHours = 3f;

            gear = GearItem.LoadGearItemPrefab("GEAR_UncookedRosehipJam").gameObject;

            gear.GetComponent<Cookable>().m_CanBePickedUpWhileCooking = false;
        }
    }
}