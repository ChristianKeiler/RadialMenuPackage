RadialMenu
===

RadialMenu is an Editor and UI Extension for creating Radial Menu Elements.
The package contains a Radial Layout Group as well as a Radial Menu Component.

## Usage

To create a Radial Menu simply click the RadialMenu button on your Unity Menubar and select 'Radial Menu'. A new window will open which lets you chose how many elements you want on your menu.
If you need to add a new Item later on you can do so by clicking the 'Add Item' button on the RadialMenu component. If you want to delete an item simply delete it in the hierarchy and the menu will update itself accordingly.

You may also just create a circular layout with a certain radius. To do so you can add the RadialLayoutGroup component to your GameObject.

## Installation

### Requirements

* Unity 2019.1 or later
* [SoftMaskForUGUI by mob-sakai](https://github.com/mob-sakai/SoftMaskForUGUI)
* TextMeshPro

### About TextMeshPro

TextMeshPro is a temporary requirement. The package was written with a focus on TextMeshPro and while using it will give you the best possible result not everyone may want to use TMP. As such it will be removed as a requirement and changed into an optional Addon at a later date.

### OpenUPM

OpenUPM is currently not supported. Once the package is ready for a full release this will be the official installation method so keep an eye out for the release.

### Git

Copy the repository URL: https://github.com/ChristianKeiler/RadialMenuPackage.git

Inside Unity open Window->Package Manager. On the top left corner of the screen click the + Icon and choose 'Add package from git URL'.

Paste the repository URL into the input field and click 'Add'

### Manual

[Download the latest releases *.zip folder and extract it to a location of your choice](https://github.com/ChristianKeiler/RadialMenuPackage/releases)

Inside Unity open Window->Package Manager. On the top left corner of the screen click the + Icon and choose 'Add package from disk'.

Locate your extracted files, select the package.json file and click 'Open'
