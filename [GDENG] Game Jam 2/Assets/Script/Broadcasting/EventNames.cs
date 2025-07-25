using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {
	public const string ON_UPDATE_SCORE = "ON_UPDATE_SCORE";
	public const string ON_CORRECT_MATCH = "ON_CORRECT_MATCH";
	public const string ON_WRONG_MATCH = "ON_WRONG_MATCH";
	public const string ON_INCREASE_LEVEL = "ON_INCREASE_LEVEL";

	public const string ON_PICTURE_CLICKED = "ON_PICTURE_CLICKED";


	public class ARBluetoothEvents {
		public const string ON_START_BLUETOOTH_DEMO = "ON_START_BLUETOOTH_DEMO";
		public const string ON_RECEIVED_MESSAGE = "ON_RECEIVED_MESSAGE";
	}

	public class ARPhysicsEvents {
		public const string ON_FIRST_TARGET_SCAN = "ON_FIRST_TARGET_SCAN";
		public const string ON_FINAL_TARGET_SCAN = "ON_FINAL_TARGET_SCAN";
	}

	public class ExtendTrackEvents {
		public const string ON_TARGET_SCAN = "ON_TARGET_SCAN";
		public const string ON_TARGET_HIDE = "ON_TARGET_HIDE";
		public const string ON_SHOW_ALL = "ON_SHOW_ALL";
		public const string ON_HIDE_ALL = "ON_HIDE_ALL";
		public const string ON_DELETE_ALL = "ON_DELETE_ALL";
	}

    public class EnvironmentEvents
	{
		public const string ON_ENVIRONMENT_DELIRIOUS_MODE = "ON_ENVIRONMENT_DELIRIOUS_MODE";
		public const string ON_ENVIRONMENT_RESET = "ON_ENVIRONMENT_RESET";
		public const string ON_SANITY_CRITICAL = "ON_SANITY_CRITICAL";
    }

    public class PuzzleEvents
    {
        public const string ON_LEVER_PUZZLE_SOLVED = "ON_LEVER_PUZZLE_SOLVED";
        public const string ON_SEQUENCE_PUZZLE_SOLVED = "ON_SEQUENCE_PUZZLE_SOLVED";
		public const string ON_CLASSROOM_PUZZLE_SOLVED = "ON_CLASSROOM_PUZZLE_SOLVED";
    }

}







