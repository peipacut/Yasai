![Untitled](https://user-images.githubusercontent.com/28855597/133410381-8996ebf2-7a67-42fa-915f-e711a330dbb0.png)

the funny vegetable framework, this serves as my general graphics playground. Powered by SDL.

# What
[infinite monkey theorem](https://en.wikipedia.org/wiki/Infinite_monkey_theorem) but it's being applied to writing a C# graphics framework. 

This framework features an awful mismatch of imperative and declerative programming styles to iterate interfaces in a concise yet comprehensive fashion. What you're looking at here is the culmination of years of wasted time and effort to produce what is subpar at best!

see how much of part 1 is left to go on [the part 1 project page](https://github.com/EpicTofuu/Yasai/projects/1) 

## Why
I like writing code that does nothing impressive.

# Contribution and Use
This is literally a worse version of [osu!framework](https://github.com/ppy/osu-framework), if you're looking for something similar just use that.

This project is still in *part 1* of its development. Although development will be fast paced and code will be frantically put together, I will still take the time to review any issues or PRs that are sent. 

**At this point in time, although the code is open source, the general development process will be largely kept to myself**. This will change after part 1.

## Testing 
Pressing tab shows a test picker that allows you to view different test scenarios. Please read [this guide](https://github.com/EpicTofuu/Yasai/wiki/Testing) on Yasai testing

# Getting started
At the moment, the framework doesn't actually work. You can still test it by cloning the repo and following the steps below

**This project uses submodules!** Ensure when you are cloning, you initialise the submodules correctly using 
`git submodule update --init --recursive`

## Windows
1. Download [SDL](https://www.libsdl.org/download-2.0.php), [SDL_image](https://www.libsdl.org/projects/SDL_image/) and [SDL_ttf](https://www.libsdl.org/projects/SDL_ttf/). 
2. Place the .dlls in the root of the Yasai.Tests folder. Ensure you set the build settings to *Build if newer* so the dlls are present in the application folder.

## Linux
1. Using your distro's package manager, install SDL2, SDL_image and SDL_ttf. 

If your distro is not listed here, a quick google search for *[distro name] SDL2 install* should give you all the information you need. Apart from running a few commands, no extra setup is required.

### Arch linux
Everything you need to get started with SDL and yasai is already available on the AUR 
```
pacman -S sdl2 sdl2_ttf sdl2_image
```
### Ubuntu/Debian
see also [this page](https://lazyfoo.net/tutorials/SDL/01_hello_SDL/linux/index.php)

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
The instructions for the missing files workaround haven't been confirmed if it applies everywhere.
