using UnityEngine;

[CreateAssetMenu(menuName = "EndlessRunner/Random Platform Config")]
public class RandomPlatformConfig : BasePlatformConfig {
    public BasePlatform[] prefabs;
    public float[] platformSpawnProbs;

    public override void ResetConfig() {
        // do nothing
    }

    public override BasePlatform GetNextPrefab() {
        checkParams();
        int value = ChoosePrefab();
        return prefabs[value];
    }

    private void checkParams() {
        if (prefabs.Length != platformSpawnProbs.Length) {
            throw new System.ArgumentException("mismatch between array lengths of 'prefabs' and 'platformSpawnProbs'");
        }

        float sum = 0;
        for (int i = 0; i < platformSpawnProbs.Length; i++) {
            sum += platformSpawnProbs[i];
        }

        if (sum != 1) {
            throw new System.ArgumentException("probabilities in array 'platformSpawnProbs' must sum up to 1");
        }
    }


    private int ChoosePrefab() {
        float probsTotal = 0;

        foreach (float prob in platformSpawnProbs) {
            probsTotal += prob;
        }

        float randomPoint = Random.value * probsTotal;

        for (int i = 0; i < platformSpawnProbs.Length; i++) {
            if (randomPoint < platformSpawnProbs[i]) {
                return i;
            }
            else {
                randomPoint -= platformSpawnProbs[i];
            }
        }
        return platformSpawnProbs.Length - 1;
    }
}
