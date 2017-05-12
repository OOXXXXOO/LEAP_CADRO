#pragma strict

import System.Collections.Generic;

// This script will only work with the SpaceD skin
var SpaceDSkin : GUISkin;
var Image1 : Texture2D;

// Some info about our loading animation
var LoadingAnimation1 : Texture2D;
private var LoadingAnimation1TileX : int = 23; //Here you can place the number of columns of your sheet. 
private var LoadingAnimation1TileY : int = 1; //Here you can place the number of rows of your sheet. 
private var LoadingAnimation1FPS : float = 30.0;
private var LoadingAnimation1TexOffset : Vector2 = Vector2(0, 0);
private var LoadingAnimation1TexSize : Vector2 = Vector2(1.0 / LoadingAnimation1TileX, 1.0 / LoadingAnimation1TileY);
private var LoadingAnimation1Start : boolean = true;
private var LoadingAnimation1Percentage : float = 0.0;
private var LA1PUT : float = 0.100;
private var LA1NPUT : float = 0.0;

// Scaling the GUI
private var originalWidth : float = 1920;
private var originalHeight : float = 1200;
private var scale: Vector3;

// Windows
private var Windows : SDWindowInfo[] = new SDWindowInfo[4];

// The first window info
Windows[0].rect = Rect(0, 0, 484, 711);
Windows[0].Alpha = 0.0;
Windows[0].UIAlpha = 0.0;
Windows[0].Show = true;
Windows[0].TimeToWait = 0.0;
Windows[0].Speed = 2.0;

// The second window info
Windows[1].rect = Rect(484, 0, 484, 711);
Windows[1].Alpha = 0.0;
Windows[1].UIAlpha = 0.0;
Windows[1].Show = true;
Windows[1].TimeToWait = 0.0;
Windows[1].Speed = 2.0;

// The third window info
Windows[2].rect = Rect(968, 0, 484, 711);
Windows[2].Alpha = 0.0;
Windows[2].UIAlpha = 0.0;
Windows[2].Show = true;
Windows[2].TimeToWait = 0.0;
Windows[2].Speed = 2.0;

// The login window
Windows[3].rect = Rect(1473, 0, 424, 473);
Windows[3].Alpha = 0.0;
Windows[3].UIAlpha = 0.0;
Windows[3].Show = true;
Windows[3].TimeToWait = 1.0; // If you wish to delay the Fade execution, set this value to the UI time when it should execute, eg: Time.time +_2.0;
Windows[3].Speed = 2.0;

// Toggle booleans
private var rememberme = false;

private var textAreaStr : String = "FSuspendisse potenti. Cras eleifend nisi sit amet molestie pellentesque. Fusce vehicula eros neque, a suscipit tortor tristique id. Nam ullamcorper luctus tempus. Sed posuere volutpat dolor. Nunc nibh lacus, congue eu scelerisque non, euismod non libero. ";
private var textFieldStr : String = "Click to edit this text input field";
private var textFieldStr1 : String = "Click to edit this text input field";
private var textFieldStr2 : String = "Please enter username";
private var textFieldStr3 : String = "Please enter password";

private var scrollPosition : Vector2 = Vector2.zero;
private var scrollViewText : String = "Fusce ac justo ornare, tempor purus eu, sagittis diam. Donec eu erat eget odio ullamcorper iaculis. Proin placerat tincidunt velit, id pharetra dolor tempor vitae.Donec eu erat eget odio ullamcorper iaculis. Fusce ac justo ornare, tempor purus eu, sagittis diam. Donec eu erat eget odio ullamcorper iaculis. Fusce ac justo ornare, tempor purus eu, sagittis diam. Donec eu erat eget odio ullamcorper iaculis. Proin placerat tincidunt velit, id pharetra dolor tempor vitae.Donec eu erat eget odio ullamcorper iaculis.";

private var radioSelected : int = 0;

private var sliderValue0 : float = 0.5;
private var sliderValue1 : float = 0.5;

function Update()
{
	// Window Animations
	// Loop trough our windows
	for (var i : int = 0; i < Windows.Length; i++)
	{
		// Check if delay is set
		if (Time.time >= Windows[i].TimeToWait)
		{
			var newAlpha : float;
			
			// Determine wether to fadeIn or fadeOut
			if (Windows[i].Show)
			{
				// FadeIn
				if (Windows[i].Alpha < 1.0)
		    	{
		    		newAlpha = Windows[i].Alpha + (Time.deltaTime * Windows[i].Speed);
					Windows[i].Alpha = newAlpha;
					Windows[i].UIAlpha = newAlpha;
				}
				else
				{
					Windows[i].Alpha = 1.0; // Accounts for Time.deltaTime variance
					Windows[i].UIAlpha = 1.0;
				}
			}
			else
			{
				// FadeOut
				if (Windows[i].Alpha > 0.0)
		    	{
		    		newAlpha = Windows[i].Alpha - (Time.deltaTime * Windows[i].Speed);
					Windows[i].Alpha = newAlpha;
					Windows[i].UIAlpha = newAlpha - (Time.deltaTime * Windows[i].Speed) * 2; // On FadeOut we must increase the UI fading speed abit
				}
				else
				{
					Windows[i].Alpha = 0.0; // Accounts for Time.deltaTime variance
					Windows[i].UIAlpha = 0.0;
				}
			}
		}
	}
    
    // Loading Animation 1
    if (LoadingAnimation1Start && LoadingAnimation1)
    {
	    // Calculate index
		var index : int = Time.time * LoadingAnimation1FPS;
		// repeat when exhausting all frames
		index = index % (LoadingAnimation1TileX * LoadingAnimation1TileY);
	 
		// Size of every tile
		LoadingAnimation1TexSize = Vector2 (1.0 / LoadingAnimation1TileX, 1.0 / LoadingAnimation1TileY);
	 
		// split into horizontal and vertical index
		var uIndex = index % LoadingAnimation1TileX;
		var vIndex = index / LoadingAnimation1TileX;
	 	
		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		LoadingAnimation1TexOffset = Vector2(uIndex * LoadingAnimation1TexSize.x, 1.0 - LoadingAnimation1TexSize.y - vIndex * LoadingAnimation1TexSize.y);
		
		// Calculate the percentage
		var totalFrames : int = LoadingAnimation1TileX * LoadingAnimation1TileY;
		var percentage  : float;
		percentage = (1.0 * index) / (1.0 * totalFrames);
		percentage = percentage * 100;
		
		// Set the percentage for usage outside of this functions scope
		if (Time.time >= LA1NPUT)
		{
			LoadingAnimation1Percentage = Mathf.Round(percentage);
			LA1NPUT += LA1PUT;
		}
	}
}

function OnGUI()
{
	GUI.skin = SpaceDSkin;
	
	// Do the windows
	GUI.color.a = Windows[0].Alpha;
	Windows[0].rect = GUI.Window(0, Windows[0].rect, DoWindow0, "");
	
	GUI.color.a = Windows[1].Alpha;
	Windows[1].rect = GUI.Window(1, Windows[1].rect, DoWindow1, "");
	
	GUI.color.a = Windows[2].Alpha;
	Windows[2].rect = GUI.Window(2, Windows[2].rect, DoWindow2, "");

	GUI.color.a = Windows[3].Alpha;
	Windows[3].rect = GUI.Window(3, Windows[3].rect, DoWindow3, "");
	
	//now adjust to the group. (0,0) is the topleft corner of the group.
	GUI.BeginGroup(Rect(0,0,100,100));
	// End the group we started above. This is very important to remember!
	GUI.EndGroup ();
}

function DoWindow0 (windowID : int) 
{
	GUI.color.a = Windows[windowID].UIAlpha;
	
	// Do the window title
	DoWindowTitle(windowID, "WINDOW TITLE");

	// Do a text with the first style
	DoTextStyle1(Rect(79, 134, 332, 71), "Suspendisse potenti. Cras eleifend nisi sit amet molestie pellentesque. Fusce vehicula eros neque, a suscipit tortor tristique id. Nam ullamcorper luctus tempus. Sed posuere volutpat dolor. ");
	
	// Do an image
	if (Image1)
		DoImage(Vector2(74, 223), Image1);
	
	// Do a separator
	DoSeparator(Vector2(72, 355));
	
	// Do some toggles
	Windows[3].Show = DoToggle(Vector2(77, 386), Windows[3].Show, "Display the login window?");
	Windows[1].Show = DoToggle(Vector2(77, 426), Windows[1].Show, "Display the second window?");
	Windows[2].Show = DoToggle(Vector2(77, 466), Windows[2].Show, "Display the thrid window?");
	
	// Do a fliped separator
	DoSeparator(Vector2(72, 511));
	
	// Do a text with the second style
	DoTextStyle2(Rect(72, 550, 339, 90), "Suspendisse potenti. Cras eleifend nisi sit amet molestie pellentesque. Fusce vehicula eros neque, a suscipit tortor tristique id. Nam ullamcorper luctus tempus. Sed posuere volutpat dolor. ");

	// Make the windows be draggable.
	GUI.DragWindow (Rect (0,0,10000,10000));
}

function DoWindow1 (windowID : int) 
{
	GUI.color.a = Windows[windowID].UIAlpha;
	
	// Do the window title
	DoWindowTitle(windowID, "SECOND WINDOW");

	// Do a button
	DoButton(Rect(127, 132, 230, 71), "BUTTON");
	
	// Do a text area
	//textAreaStr = GUI.TextArea(Rect(68, 226, 350, 182), textAreaStr);
	
	// Do a text container
	GUI.BeginGroup(Rect(68, 226, 350, 182), GUIStyle("textContainer"));
	DoTextContainerTitle(Rect(27, 25, 293, 20), "Text Container");
	DoTextContainerText(Rect(27, 55, 293, 71), "Suspendisse potenti. Cras eleifend nisi sit amet molestie pellentesque. Fusce vehicula eros neque, a suscipit tortor tristique id. Nam ullamcorper luctus tempus. Sed posuere volutpat dolor. Nunc nibh lacus, congue eu scelerisque non, euismod non libero.");
	GUI.EndGroup();
	
	// Do a text field
	textFieldStr = GUI.TextField(Rect(68, 427, 350, 66), textFieldStr);
	
	var radios : String[] = new String[3];
	radios[0] = "Semper facilisis tellus ?";
	radios[1] = "Phasellus eu sodales leo!";
	radios[2] = "Aliquam semper facilisis tellus...";
	
	// Do a group of radio style toggles
	radioSelected = ToggleList(Rect(77, 515, 337, 112), radioSelected, radios);
	
	// Make the windows be draggable.
	GUI.DragWindow (Rect (0,0,10000,10000));
}

function DoWindow2 (windowID : int) 
{
	GUI.color.a = Windows[windowID].UIAlpha;
	
	// Do the window title
	DoWindowTitle(windowID, "THIRD WINDOW");
	
	// Do a scroll view
	var viewPort : Rect = Rect(0, 0, 350, 180);
	scrollPosition = GUI.BeginScrollView(Rect(82, 143, 322, 154), scrollPosition, viewPort);
	// Do a text with the first style
	DoTextStyle1(Rect(12, 7, viewPort.width, viewPort.height), scrollViewText);
	// End the scroll view that we began above.
	GUI.EndScrollView();
	
	// Do a horizontal slider
	LA1PUT = GUI.HorizontalSlider(Rect(73, 332, 340, 13), LA1PUT, 0.0, 0.2);
	
	// Do a vertical slider
	LoadingAnimation1FPS = GUI.VerticalSlider(Rect(73, 366, 13, 160), LoadingAnimation1FPS, 20.0, 40.0);
	
	// Do the loading animation
	if (LoadingAnimation1)
		DoAnimation1(Vector2(183, 388));
	
	// Do a label
	DoLabel(Rect(121, 523, 241, 43), "Input label comes here");
	
	// Do a text field
	textFieldStr1 = GUI.TextField(Rect(68, 559, 350, 66), textFieldStr1);

	// Make the windows be draggable.
	GUI.DragWindow (Rect (0,0,10000,10000));
}

function DoWindow3 (windowID : int) 
{
	GUI.color.a = Windows[windowID].UIAlpha;
	
	// Do a label
	DoLabel(Rect(67, 53, 290, 43), "Username / Email");
	
	// Do a text field
	textFieldStr2 = GUI.TextField(Rect(67, 90, 290, 66), textFieldStr2);
	
	// Do a label
	DoLabel(Rect(67, 153, 290, 43), "Password");
	
	// Do a text field
	textFieldStr3 = GUI.TextField(Rect(67, 190, 290, 66), textFieldStr3);
	
	// Do a toggle
	rememberme = DoToggle(Vector2(142, 275), rememberme, "Remember me?");
	
	// Do a button
	DoButton(Rect(91, 325, 242, 71), "LOGIN");
	
	// Make the windows be draggable.
	GUI.DragWindow (Rect (0,0,10000,10000));
}

function DoLabel(r : Rect, text : String)
{
	var LabelStyle : GUIStyle = GUI.skin.GetStyle("label");
	var LabelShadowStyle : GUIStyle = GUI.skin.GetStyle("labelTextShadow");

	DoTextWithShadow(r, GUIContent(text), LabelStyle, LabelStyle.normal.textColor, LabelShadowStyle.normal.textColor, Vector2(1.0, 1.0));
}

function DoWindowTitle(windowID : int, text : String)
{
	var bgOffset : Vector2 = Vector2(36, 15);
	// Determine the width and height of the title background
	var bgWidth : float = Windows[windowID].rect.width - (2 * bgOffset.x);
	var bgHeight : float = 91;
	var windowTitleBGRect = Rect(bgOffset.x, bgOffset.y, bgWidth, bgHeight);
	
	// Lay the title background
	GUI.Label(windowTitleBGRect, "", "windowTitleBackground");
	
	var titleOffset0 : Vector2 = Vector2(0, 56);
	var titleOffset1 : Vector2 = Vector2(2, 53);
	var titleOffset2 : Vector2 = Vector2(-3, 57);
	var titleOffset3 : Vector2 = Vector2(1, 55);
	var titleOffset4 : Vector2 = Vector2(4, 53);
	var titleSize : Vector2 = Vector2(Windows[windowID].rect.width, 29);
	
	// Draw the title
	GUI.Label(Rect(titleOffset0.x, titleOffset0.y, titleSize.x, titleSize.y), text, "windowTitle");
	GUI.Label(Rect(titleOffset1.x, titleOffset1.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
	GUI.Label(Rect(titleOffset2.x, titleOffset2.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
	GUI.Label(Rect(titleOffset3.x, titleOffset3.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
	GUI.Label(Rect(titleOffset4.x, titleOffset4.y, titleSize.x, titleSize.y), text, "windowTitleShadow");
	
	var olOffset : Vector2 = Vector2(37, 41);
	// Determine the width and height of the title overlay
	var olWidth : float = Windows[windowID].rect.width - (2 * olOffset.x);
	var olHeight : float = 55;
	var windowTitleOLRect : Rect = Rect(olOffset.x, olOffset.y, olWidth, olHeight);
	
	// Lay the title overlay
	GUI.Label(windowTitleOLRect, "", "windowTitleOverlay");
}

function DoAnimation1(offset : Vector2)
{
	GUI.BeginGroup(Rect(offset.x, offset.y, 128, 106));
	
	// Set the background
	GUI.Label(Rect(0, 0, 128, 106), "", "animationBackground");
	
	// Draw the texture
	var position : Rect = new Rect(24, 14, 80, 78);
	var texCoords : Rect = new Rect(LoadingAnimation1TexOffset.x, LoadingAnimation1TexOffset.y, LoadingAnimation1TexSize.x, LoadingAnimation1TexSize.y);
	var alpha : boolean = true;
	
	GUI.DrawTextureWithTexCoords(position, LoadingAnimation1, texCoords, alpha);
	
	// Do the percentage
	var PercentageStyle : GUIStyle = GUI.skin.GetStyle("animationPercentage");
	var TextStyle : GUIStyle = GUI.skin.GetStyle("animationText");
	var TextShadowStyle : GUIStyle = GUI.skin.GetStyle("animationTextShadow");

	DoTextWithShadow(Rect(35, 33, 59, 25), GUIContent(LoadingAnimation1Percentage + "%"), PercentageStyle, PercentageStyle.normal.textColor, TextShadowStyle.normal.textColor, Vector2(0.0, 1.0));
	DoTextWithShadow(Rect(35, 56, 59, 15), GUIContent("loading"), TextStyle, TextStyle.normal.textColor, TextShadowStyle.normal.textColor, Vector2(0.0, 1.0));
	
	GUI.EndGroup();
}

function ScaleRect(rect : Rect, scale : float) : Rect
{
	scale = scale * 100;
	
	var newRect : Rect = new Rect(0, 0, 0, 0);
	newRect.x = Mathf.CeilToInt((rect.x / 100) * scale);
	newRect.y = Mathf.CeilToInt((rect.y / 100) * scale);
	newRect.width = Mathf.CeilToInt((rect.width / 100) * scale);
	newRect.height = Mathf.CeilToInt((rect.height / 100) * scale);
	
	return newRect;
}

function DoImage(offset : Vector2, imageTexture : Texture2D)
{
	var frameSize : Vector2 = Vector2(imageTexture.width + 8, imageTexture.height + 8);
	
	GUI.BeginGroup(Rect(offset.x, offset.y, frameSize.x, frameSize.y));
	GUI.Label(Rect(0, 0, frameSize.x, frameSize.y), "", "imageFrame");
	GUI.DrawTexture(Rect(4, 4, imageTexture.width, imageTexture.height), imageTexture);
	GUI.EndGroup();
}

function DoTextWithShadow(rect : Rect, content : GUIContent, style : GUIStyle, txtColor : Color, shadowColor : Color, direction : Vector2)
{
    var backupStyle : GUIStyle = new GUIStyle(style);

    style.normal.textColor = shadowColor;
    rect.x += direction.x;
    rect.y += direction.y;
    GUI.Label(rect, content, style);

    style.normal.textColor = txtColor;
    rect.x -= direction.x;
    rect.y -= direction.y;
    GUI.Label(rect, content, style);

    style = backupStyle;
}

function DoTextStyle1(r : Rect, text : String)
{
	var TextStyle : GUIStyle = GUI.skin.GetStyle("textStyle1");
	var TextStyleShadow : GUIStyle = GUI.skin.GetStyle("textStyle1Shadow");
	
	DoTextWithShadow(r, GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyleShadow.normal.textColor, Vector2(2.0, 1.0));
}

function DoTextStyle2(r : Rect, text : String)
{
	var TextStyle : GUIStyle = GUI.skin.GetStyle("textStyle2");
	var TextStyleShadow : GUIStyle = GUI.skin.GetStyle("textStyle2Shadow");
	
	DoTextWithShadow(r, GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyleShadow.normal.textColor, Vector2(2.0, 1.0));
}

function DoSeparator(offset : Vector2)
{
	GUI.Label(Rect(offset.x, offset.y, 340, 16), "", "separator");
}

function DoToggle(offset : Vector2, toggle : boolean, text : String) : boolean
{
	var ToggleTextStyle : GUIStyle = GUI.skin.GetStyle("toggleText");
	var ToggleTextShadowStyle : GUIStyle = GUI.skin.GetStyle("toggleTextShadow");
	
	GUI.BeginGroup(Rect(offset.x, offset.y, 349, 146));
	toggle = GUI.Toggle(Rect(0, 0, 32, 32), toggle, "");
	DoTextWithShadow(Rect(39, 2, 278, 32), GUIContent(text), ToggleTextStyle, ToggleTextStyle.normal.textColor, ToggleTextShadowStyle.normal.textColor, Vector2(2.0, 1.0));
	GUI.EndGroup();
	
	return toggle;
}

// Displays a vertical list of toggles and returns the index of the selected item.
function ToggleList(offset : Rect, selected : int, items : String[]) : int
{
    // Keep the selected index within the bounds of the items array
    selected = (selected < 0) ? 0 : (selected >= items.Length ? items.Length - 1 : selected);
	
	// Get the radio toggles style
	var radioStyle : GUIStyle = GUI.skin.GetStyle("radioToggle");
	var ToggleTextStyle : GUIStyle = GUI.skin.GetStyle("toggleText");
	var ToggleTextShadowStyle : GUIStyle = GUI.skin.GetStyle("toggleTextShadow");
	
	// Get the toggles height
	var height : int = radioStyle.fixedHeight;
	var width : int = radioStyle.fixedWidth;
	
	GUI.BeginGroup(Rect(offset.x, offset.y, offset.width, (height * items.Length) + height));
    GUILayout.BeginVertical();
    
    var offsetY : float = 0;
    var textOffsetX : float = 37;
    
    for (var i : int = 0; i < items.Length; i++)
    {
        // Display toggle. Get if toggle changed.
        var change : boolean = GUI.Toggle(Rect(0, offsetY, width, height), selected == i, "", radioStyle);
		DoTextWithShadow(Rect(textOffsetX, (offsetY + 3), (offset.width - textOffsetX), height), GUIContent(items[i]), ToggleTextStyle, ToggleTextStyle.normal.textColor, ToggleTextShadowStyle.normal.textColor, Vector2(1.0, 1.0));
		
        // If changed, set selected to current index.
        if (change)
            selected = i;
            
        // Increase the offset for the next toggle
        offsetY = offsetY + (height + 8);
    }

    GUILayout.EndVertical();
	GUI.EndGroup();
	
    // Return the currently selected item's index
    return selected;
}

function DoTextContainerTitle(r : Rect, text : String)
{
	var TextStyle : GUIStyle = GUI.skin.GetStyle("textContainerTitle");
	
	DoTextWithShadow(r, GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyle.hover.textColor, Vector2(2.0, 1.0));
}

function DoTextContainerText(r : Rect, text : String)
{
	var TextStyle : GUIStyle = GUI.skin.GetStyle("textContainerText");
	
	DoTextWithShadow(r, GUIContent(text), TextStyle, TextStyle.normal.textColor, TextStyle.hover.textColor, Vector2(2.0, 1.0));
}

function DoButton(r : Rect, content : String) : boolean
{
	var ButtonTextStyle : GUIStyle = GUI.skin.GetStyle("buttonText");
	var backupStyle : GUIStyle = new GUIStyle(ButtonTextStyle);
	var ShadowStyle : GUIStyle = GUI.skin.GetStyle("buttonTextShadow");
	
	var size : Rect = Rect(0, 0, r.width, r.height);
	var buttonRect : Rect = Rect(13, 13, (r.width - (13 * 2)), (r.height - (13 * 2)));
	
	GUI.BeginGroup(r);
	
	// Do the button, exclude the overflow from the size
    var result : boolean = GUI.Button(buttonRect, "");
    
	// Get the colors for diferrent scenarios
    var color : Color = (buttonRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0)) ? ButtonTextStyle.active.textColor : (buttonRect.Contains(Event.current.mousePosition) ? ButtonTextStyle.hover.textColor : ButtonTextStyle.normal.textColor);
	var colorShadow : Color = (buttonRect.Contains(Event.current.mousePosition) && Input.GetMouseButton(0)) ? ShadowStyle.active.textColor : (buttonRect.Contains(Event.current.mousePosition) ? ShadowStyle.hover.textColor : ShadowStyle.normal.textColor);
	var direction : Vector2 = Vector2(0.0, 1.0);
	
	// Do the text
    DoTextWithShadow(size, GUIContent(content), ButtonTextStyle, color, colorShadow, direction);
	
	GUI.EndGroup();
	
	// Restore the color
	ButtonTextStyle.normal.textColor = backupStyle.normal.textColor;
	
    return result;
}

class SDWindowInfo extends System.ValueType
{
	var rect : Rect;
	var Alpha : float;
	var UIAlpha : float;
	var Show : boolean;
	var TimeToWait : float;
	var Speed : float;
	
	public function SDWindowInfo(rect : Rect, alpha : float, uialpha : float, show : boolean, TimeToWait : int, speed : float)
	{
		this.rect = rect;
		this.Alpha = alpha;
		this.UIAlpha = uialpha;
		this.Show = show;
		this.TimeToWait = TimeToWait;
		this.Speed = speed;
	}
}