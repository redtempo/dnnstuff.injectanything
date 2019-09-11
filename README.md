<mark>IMPORTANT: This project is not being maintained going forward. If you are interested in taking over this repo, please open an issue and I'll make arrangements for the transfer</mark>

# Inject Anything Module for DNN (DotNetNuke)

A DNN module that allows you to inject javascript/vbscript/css into your page

## Donate

Opensource at Red Tempo is done with spare time, nights and weekends. [Please consider donating](https://paypal.me/redwards1966/25) to help add new features and fix bugs for newer DNN builds

## Minimum DNN Version

Current releases support DNN 7.2.0 and later

## Documentation

[Documentation](https://redtempo.github.io/dnnstuff.injectanything/)

## Building Extension Package

To build a package for installing with the DNN extension installer do the following:

Drop to a command line and go into the build folder

Run `build.bat [version] [configuration]`

where:

* version is the version formatted as major.minor.patch (ie. 01.04.05)
* configuration is the build configuration to use (Debug or Release)

Example:

* `build.bat 01.04.05 Release` will created a release build with version 01.04.05

All builds are written into the Build\Deploy folder
