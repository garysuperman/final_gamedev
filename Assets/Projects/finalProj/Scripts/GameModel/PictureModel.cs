using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the model of the picture to be matched.
/// By: Neil DG
/// </summary>
public class PictureModel {
	
	public enum PictureType {
		BLUE_FISH = 0,
		GREEN_FISH = 1,
		VIOLET_FISH = 2,
		RED_FISH = 3,
		ORANGE_FISH = 4,
		YELLOW_STAR = 5,
		SPECIAL_BOOT = 6,
		SPECIAL_FLOATABLE = 7,
		SPECIAL_SLIPPER = 8
	}

	private PictureType pictureType;

	public PictureModel(PictureType pictureType) {
		this.pictureType = pictureType;
	}

	public PictureType GetPictureType() {
		return this.pictureType;
	}

	public static PictureType GenerateRandomType() {
		int type = Random.Range ((int) PictureType.BLUE_FISH, (int) PictureType.SPECIAL_SLIPPER);

		return (PictureType)type;
	}

	public static string ConvertTypeToString(PictureType pictureType) {
		if (pictureType == PictureType.BLUE_FISH) {
			return "Blue Fish";
		} else if (pictureType == PictureType.GREEN_FISH) {
			return "Green Fish";
		} else if (pictureType == PictureType.VIOLET_FISH) {
			return "Violet Fish";
		} else if (pictureType == PictureType.RED_FISH) {
			return "Red Fish";
		} else if (pictureType == PictureType.ORANGE_FISH) {
			return "Orange Fish";
		} else if (pictureType == PictureType.YELLOW_STAR) {
			return "Yellow Star";
		} else if (pictureType == PictureType.SPECIAL_BOOT) {
			return "Old Boots";
		} else if (pictureType == PictureType.SPECIAL_FLOATABLE) {
			return "Floatable";
		} else if (pictureType == PictureType.SPECIAL_SLIPPER) {
			return "Slippers";
		} else {
			return "N/A";
		}
	}

	public static int ConvertTypeToInt(PictureType pictureType) {
		return (int)pictureType;
	}
}
