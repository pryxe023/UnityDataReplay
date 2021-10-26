# UnityDataReplay
A system for replaying recorded motions (position and rotation data) in Unity.

To record data from Unity, I've used Alex's (https://github.com/Avdbergnmf) UnityDataLogging package with my (super simple) GameObject logger found in my fork (https://github.com/pryxe023/UnityDataLogging).

## How it works
The recorded data (see above) is in csv-files. This (Re)player allows you to move GameObjects based on the information in these csv-files.

**This is still a work in progress.**

:construction:

### CSV format
It is important that the format of your csv-files is as follows (from left to right);

1. Title
1. Position X
1. Position Y
1. Position Z
1. Rotation X
1. Rotation Y
1. Rotation Z
1. Rotation W
1. Timestamp

Again, for this the use of the UnityDataLogging package with the GameObject logger (see above) is recommended.

## Use

Simply add the `GameObjectMotionReplay.cs` and `ReplayManager.cs` files to your Unity assets.

Create `GameObject`s to be controlled by the scripts. Attach the `GameObjectMotionReplay.cs` to each `GameObject`.
In this script, attach the csv-file containing the positional and rotational data to the correct `GameObject`.

Next, create an empty `GameObject` (and for simplicity call it 'Replay Manager') and attach the `ReplayManager.cs` script to it.
Here you can change general settings and connect the replayable instances of `GameObject` to the manager.

That's it!

## Future updates

Nothing in the works right now.

### Known issues

* Currently; none!