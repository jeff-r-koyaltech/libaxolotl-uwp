# Wait...deprecated!
You are probably looking for this instead:
https://github.com/langboost/libsignal-protocol-pcl

This will work on Xamarin, all current versions of .NET, etc.

If you want just "modern" .NET support, you can still use this repository. But I'm not planning to maintain it since it's just as easy to use a PCL instead of a UWP library.

# libaxolotl-windows
An implementation of the axolotl protocol, based on WhisperSystems/libaxolotl-java.

Depends on langboost/curve25519-uwp, Google Protocol Buffers, and Strilanc.May.

A very basic sample implementation can be found here:
https://github.com/langboost/axolotl-sample-client

The sample implementation simply proves the concept, and allows someone to step through the Axolotl protocol as it ratchets, encrypts, decrypts, etc.
