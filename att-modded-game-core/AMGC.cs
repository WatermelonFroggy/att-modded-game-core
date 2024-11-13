using MelonLoader;
using UnityEngine;
using Alta;
using Alta.Chunks;
using Alta.Intelligence;
using System.IO;

[assembly: MelonInfo(typeof(att_modded_game_core.AMGC), "att-modded-game-core", "1.0.0", "WatermelonFrogy", null)]
[assembly: MelonGame("Alta", "A Township Tale")]
[assembly: MelonColor(ConsoleColor.Yellow)]
[assembly: MelonAuthorColor(ConsoleColor.DarkGreen)]
[assembly: MelonPriority(-250)]
[assembly: MelonOptionalDependencies("Hypha")]

namespace att_modded_game_core
{
    public class AMGC : MelonPlugin
    {
        private static KeyCode sirQuacksKey;
        private static bool summoned;

        private Vector3 prefabSpawnPos => VRPlayerController.Current.Head.transform.position;  // Updated for automatic access
        private AssetBundle assetBundle;

        public override void OnEarlyInitializeMelon()
        {
            sirQuacksKey = KeyCode.Space;
        }

        public override void OnInitializeMelon()
        {
            // Load the AssetBundle from the specific path where "SirQuacks" prefab is located
            string assetBundlePath = Path.Combine(Application.persistentDataPath, "Assets", "sirquackscustomitemtest");
            if (File.Exists(assetBundlePath))
            {
                assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
                LoggerInstance.Msg("[AMGC] AssetBundle loaded successfully. :D");
            }
            else
            {
                LoggerInstance.Error($"[AMGC] AssetBundle not found at path: {assetBundlePath} D:");
            }

            LoggerInstance.Msg("[AMGC] Initialized. :D");
        }

        public override void OnLateUpdate()
        {
            // Summon Sir Quacks when the key is pressed
            if (Input.GetKeyDown(sirQuacksKey))
            {
                SummonSirQuacks();
            }
        }

        private void SummonSirQuacks()
        {
            if (!summoned && assetBundle != null)
            {
                LoggerInstance.Msg("Summoning Sir Quacks :D");

                summoned = true;

                // Load the SirQuacks prefab from the asset bundle
                GameObject sirQuacksPrefab = assetBundle.LoadAsset<GameObject>("SirQuacks0927549863/SirQuacks");

                if (sirQuacksPrefab != null)
                {
                    // Instantiate the prefab at the player's current head position
                    GameObject newObject = GameObject.Instantiate(sirQuacksPrefab, prefabSpawnPos, Quaternion.identity);
                    newObject.name = "SirQuacks";
                }
                else
                {
                    LoggerInstance.Error("SirQuacks was not found in asset bundle. D:");
                }
            }
            else if (assetBundle == null)
            {
                LoggerInstance.Error("AssetBundle is not loaded. D:");
            }
        }
    }
}
