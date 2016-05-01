using UnityEngine;
using System.Collections;
using System;
using System.IO;

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

}