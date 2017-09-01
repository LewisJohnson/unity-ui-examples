# How to contribute

We welcome everyone to improve the quality and add content to this repository.

This project adheres to the Contributor Covenant [code of conduct](https://github.com/LewisJohnson/unity-ui-examples/blob/master/CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code. Please report unacceptable behavior to lewisjohnsondev@gmail.com.

The following is a set of guidelines for contributing to Unity User Interface Examples. These are just guidelines, not rules, use your best judgment and feel free to propose changes to this document in a pull request.

## Getting Started
Before you can contribute, you'll need a [GitHub account](https://github.com/signup/free). If you're a developer you'll need [Unity 2017.1](https://unity3d.com/get-unity/download). Most projects come with a [Visual Studio](https://www.visualstudio.com/) project solution, if you're on Windows, we recommended you use it.

## Reporting a bug
* Submit an issue, assuming one does not already exist.
* Clearly describe the issue including steps to reproduce.

## Improving an example (i.e. Fixing a bug, cleaning code)
* Follow the "Reporting a bug" steps.
* Fork the repository.
* Fix the code/scene.
* Submit a new pull request.
* Wait for it to be approved.

 ## Adding a new example
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

 * Add a README.md which is like existing examples (try to include a gif).
 * Submit a new pull request.
 * Wait for it to be approved.

## Note 
 * Feel free to add to the .gitignore if you're using a diffrent IDE/OS.
 * We only accept C# examples at this time.
