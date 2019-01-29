# Nokia Service Tool (or No Service Tool)
This is a small hobby project that aims to make the service tool for HMD-Nokia
phones, called "OST LA" more useable and user-friendly.

Changes include:
 * Moved the logs folder to the same folder as the application, as opposed to somewhere on the system
 * Includes the neccessary patches to remove the login prompt at the start.
 * The installation options should show relatively reliable now, for me they never show with stock OST
 * Added support for different types of firmware packages, see below for a detailed explanation

NOST is based on an unmodified OST LA 6.0.4. Instead of patching and replacing its
executable NOST wraps it with a custom launcher, which allows it to patch the relative
methods at runtime.

## Custom firmware packages
For some time people who wanted to reflash their phone had to download a firmware
bundle, extract and edit it to be able to use it with OST LA 6.0.4, since the newer
versions had unpatchable issues that prevented using them. Repacking the images in a
format OST expects wasn't possible either since that enabled some sort of signature
algorithm on the modified images and caused the flashing to fail.

NOST solves this problem by allowing the use of a different packaging format.
Those binaries still need to be extracted but it is done transparently in the
background without the user having to download any other tools. The formats that
can be used in images are .zip and .qlz

### .zip firmwares
.zip firmware files are simply archives of the (edited) files that would normally
be extracted from an .nb0 file. This means, if you extract a .nb0 with the extractor
found on XDA, the contents of the `*_unpacked` folder it creates should be the
contents of your .zip.

### .qlz firmwares
.qlz files are based on [QuickLZ](https://www.quicklz.com/) compression, which gives them a small
size but also a low decompression time. The tool to generate them is called [exdupe](https://www.quicklz.com/exdupe/).

Generating these images is pretty straigtforward. Assuming you are on windows,
download the exdupe tool from the link above (or take it from the NOST `Tools` folder)
and copy it into the folder that contains the unpacked .nb0.

```
- exdupe.exe
- <nb0 name>_unpacked/
    - <nb0 name>.mlf
    - ....
```

Open a commandline in that folder, and run the following command:

```
exdupe.exe <name of the folder to compress> <name of the firmware file>.qlz
```

You should already see how fast it compresses the firmware folder now.
As a reference: Compressing the latest Nokia 8 firmware (about 4GB) takes maybe
30 seconds and yields a 2GB file.

## Future ideas
So far this project was powered by pure annoyance about the lack of a standard
service tool for Nokia Phones, and the requirement for using outdated versions,
patched executables, and patched firmware images. I have some more ideas that I would
like to implement, like for example a small check that tells you if your bootloader
is unlocked to critical, or other small convenience stuff. Larger features are also
possible, but harder since everything has to be written as a patch.

## License
OST LA 6.0.4 is copyrighted by the respective authors. It is not modified permanently.

The custom NOST code is licensed under the GNU General Public License.

Icon by Freepik © Flaticon
