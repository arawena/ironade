using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class FileOperations {
	public static int[,] ReadFile(String nameOfFile) {
		
		TextAsset textFile = (TextAsset)Resources.Load (nameOfFile);

		String noSpacesText = textFile.text.Replace (" ", "");
		char[] separators = { '\n', '\r' };
		var lines = noSpacesText.Split (separators,StringSplitOptions.RemoveEmptyEntries);

		int[,] resultsArray = new int[lines.Length,lines[0].Length];

		for (int i = 0; i < resultsArray.GetLength(0); i++) {
			for (int j = 0; j < resultsArray.GetLength(1); j++) {
				resultsArray [i, j] = (int)Char.GetNumericValue(lines[i][j]);
			}
		}
		return resultsArray;
	}

	public static void WriteFile(String nameOfFile, int[,]gridStructure) {
		
		using (StreamWriter writer = new StreamWriter(nameOfFile, false)) {
			
			writer.WriteLine (gridStructure.GetLength (1).ToString ());
			String lineText = "";

			for (int i = 0; i < gridStructure.GetLength(0); i++) {
				lineText = "";
				for (int j = 0; j < gridStructure.GetLength(1); j++) {
					lineText += gridStructure[i,j] + " ";
				}
				writer.WriteLine (lineText);
			}

		}
    }

    public static List<LevelDescription> getLevelDescription(String nameOfFile)
    {
        int k = 0;
        TextAsset textFile = (TextAsset)Resources.Load(nameOfFile);

        char[] separators = { '\n', '\r' };
        var lines = textFile.text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        List < LevelDescription > resultsArray = new List<LevelDescription>();

        int numberOfLevels = int.Parse(lines[k]);
        k++;

        for (int i=0; i<numberOfLevels; i++)
        {
            LevelDescription level;
            level.budget = int.Parse(lines[k]);
            int numberOfRounds = int.Parse(lines[k+1]);
            level.enemySpawnDetails = new List<List<EnemySpawn>>();
            k += 2;
            for(int j=k; j<k+numberOfRounds;j++)
            {
                string[] enemyRound = lines[j].Split(' ');
                List<EnemySpawn> oneRound = new List<EnemySpawn>();
                for(int q = 0; q<enemyRound.Length; q+=2)
                {
                    EnemySpawn spawn = new EnemySpawn { enemyType = (CharacterType)int.Parse(enemyRound[q]) , spawnTime = float.Parse(enemyRound[q+1]) };
                    oneRound.Add(spawn);
                }
                level.enemySpawnDetails.Add(oneRound);
            }
            k += numberOfRounds;
            resultsArray.Add(level);
        }
        return resultsArray;
    }

}