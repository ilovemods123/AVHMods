using System;
using UnityEngine;
using static DoggysMod.ModMain;
namespace DoggysMod{
	public class MyGUIClass:MonoBehaviour{
		public static bool ShowHideWindow0;
		public static Rect WindowUiRect0;
		public static bool ShowHideWindow1;
		public static Rect WindowUiRect1;
		public static bool ShowHideWindow2;
		public static Rect WindowUiRect2;
		public static bool ShowHideWindow3;
		public static Rect WindowUiRect3;
		public static bool ShowHideWindow4;
		public static Rect WindowUiRect4;
		public static bool ShowHideWindow5;
		public static Rect WindowUiRect5;
		public static string currencyString;
		public static string livesString;
		public static string speedString;
		public static float movementSpeed;
		public static bool resetSmovementSpeed;
		public static bool noknockback;
		public void OnGUI(){
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(GUILayout.Button("Doggy's Mod",new GUILayoutOption[0])){
				ShowHideWindow0=!ShowHideWindow0;
			}
			if(ShowHideWindow0){
				WindowUiRect0=GUILayout.Window(0,WindowUiRect0,new GUI.WindowFunction(WindowUI0),"Doggy's Mod",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			if(ShowHideWindow1){
				WindowUiRect1=GUILayout.Window(1,WindowUiRect1,new GUI.WindowFunction(WindowUI1),"Money and Lives",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			if(ShowHideWindow2){
				WindowUiRect2=GUILayout.Window(2,WindowUiRect2,new GUI.WindowFunction(WindowUI2),"Test2",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			if(ShowHideWindow3){
				WindowUiRect3=GUILayout.Window(3,WindowUiRect3,new GUI.WindowFunction(WindowUI3),"Test3",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			if(ShowHideWindow4){
				WindowUiRect4=GUILayout.Window(4,WindowUiRect4,new GUI.WindowFunction(WindowUI4),"Test4",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			if(ShowHideWindow5){
				WindowUiRect5=GUILayout.Window(5,WindowUiRect5,new GUI.WindowFunction(WindowUI5),"Test5",new GUILayoutOption[]{
					GUILayout.MaxWidth(300f),
					GUILayout.MinWidth(200f)
				});
			}
			GUILayout.EndHorizontal();
		}
		public static void WindowUI0(int windowID){
			if(GUILayout.Button("Player",Array.Empty<GUILayoutOption>())){
				ShowHideWindow1=!ShowHideWindow1;
			}
			GUI.DragWindow();
		}
		public static void WindowUI1(int windowID){
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(GUILayout.Button("Change Money to",Array.Empty<GUILayoutOption>())){
				int number=0;
				if(int.TryParse(currencyString,out number)){
					changeMoney(number);
				}
			}
			currencyString=GUILayout.TextField(currencyString,Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(GUILayout.Button("Change Lives to",Array.Empty<GUILayoutOption>())){
				int number2=0;
				if(int.TryParse(livesString,out number2)){
					changeLives(number2);
				}
			}
			livesString=GUILayout.TextField(livesString,Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(GUILayout.Button("Change Speed to",Array.Empty<GUILayoutOption>())){
				int num=0;
				if(int.TryParse(speedString,out num)){
					movementSpeed=num;
				}
			}
			speedString=GUILayout.TextField(speedString,Array.Empty<GUILayoutOption>());
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(resetSmovementSpeed){
				GUI.backgroundColor=Color.green;
			}else{
				GUI.backgroundColor=Color.grey;
			}
			if(GUILayout.Button("Reset Speed",Array.Empty<GUILayoutOption>())){
				resetSmovementSpeed=!resetSmovementSpeed;
			}
			GUILayout.EndHorizontal();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if(noknockback){
				GUI.backgroundColor=Color.green;
			}else{
				GUI.backgroundColor=Color.grey;
			}
			if(GUILayout.Button("No knockback",Array.Empty<GUILayoutOption>())){
				noknockback=!noknockback;
			}
			GUILayout.EndHorizontal();
			GUI.DragWindow();
		}
		public static void WindowUI2(int windowID){
			GUI.DragWindow();
		}
		public static void WindowUI3(int windowID){
			GUI.DragWindow();
		}
		public static void WindowUI4(int windowID){
			GUI.DragWindow();
		}
		public static void WindowUI5(int windowID){
			GUI.DragWindow();
		}
		public static void changeMoney(int number){
			Currency.instance.UpdateCurrency(number);
		}
		public static void changeLives(int number){
			PlayerHealth.instance.UpdateHealth(number);
		}
	}
}