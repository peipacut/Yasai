![Untitled](https://user-images.githubusercontent.com/28855597/133410381-8996ebf2-7a67-42fa-915f-e711a330dbb0.png)

[![Status](https://github.com/EpicTofuu/Yasai/actions/workflows/dotnet.yml/badge.svg)](https://github.com/EpicTofuu/Yasai/actions/workflows/dotnet.yml)
![GitHub](https://img.shields.io/github/license/epictofuu/yasai)

the funny vegetable framework, this serves as my general graphics playground. Powered by OpenGL4 (through OpenTK)

# What
[infinite monkey theorem](https://en.wikipedia.org/wiki/Infinite_monkey_theorem) but it's being applied to writing a C# graphics framework. 

This framework features an awful mismatch of imperative and declerative programming styles to iterate interfaces in a concise yet comprehensive fashion. What you're looking at here is the culmination of years of wasted time and effort to produce what is subpar at best!

## Why
I like writing code that does nothing impressive.

### Why is CI failing
Yasai is currently using SDL2CS through a custom submodule. Too lazy to fix, but later we'll be using OpenTK which won't require submodules. CI will probably work then.

# Contribution and Use
This is literally a worse version of [osu!framework](https://github.com/ppy/osu-framework), if you're looking for something similar just use that.

See [this document](https://docs.google.com/document/d/1iS57Z2sUqg6D1YusFn45NSafXYUQtsQ1NACNWx0kiN0/edit?usp=sharing) for potential tasks. Feel free to raise issues about information regarding a task you wish to complete. **Although the code is open source, the general development process will be largely kept to myself**. Again, raise an issue if you want more information. 

## Testing 
Pressing tab shows a test picker that allows you to view different test scenarios. Please read [this guide](https://github.com/EpicTofuu/Yasai/wiki/Testing) on Yasai testing

# Getting started
Later, the framework will move towards using NuGet packages. Currently your most viable option is to either submodule the framework and use the code directly or wait a little longer for things to settle.

**This project uses submodules!** Ensure when you are cloning, you initialise the submodules correctly using 
`git submodule update --init --recursive`

## Windows
1. Download [SDL](https://www.libsdl.org/download-2.0.php), [SDL_image](https://www.libsdl.org/projects/SDL_image/) and [SDL_ttf](https://www.libsdl.org/projects/SDL_ttf/). 
2. Place the .dlls in the root of the Yasai.Tests folder. Ensure you set the build settings to *Build if newer* so the dlls are present in the application folder.

## Linux
1. Using your distro's package manager, install SDL2, SDL_image and SDL_ttf, check the list below for distro specific information. If your distro is not listed here, a quick google search for *[distro name] SDL2 install* should give you all the information you need. 

### Arch linux
Everything you need to get started with SDL and yasai is already available on the AUR. Your package manager should handle the rest. 
```
sudo pacman -S sdl2 sdl2_ttf sdl2_image
```

### Ubuntu/Debian
Also see [this page](https://lazyfoo.net/tutorials/SDL/01_hello_SDL/linux/index.php)

Currently, the needed packages are installed by running:
```
sudo apt-get install libsdl2-2.0-0 libsdl2-ttf-2.0-0 libsdl2-image-2.0-0
```
Programs using the framework currently cannot be run without some fixes.
Something is expecting files called `libSDL2.so`, `libSDL2_ttf.so`, and `libSDL2_image.so`.
These files are usually symlinks, in this situation to be created manually targetting the respective files.
The simplest way to workaround this is to run the following:
```
cd CertainProgramDir
ln -s /usr/lib/HostTriplet/libSDL2-2.0.so.0        libSDL2.so
ln -s /usr/lib/HostTriplet/libSDL2_ttf-2.0.so.0    libSDL2_ttf.so
ln -s /usr/lib/HostTriplet/libSDL2_image-2.0.so.0  libSDL2_image.so
```
`HostTriplet` is the triplet of your system, for example: `x86_64-linux-gnu`.
The instructions for the missing files workaround haven't been confirmed to apply everywhere.
