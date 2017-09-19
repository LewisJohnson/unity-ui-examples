# How to Contribute

Thank you for considering to contributing. We welcome everyone to improve the quality and add content to this repository.

This project adheres to the Contributor Covenant [code of conduct](https://github.com/LewisJohnson/unity-ui-examples/blob/master/CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code. Please report unacceptable behaviour to lewisjohnsondev@gmail.com.

The following is a set of guidelines for contributing to Unity User Interface Examples. These are just guidelines, not rules, use your best judgment and feel free to propose changes to this document in a pull request.

## Table of contents
- [Getting Started](#getting-started)
- [Quick Notes](#quick-notes)
- [Reporting a Bug](#reporting-a-bug)
- [Improving an Example](#improving-an-example)
- [Adding a New Example](#adding-a-new-example)
	- [Quick](#quick-start)
	- [Manual](#manual)

## Getting Started
Before you can contribute, you'll need a [GitHub account](https://github.com/signup/free). If you're a developer you'll need [Unity 2017.1](https://unity3d.com/get-unity/download). Most projects come with a [Visual Studio](https://www.visualstudio.com/) project solution, if you're on Windows, we recommend using it.

#### Quick Notes 
* Feel free to add to the .gitignore file.
* We only accept examples written in C#.
* There should be no spaces in any directory name.

## Reporting a Bug
* Submit an issue, assuming one does not already exist.
* Clearly describe the issue including steps to reproduce.

## Improving an Example
Sometimes an example will have unclean code, a bug, or missing documentation. We welcome you to fix any example you see improvments in.
* Follow the "Reporting a bug" steps.
* Assign yourself to the issue.
* Fork the repository.
* Add your improvements.
* Submit a new pull request.
	* The title should be "Improved {Example Name} example", followed by a detailed expanded description. 
* Wait for it to be approved.

## Adding a New Example
Follow quick start ***or*** manual way.
 
### Quick Start
There is a little python script called ```create-files.py``` in the root folder to help kickstart new examples. Just run the script and follow the prompts then you're good to go!

### Manual
 * Fork the repository.
 * Add the examples code and scene, make sure you follow the same structure as existing examples.
 ```
unity-ui-examples
│   README.md    
└───Assets
    └───Common
	└───ScreenSpace
	│	└───ExampleProjectName
	│		│   README.md
	│		└───Scenes
	│			│   ExampleProjectName.unity
	│		└───Scripts
	│			│   ExampleScript.cs
	│			│   ExampleScript2.cs
	│		└───Images
	│			│   Background.png
	│		└───Etc
	│			│   etc.x
	└───WorldSpace
		└───ExampleProjectName
			│   README.md
			└───Scenes
				│   ExampleProjectName.unity
			└───Scripts
				│   ExampleScript.cs
				│   ExampleScript2.cs
			└───Images
				│   Background.png
			└───Etc
				│   etc.x
```

 * Add a README.md which follows the [documention guideline](https://github.com/LewisJohnson/unity-ui-examples/blob/master/DOCUMENTATION.md).
 * Submit a new pull request.
 * Wait for it to be approved.
